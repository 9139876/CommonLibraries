using System;
using System.Threading.Tasks;

namespace CommonLibraries.Common.Models
{
    public class ResponseWithStatus<T> : StandartResponse
    {
        public T Value { get; set; }

        public static ResponseWithStatus<T> GetActionResponse(Func<T> func)
        {
            try
            {
                return new ResponseWithStatus<T>() { IsSuccess = true, Value = func.Invoke() };
            }
            catch (Exception ex)
            {
                return new ResponseWithStatus<T>() { IsSuccess = false, Message = ex.Message };
            }
        }

        public static async Task<ResponseWithStatus<T>> GetActionResponseAsync(Func<Task<T>> func)
        {
            try
            {
                return new ResponseWithStatus<T>() { IsSuccess = true, Value = await func.Invoke() };
            }
            catch (Exception ex)
            {
                return new ResponseWithStatus<T>() { IsSuccess = false, Message = ex.Message };
            }
        }
    }
}
