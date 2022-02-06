using System;
using System.Threading.Tasks;

namespace CommonLibraries.Common.Models
{
    public class StandartResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public static StandartResponse GetActionResponse(Action action)
        {
            try
            {
                action?.Invoke();
                return new StandartResponse() { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new StandartResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public static async Task<StandartResponse> GetActionResponseAsync(Action action)
        {
            return await Task.Run(() => GetActionResponse(action));
        }
    }
}
