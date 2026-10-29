using SD_JWT_VC_Demo.Services;

namespace SD_JWT_VC_Demo_Test.Services
{
    public class IssuerServiceTest
    {
        [Fact]
        public void CreateTokenWithDisclosedDataTest()
        {
            IssuerService issuerService = Shared.Services.GetIssuerService();
            Dictionary<string, string> privateData = IssuerService.CreateHolderPrivateData();
            string issuedTokenWithDisclosedData = issuerService.CreateTokenWithDisclosedData(privateData);

            Assert.False(string.IsNullOrWhiteSpace(issuedTokenWithDisclosedData));
        }
    }
}
