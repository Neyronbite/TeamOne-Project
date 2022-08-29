using Common.Enums;

namespace Common.Response
{
    public class ResponseList<T>
    {
        public ResponseCodes ResponseCode { get; set; }
        public string Description { get; set; }
        public (int, IList<T>) Value { get; set; } = (0, default);

        public ResponseList()
        {

        }
        public ResponseList(ResponseCodes responseCode)
        {
            ResponseCode = responseCode;
        }
        public ResponseList((int, IList<T>) value)
        {
            Value = value;
            ResponseCode = ResponseCodes.Success;
        }
    }
}
