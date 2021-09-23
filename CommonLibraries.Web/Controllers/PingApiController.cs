using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CommonLibraries.Core.Extensions;
using CommonLibraries.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CommonLibraries.Web.Controllers
{
    public class PingApiController : Controller
    {
        private readonly IConfiguration _configuration;

        public PingApiController(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("api/ping/get")]
        public PingResponse Get()
        {
            var ret = new PingResponse { IsSuccess = true };

            return ret;
        }

        [HttpGet]
        [Route("api/ping/date")]
        public DateTime GetDate()
        {
            return DateTime.Now;
        }

        [HttpGet]
        [Route("api/ping/get_env")]
        public EnvResponse GetEnvironment()
        {
            var assemblyInfo = AssemblyInfo.GetAssemblyInfo();

            var ret = new EnvResponse
            {
                AppName = assemblyInfo.Title,
                MachineName = Environment.MachineName,
                Version = assemblyInfo.Version,
                EnvironmentLocation = _configuration.GetSection("EnvironmentLocation").Value
            };

            return ret;
        }

        [HttpGet]
        [Route("api/ping/throw_exception")]
        public EnvResponse ThrowException()
        {
            throw new Exception("Test exception");
        }

        [HttpGet]
        [Route("api/ping/throw_exception_with_inner")]
        public EnvResponse ThrowExceptionWithInner()
        {
            throw new Exception("Test exception", new Exception("Test inner exception"));
        }

        [HttpGet]
        [Route("api/ping/get_config")]
        public string GetConfig()
        {
            var configurationValues = _configuration.AsEnumerable().Where(x => x.Value != null).Serialize();

            return configurationValues;
        }
    }
}
