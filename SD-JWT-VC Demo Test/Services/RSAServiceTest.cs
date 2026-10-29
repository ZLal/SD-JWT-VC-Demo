using SD_JWT_VC_Demo.Services;

namespace SD_JWT_VC_Demo_Test.Services
{
    public class RSAServiceTest
    {
        private static RSAService CreateRSAService()
        {
            UtilService utilService = new();
            return new RSAService(utilService);
        }

        [Fact]
        public void CreateNewKey_XML()
        {
            var rsaService = CreateRSAService();
            (string publicKey, string privateKey) = rsaService.CreateNewKey_XML();
            Assert.NotEmpty(publicKey);
            Assert.NotEmpty(privateKey);
        }

        [Fact]
        public void CreateNewKey_PEM()
        {
            var rsaService = CreateRSAService();
            (string publicKey, string privateKey) = rsaService.CreateNewKey_PEM();
            Assert.NotEmpty(publicKey);
            Assert.NotEmpty(privateKey);
        }
    }
}
