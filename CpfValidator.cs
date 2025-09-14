using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace CpfValidatorService
{
    public class CpfValidator
    {
        private readonly ILogger<CpfValidator> _logger;

        public CpfValidator(ILogger<CpfValidator> logger)
        {
            _logger = logger;
        }

        [Function("CpfValidator")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "validate/{cpf}")] HttpRequestData req,
            string cpf)
        {
            _logger.LogInformation($"C# HTTP trigger function processed a request to validate CPF: {cpf}");

            bool isValid = IsCpfValid(cpf);

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");
            
            var responseBody = new { cpf = cpf, isValid = isValid };

            await response.WriteStringAsync(System.Text.Json.JsonSerializer.Serialize(responseBody));

            return response;
        }

        public static bool IsCpfValid(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;
            var numbersOnly = new string(cpf.Where(char.IsDigit).ToArray());
            if (numbersOnly.Length != 11) return false;
            if (numbersOnly.All(c => c == numbersOnly[0])) return false;
            int sum = 0;
            for (int i = 0; i < 9; i++) sum += (numbersOnly[i] - '0') * (10 - i);
            int remainder = sum % 11;
            int verifier1 = (remainder < 2) ? 0 : 11 - remainder;
            if ((numbersOnly[9] - '0') != verifier1) return false;
            sum = 0;
            for (int i = 0; i < 10; i++) sum += (numbersOnly[i] - '0') * (11 - i);
            remainder = sum % 11;
            int verifier2 = (remainder < 2) ? 0 : 11 - remainder;
            return (numbersOnly[10] - '0') == verifier2;
        }
    }
}