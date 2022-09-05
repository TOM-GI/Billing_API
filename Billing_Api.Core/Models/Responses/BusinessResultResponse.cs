namespace Billing_Api.Core.Models.Responses
{
    public class BusinessResultResponse
    {
        public static BusinessResultResponse Ok() => new BusinessResultResponse(null, true);

        public BusinessResultResponse(string error, bool isSuccess)
        {
            Error = error;
            IsSuccess = isSuccess;
        }

        public string Error { get; protected init; }
        public bool IsSuccess { get; protected init; }
    }

    public class BusinessResultResponse<T> : BusinessResultResponse
        where T : class
    {
        public static BusinessResultResponse<T> Fail(string error) => new BusinessResultResponse<T>(null, error, false);
        public static BusinessResultResponse<T> Ok(T payload) => new BusinessResultResponse<T>(payload, null, true);

        public static BusinessResultResponse<T> SuccessByPayload(T payload)
        {
            if (payload == null) return Fail(null);
            return Ok(payload);
        }

        public BusinessResultResponse(T payload, string error, bool isSuccess)
            : base(error, isSuccess)
        {
            Payload = payload;
        }

        public T Payload { get; }
        //We should probably have status here
    }
}