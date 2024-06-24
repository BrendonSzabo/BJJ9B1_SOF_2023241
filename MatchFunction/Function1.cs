using System;
using System.Net;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace MatchFunction
{
    public class Function1
    {
        [FunctionName("Match")]
        public void Run([TimerTrigger("0 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            WebClient wc = new WebClient();
            // SetMatch
            var SetMatchResult = wc.DownloadString("https://legendsoffantasy.azurewebsites.net/Home/Dashboard");
            var SetMatchesResult = wc.DownloadString("https://legendsoffantasy.azurewebsites.net/Home/Dashboard/SetMatches");
            var PlayMatchesResult = wc.DownloadString("https://legendsoffantasy.azurewebsites.net/Home/Dashboard/PlayMatches");
        }
    }
}
