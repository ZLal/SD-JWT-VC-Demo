using SD_JWT_VC_Demo.Services;

namespace SD_JWT_VC_Demo_Test.Services
{
    public class VerifierServiceTest
    {
        [Fact]
        public void ValidateTokenAndGetClaimsTest()
        {
            VerifierService service = Shared.Services.GetVerifierService();
            string nonce = "faj9Elvq0ZYq";
            string holderToken = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJ2Y3QiOiJodHRwczovL2NyZWRlbnRpYWxzLmV4YW1wbGUuY29tL2lkZW50aXR5X2NyZWRlbnRpYWwiLCJfc2QiOiJbN2IzMGNlNTVhZWI2MDYyOGI3NDY4Yjk5YWI3Mzc1YTBmMGRhMTgyZWM5ZTRmMDQ5NzAxYjJmZmJkMTY5YWE1YixlZGZhNDQzMmE0NWQ5ZjYzN2E3MzI0ZTEwNjg2MGI1NjRkYmQ3YTAzOTMxMTVlNDg1NmY1ZTQ0YzZhOThjNGNkLDhlYzkxZmYyNTFjNzE4YTFhYWVlZjc1MGVhMzI0ZmE3ZTUwMDAyMmVlYTI3N2ZjMTY5OTRiYWZmMGEyY2I0NWQsMTA4ODI4NmZmYzBlNTgzMWNmYjQxYTIzOTIwNmU1YTY2NTRmYmQ4NWU3NTQxYzcyNjVkMzYxYjU5MTU4NWY3NSw1OGJmZDM2MWIyZDMzYjViNzZmYmY4ZDE2NjZhODEzZjRlNDFmMjBmODJiNWUxMmY5Yjg0ZDcyNWNiYmJiNDg5XSIsIl9zZF9hbGciOiJzaGEtMjU2IiwiY25mIjoieyBcImp3a1wiOiB7IFwia3R5XCI6IFwiRUNcIiwgXCJjcnZcIjogXCJQLTI1NlwiLCBcInhcIjogXCJUQ0FFUjE5WnZ1M09IRjRqNFc0dmZTVm9ISVAxSUxpbERsczd2Q2VHZW1jXCIsIFwieVwiOiBcIlp4amlXV2JaTVFHSFZXS1ZRNGhiU0lpcnNWZnVlY0NFNnQ0alQ5RjJIWlFcIiB9IiwiZXhwIjoxODI0NzgyOTk4LCJpc3MiOiJUZXN0IElzc3VlciIsImF1ZCI6IlRlc3QgSXNzdWVyIEF1ZGllbmNlIn0.XzduzYnmOtQhw9lw3Y6Hp1y1W4NQgOVLghPQEGUjmeaxzHt-QxOrPyyU5xX58zPZLnzB4BjGnrFjcUwwrI2Kn07dlNflro5yfuImejO5SoqdZ_Z9pLQBegDMeTvv6mQJMBiZPTT3Znrl1usCxEAlyClhOtWyxWtMIT0jQ3Fe9quHVZzz7OPFmjMtEf94lHY0CP2UYbLe0UeF5H1NI0nGMWlnKW9Ph0-nXJfwxT-qmVOE1ywYqUAyrxc_AmHo40IOj2E3_-KfrzR6JM9_QgjlNrJZETtDUNqS92DZi7Kp1PUw6rUXg0Xxw_DktpA84qhJf8JzVC4xRhwJsDeRCAM4AA~WzdiMzBjZTU1YWViNjA2MjhiNzQ2OGI5OWFiNzM3NWEwZjBkYTE4MmVjOWU0ZjA0OTcwMWIyZmZiZDE2OWFhNWIsIE5hbWUsIEFtaXRoXQ==~W2VkZmE0NDMyYTQ1ZDlmNjM3YTczMjRlMTA2ODYwYjU2NGRiZDdhMDM5MzExNWU0ODU2ZjVlNDRjNmE5OGM0Y2QsIEFnZUFib3ZlMTgsIHRydWVd~eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJub25jZSI6ImZhajlFbHZxMFpZcSIsInNkX2hhc2giOiJiODllZjI4NDU5YjZmMzU0NjMwYTI5MjZmOTNiN2YxYjMzOGE5MTFhNGQ0M2Q0MDgxMDhmYWI1YWY2ZDc2ZTljIiwiZXhwIjoxODI0NzgzODA0LCJpc3MiOiJUZXN0IEhvbGRlciIsImF1ZCI6IlRlc3QgSG9sZGVyIEF1ZGllbmNlIn0.lCUCsoVRNHi-KIstXMHEEaFs7VYKUHjFTQJioJrwmXcAhAmm9wDGPHG6nYOQ6qu5uvcX5yKSqUmakND1hSRrlh8a6Ob62ABS21p1NO1c8gzlcKBiXn_-D1uaP1ihnEaLfjZvmpi1nfekKa1GvEFY9WV9mMqTnUrGISCMf8hTCwZcx-Mjx934aimGDz9sAzSGEXsWxkx7lZ5FKf9hQHwumbUP7EmzQnq01k26HB8uugs5IkJQ8E26aHWWxIX9y_Xgi-1EBB7X_P1GEgTEq9tZ5LI8CRDueSN7HClrFjzWpwtqmKG-kw0JVrLAIElLiLKGKevtWBTTCHkiP96bbJNEcA";
            var claims = service.ValidateTokenAndGetClaims(holderToken, nonce);
            
            Assert.NotEmpty(claims);
            Assert.True(claims.ContainsKey("Name"));
            Assert.Equal("Amith", claims["Name"]);
            Assert.True(claims.ContainsKey("AgeAbove18"));
            Assert.Equal("true", claims["AgeAbove18"]);
        }
    }
}
