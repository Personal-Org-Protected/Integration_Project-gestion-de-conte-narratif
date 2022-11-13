using Application.Common.Interfaces;
using Polly;
using Polly.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.RetryPolicies
{
    public class PolicyRegistries 
    {
        private static PolicyRegistry _policyRegistry  = new PolicyRegistry();
         static PolicyRegistries()
        {
            _policyRegistry.Add("RetryPolicyImageClient", Policy
                .HandleResult<HttpResponseMessage>(opt => opt.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));

            _policyRegistry.Add("CircuitBreakerImageClient", Policy
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .CircuitBreakerAsync(2, TimeSpan.FromSeconds(30)));
        }

        public static PolicyRegistry GetRegistries()
        {
           return _policyRegistry;
        }
    }
}
