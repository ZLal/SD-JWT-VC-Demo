using SD_JWT_VC_Demo.Models;
using SD_JWT_VC_Demo.Services;
using System.Text.Json;

namespace SD_JWT_VC_Demo_Test.Services
{
    public class HolderServiceTest
    {
        private DisclosedTokenRequest CreateDisclosedTokenRequest()
        {
            //UtilService utilService = new();
            //string nonce = utilService.GetRandomString(12);
            string nonce = "faj9Elvq0ZYq";
            string jsonTokenRequest = """
                {
                  "client_id": "https://verifier.example.com",
                  "nonce": "{nonce}",
                  "dcql_query": {
                    "credentials": [{
                      "id": "id_cred",
                      "format": "dc+sd-jwt",
                      "meta": { "vct_values": ["https://credentials.example.gov/identity"] },
                      "claims": [ { "path": ["{claim1}"] }, { "path": ["{claim2}"] } ]
                    }]
                  }
                }
                """;
            jsonTokenRequest = jsonTokenRequest
                .Replace("{nonce}", nonce)
                .Replace("{claim1}", "Name")
                .Replace("{claim2}", "AgeAbove18");
            DisclosedTokenRequest tokenRequest = JsonSerializer.Deserialize<DisclosedTokenRequest>(jsonTokenRequest)
                ?? throw new Exception("Failed to deserialize token request");
            return tokenRequest;
        }

        [Fact]
        public void CreateDisclosedTokenTest()
        {
            HolderService service = Shared.Services.GetHolderService();
            string issuerToken = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJ2Y3QiOiJodHRwczovL2NyZWRlbnRpYWxzLmV4YW1wbGUuY29tL2lkZW50aXR5X2NyZWRlbnRpYWwiLCJfc2QiOiJbN2IzMGNlNTVhZWI2MDYyOGI3NDY4Yjk5YWI3Mzc1YTBmMGRhMTgyZWM5ZTRmMDQ5NzAxYjJmZmJkMTY5YWE1YixlZGZhNDQzMmE0NWQ5ZjYzN2E3MzI0ZTEwNjg2MGI1NjRkYmQ3YTAzOTMxMTVlNDg1NmY1ZTQ0YzZhOThjNGNkLDhlYzkxZmYyNTFjNzE4YTFhYWVlZjc1MGVhMzI0ZmE3ZTUwMDAyMmVlYTI3N2ZjMTY5OTRiYWZmMGEyY2I0NWQsMTA4ODI4NmZmYzBlNTgzMWNmYjQxYTIzOTIwNmU1YTY2NTRmYmQ4NWU3NTQxYzcyNjVkMzYxYjU5MTU4NWY3NSw1OGJmZDM2MWIyZDMzYjViNzZmYmY4ZDE2NjZhODEzZjRlNDFmMjBmODJiNWUxMmY5Yjg0ZDcyNWNiYmJiNDg5XSIsIl9zZF9hbGciOiJzaGEtMjU2IiwiY25mIjoieyBcImp3a1wiOiB7IFwia3R5XCI6IFwiRUNcIiwgXCJjcnZcIjogXCJQLTI1NlwiLCBcInhcIjogXCJUQ0FFUjE5WnZ1M09IRjRqNFc0dmZTVm9ISVAxSUxpbERsczd2Q2VHZW1jXCIsIFwieVwiOiBcIlp4amlXV2JaTVFHSFZXS1ZRNGhiU0lpcnNWZnVlY0NFNnQ0alQ5RjJIWlFcIiB9IiwiZXhwIjoxODI0NzgyOTk4LCJpc3MiOiJUZXN0IElzc3VlciIsImF1ZCI6IlRlc3QgSXNzdWVyIEF1ZGllbmNlIn0.XzduzYnmOtQhw9lw3Y6Hp1y1W4NQgOVLghPQEGUjmeaxzHt-QxOrPyyU5xX58zPZLnzB4BjGnrFjcUwwrI2Kn07dlNflro5yfuImejO5SoqdZ_Z9pLQBegDMeTvv6mQJMBiZPTT3Znrl1usCxEAlyClhOtWyxWtMIT0jQ3Fe9quHVZzz7OPFmjMtEf94lHY0CP2UYbLe0UeF5H1NI0nGMWlnKW9Ph0-nXJfwxT-qmVOE1ywYqUAyrxc_AmHo40IOj2E3_-KfrzR6JM9_QgjlNrJZETtDUNqS92DZi7Kp1PUw6rUXg0Xxw_DktpA84qhJf8JzVC4xRhwJsDeRCAM4AA~WzdiMzBjZTU1YWViNjA2MjhiNzQ2OGI5OWFiNzM3NWEwZjBkYTE4MmVjOWU0ZjA0OTcwMWIyZmZiZDE2OWFhNWIsIE5hbWUsIEFtaXRoXQ==~W2VkZmE0NDMyYTQ1ZDlmNjM3YTczMjRlMTA2ODYwYjU2NGRiZDdhMDM5MzExNWU0ODU2ZjVlNDRjNmE5OGM0Y2QsIEFnZUFib3ZlMTgsIHRydWVd~WzhlYzkxZmYyNTFjNzE4YTFhYWVlZjc1MGVhMzI0ZmE3ZTUwMDAyMmVlYTI3N2ZjMTY5OTRiYWZmMGEyY2I0NWQsIEFnZUFib3ZlNTAsIGZhbHNlXQ==~WzEwODgyODZmZmMwZTU4MzFjZmI0MWEyMzkyMDZlNWE2NjU0ZmJkODVlNzU0MWM3MjY1ZDM2MWI1OTE1ODVmNzUsIElsbGVnYWwgRHJ1ZyB1c2VyLCB0cnVlXQ==~WzU4YmZkMzYxYjJkMzNiNWI3NmZiZjhkMTY2NmE4MTNmNGU0MWYyMGY4MmI1ZTEyZjliODRkNzI1Y2JiYmI0ODksIENvbnZpY3RlZCwgZmFsc2Vd~";
            var tokenRequest = CreateDisclosedTokenRequest();
            string token = service.CreateDisclosedToken(issuerToken, tokenRequest);
            Assert.False(string.IsNullOrWhiteSpace(token));
        }
    }
}
