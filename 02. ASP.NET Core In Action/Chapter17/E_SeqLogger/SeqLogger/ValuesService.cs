namespace SeqLogger
{
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;

    public class ValuesService
    {
        private readonly ILogger<ValuesService> _logger;

        public ValuesService(ILogger<ValuesService> logger)
        {
            this._logger = logger;
        }

        public IEnumerable<string> GetValues()
        {
            this._logger.LogInformation("Inside service, outside scope");

            using (this._logger.BeginScope(new Dictionary<string, object> { { "ScopeValue2", "inner scope" } }))
            {
                this._logger.LogInformation("Inside service, inside scope");
                return new string[] { "value1", "value2" };
            }
        }
    }
}
