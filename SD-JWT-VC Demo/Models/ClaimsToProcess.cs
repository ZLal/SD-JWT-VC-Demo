namespace SD_JWT_VC_Demo.Models
{
    public class ClaimsToProcess
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Salt { get; set; }
        public string Hash { get; set; }

        public string GetSaltedString() => $"[{Salt}, {Key}, {Value}]";
        public string GetHashedString() => $"[{Hash}, {Key}, {Value}]";
    }
}
