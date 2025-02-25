using System.Text.Json.Serialization;

namespace MeuBolso.Core.Responses
{
    public class Response<TData>
    {
        [JsonConstructor]
        public Response ()
            => _code = Configuration.DEFAULT_STATUS_CODE;

        public Response(TData? data, int code = Configuration.DEFAULT_STATUS_CODE, string? message = null)
        {
            Data = data;
            Message = message;
            _code = code;
        }

        private readonly int _code;
        public TData? Data { get; set; }
        public string? Message { get; set; }

        [JsonIgnore]     
        public bool IsSuccess => _code is >= 200 and <= 299; // propriedade computada
    }
}
