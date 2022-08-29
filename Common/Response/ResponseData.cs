using Common.Enums;

namespace Common.Response
{
    public class ResponseData<T>
    {
        public ResponseCodes ResponseCode { get; set; }
        public string Description { get; set; }
        public T Value { get; set; } = default;

        public ResponseData()
        {

        }
        public ResponseData(ResponseCodes responseCode)
        {
            ResponseCode = responseCode;
        }
        public ResponseData(T value)
        {
            Value = value;
            ResponseCode = ResponseCodes.Success;
        }
    }
}
