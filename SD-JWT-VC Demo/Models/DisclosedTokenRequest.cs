namespace SD_JWT_VC_Demo.Models
{
    public class DisclosedTokenRequest
    {
        public string client_id { get; set; }
        public string nonce { get; set; }
        public Dcql_Query dcql_query { get; set; }
    }
}

public class Dcql_Query
{
    public Credential[] credentials { get; set; }
}

public class Credential
{
    public string id { get; set; }
    public string format { get; set; }
    public Meta meta { get; set; }
    public ClaimData[] claims { get; set; }
}

public class Meta
{
    public string[] vct_values { get; set; }
}

public class ClaimData
{
    public string[] path { get; set; }
}
