using Microsoft.Extensions.Options;
using SD_JWT_VC_Demo.Models;
using SD_JWT_VC_Demo.Services;

namespace SD_JWT_VC_Demo_Test.Shared
{
    public class Services
    {
        private static int GetMinutesInYear()
        {
            TimeSpan yearTime = new(365, 0, 0, 0);
            return Convert.ToInt32(yearTime.TotalMinutes);
        }
        public static IOptions<DemoOptions> GetOptions()
        {
            int yearMinutes = GetMinutesInYear();
            IOptions<DemoOptions> options = Options.Create(new DemoOptions()
            {
                Issuer = new Issuer()
                {
                    IssuerName = "Test Issuer",
                    AudienceName = "Test Issuer Audience",
                    ValidityInMinutes = yearMinutes,
                    PublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA49zksPm3aRWTA6RXZTVT90KlGeCAEhM7\r\nJqSTH6inCB0iv/P/jZ5fx5IAJjRgr+CoJxKYbwRqLCv3fRXQpiYZz0aG9m0njhSHC7AI3ryHQFqm\r\njvQbOq1b2CI+7WA1v28qDpUqcpipZ/k4ZtzYYPEUBNQuFvRLZBSZ1pMsui1lEhJcgkvfldUkglUI\r\nXXpX+tdCcQl/Ik6le8AbKa8qD13gtXP5RqvZBV5aRADOXNtZLe4GSWB4c+W5T90AWovx4VaE/Ltf\r\npVi+6IOsiOSKqhhPiwrslTrF7Yl76AaLQVStdIDn05lvwkjm5JmUNcO83fvF745O2Ug+8NBr81E8\r\nCuiWsQIDAQAB",
                    PrivateKey = "MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDj3OSw+bdpFZMDpFdlNVP3QqUZ\r\n4IASEzsmpJMfqKcIHSK/8/+Nnl/HkgAmNGCv4KgnEphvBGosK/d9FdCmJhnPRob2bSeOFIcLsAje\r\nvIdAWqaO9Bs6rVvYIj7tYDW/byoOlSpymKln+Thm3Nhg8RQE1C4W9EtkFJnWkyy6LWUSElyCS9+V\r\n1SSCVQhdelf610JxCX8iTqV7wBspryoPXeC1c/lGq9kFXlpEAM5c21kt7gZJYHhz5blP3QBai/Hh\r\nVoT8u1+lWL7og6yI5IqqGE+LCuyVOsXtiXvoBotBVK10gOfTmW/CSObkmZQ1w7zd+8Xvjk7ZSD7w\r\n0GvzUTwK6JaxAgMBAAECggEBAMZlgGFvK/i9JiIXp1ih1BgH2QTs6dXTFL+Sr97y02xHIgiMBEhk\r\nSvTS9HpPRJgePYq3sC2JZIBNT9GDewYPSo2k1MIfTD9zNBpDDZYSBJlN1JPHiyXN55gKSUwZZvLl\r\ncXL9AdaBgY7b+fXl+Ch//3Qs4mxuDG55WXDw+BKqj8Pov+4GXaJ1Pg/QTgQ4e6kZJSOXN8syudGr\r\nX/8PTUBNMcXf+Lunj4gyzQc727o5zTGmpbEL4IOfqhoPmKW9yyh2pIhXDn8XbNmQIvmAT2F2ck23\r\nA3CYxyT6Ko+u/SK2GvdQSdksYlXBN8T9UPnJ1qeopyoI/+/AJvZiqQGQ0V1slTkCgYEA6KQ2Ccdj\r\nPD+ZwHLdC/4k4eN7U7bwzzDBkeJWmGpvYPcAwbBYPSG9QMLi7OJPe/cK7ChLPd2r/wqEKrYO9w2x\r\nJLIlpjxpsC3EXI9Wby+dRewixyvixcFq5bJa0Nk5YG5l2XfFlMg3FlktBgAIWWGfwouZoNHrkxg6\r\nZ5qFz2MolJMCgYEA+r3aooYVzyZ73yPSdejPRvNXC0uXhuaX6sGQ87Mdj3otjzLDpUssNz1eb10S\r\nufH38tbTqtZlvWvpuz9x1zQ4U5TjvPQVLssghSql3z7pYoV/zCciHKA4ZPD1vYDrZ/QZ0AlpjJBW\r\nW/PVnfFrUHT6XmElhvw1YrdVuveV0T9BFisCgYBZO/OGWLaVu4eMLALhwMAA7TK276r6ribxgDHb\r\nEKhcNyJRKQUPnctr9dT5K+11DVNhNit2NXrtvujnQSIIXXYfLz4Fv5KCg2uYXVq4FzlqQqMsZob7\r\nd687H+h5x3icpnvmqGxfH1UR+26rqIOLVUjkZIuvBhYoO9jbOIgu/kfohQKBgALnZfvR37jJck/Q\r\nQ2S4MIZa1Rw9YODgFMxUN5CK94Gs4ZAx/1APwdBV9p0Fj+z1X3DBiiwFVaJO+KMBhhj+8tQM+mI6\r\n1W/4TEUejFM8kEli0GsV3PBF7sFEhSQPtSo8Cnx9T2qgVU+1UDkHDb4dzLSBHYUlsJyXEPgcW/DT\r\ncbaHAoGAZNTz12eeEaW5Yvb7NYvlhmyCLgKTX6x5zDmKCbbe3auUyrU5/wp2bHS4ohRwGcqWg/qR\r\nDXK+5mPAZOvQT7+ALEsQe2fvD9CQ1iAbD79YesAwuFdhzJ74XijLrPBap1V5qfwaQwnS+C08sbhi\r\nXdLPRczVsK/8TwuQWuSOTp2dxkI="
                },
                Holder = new Holder()
                {
                    IssuerName = "Test Holder",
                    AudienceName = "Test Holder Audience",
                    ValidityInMinutes = yearMinutes,
                    PublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAzzOu1vC1zDwyg4jPfeC8njcv0ksJkcGU+/kM8RUkqwWUnH9wlzbNDiCsLCrNdBtmlerG/vH/q9GWHNnum/hWJ4d3k/4FNoEsEdb6n0vake6jk9dMOKcxjvd2JoB4VzSVsS45IlMtwOKOayaRHc+p2azOeT+kD9LMzsXFVnXwpZdyMyewvsVvb64mDR/i35OLb2KglEFWNUtWyudhvzudYqy1ADzNfV4x0SS53PmfBMKg25ceaiOIsCIfdOhRgR5RMb4xSD0GjHElk1vQ6YuJ0qgWyWCwvaQqrnBka2tZnLAmBCwSkrnhKhYM0X9VHb3Ba4hphDs/psVG9EDqYA1mlQIDAQAB",
                    PrivateKey = "MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQDPM67W8LXMPDKDiM994LyeNy/SSwmRwZT7+QzxFSSrBZScf3CXNs0OIKwsKs10G2aV6sb+8f+r0ZYc2e6b+FYnh3eT/gU2gSwR1vqfS9qR7qOT10w4pzGO93YmgHhXNJWxLjkiUy3A4o5rJpEdz6nZrM55P6QP0szOxcVWdfCll3IzJ7C+xW9vriYNH+Lfk4tvYqCUQVY1S1bK52G/O51irLUAPM19XjHRJLnc+Z8EwqDblx5qI4iwIh906FGBHlExvjFIPQaMcSWTW9Dpi4nSqBbJYLC9pCqucGRra1mcsCYELBKSueEqFgzRf1UdvcFriGmEOz+mxUb0QOpgDWaVAgMBAAECggEATGjayRjrce2hEzCYk+sUo6PmVr6lQMMKlgE57I3Hf+oG689zTdA/vj3+QCtoqLCSHCcAS3QivNIFcY5Ml+f0nY8vYbwDYB4yikGlGql2aOqAGD0FNr8xX2+SepBV0JLrwPMyzp2t+buSmaEXUs8J6+P2IHB/p/Ys4PO92iyUGfwdU6nJxjKNelultC6PQX2fQg1LHVdHqt4QGlSQ5jLcckCdJeOEqG647z5jiMSnr1dLaTV+tzd+oKI6H66/NwR/6QLGjqdntPljFeqPvv6vT1Sa/UAmF6WD4D4SEXJbSZarIurdQZJFgHn1/Y8s9IpChk4vC8ytwY9cAvbjln5V4QKBgQDZeHQMzCjnzCYL656XLhKSaBwWZD8fdVDex3y9p0lw4N9e9/H77mhVCLAI+d3ZTD1H8XichMl2+XSAN7rk84SZakbYJPDexFh6wKtr3FBSc12o7Niy2FkohamQoK6AVDV2U9QmlnhWxthL3h+B/+slLh2MYclOIcqcdHWXDgjhnwKBgQDz6XzXNzSDxgJL9bvmQkXmpKyug14al+ubmZyEy44nB42eKYDbF5xHEZf/iPFlwKqwRrsF9LVxITACBph2aBsgoQzrTKwZPqLguaGBVqIGKMLnsriY4ziVH1mSocLmYEU//AmevXf0HUBlmxrnf+AYCg6xz7MB5Ji7bDBELfuTSwKBgA5UD2Fgejl7i+4xIgvX70VSMreO91G9SwzCnAdq5D3Zsx3wvDLbLGid3+tXezR7/3WkBUxau2NpYDGqelfna48oH27SUER9L083LpQ2udwU2dDtegGem5E97UQoMhcmdTdMoqfANkGRbGLjSbQpBnc2qJ+9OaQ6Ob1BNxYyyIcFAoGBAJh+rEyp+S1ByQHQuw9089sFrejVvSoHPYjk+1STJ2cDc+kkralNE6njOBLNfY9Oa+kV76wDmMUIKMm7cerln8BAsKEUKK0H2jGCHZWNonMTXPgbZwo/VYr2NLxWYlJCcklUvHcAolR/TVm5JZMDAgpFj3ZgEtzmHQ9gzrRxw04NAoGBAKE3+SBqiOP+51Zxr8T2Cnr4URD5KNZkhhCI+cPq5ATHoOf9SOMaWryRG344KzwmzABOBoxi+uvJ2px/8lYoA9iYGhvV2yesB43Vqh0G83wBmaAfE6/EXaOVjnSUvIp8yAkzlNs+udRcXW50J9j8fVHgM3DxEY4oEmrKpS7wa0Gu"
                }
            });
            return options;
        }

        public static IssuerService GetIssuerService()
        {
            var options = GetOptions();
            UtilService utilService = new();
            JwtService jwtService = new();
            IssuerService service = new(options, utilService, jwtService);
            return service;
        }

        public static HolderService GetHolderService()
        {
            var options = GetOptions();
            UtilService utilService = new();
            JwtService jwtService = new();
            HolderService service = new(options, utilService, jwtService);
            return service;
        }

        public static VerifierService GetVerifierService()
        {
            var options = GetOptions();
            UtilService utilService = new();
            JwtService jwtService = new();
            VerifierService service = new(options, utilService, jwtService);
            return service;
        }
    }
}
