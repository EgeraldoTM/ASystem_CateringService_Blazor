using System.Net;

namespace CateringApi.Helpers.Common.Results
{
	public class ResponseResult<T>
    {
        public T? Value { get; }
        public bool Success { get; }
        public string ErrorMessage { get; }
        public HttpStatusCode StatusCode { get; }

        public ResponseResult(T entity, bool success, string errorMessage, HttpStatusCode statusCode)
        {
            Value = entity;
            Success = success;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }

        public ResponseResult(bool success, string errorMessage, HttpStatusCode statusCode)
        {
            Value = default;
            Success = success;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }

        public static ResponseResult<T> Ok(T entity)
        {
            return new ResponseResult<T>(entity, true, string.Empty, HttpStatusCode.OK);
        }

        public static ResponseResult<T> BadRequest(string errorMessage)
        {
            return new ResponseResult<T>(false, errorMessage, HttpStatusCode.BadRequest);
        }

        public static ResponseResult<T> NotFound(string errorMessage)
        {
            return new ResponseResult<T>(false, errorMessage, HttpStatusCode.NotFound);
        }

        public static ResponseResult<T> Error(string errorMessage)
        {
            return new ResponseResult<T>(false, errorMessage, HttpStatusCode.InternalServerError);
        }

        public static ResponseResult<T> Forbidden(string errorMessage)
        {
            return new ResponseResult<T>(false, errorMessage, HttpStatusCode.Forbidden);
        }

        public static ResponseResult<T> NoContent(string message)
        {
            return new ResponseResult<T>(true, message, HttpStatusCode.NoContent);
        }
    }
}
