namespace look.domain.dto.admin
{
    
    public class ConversionResult
    {
        public bool success { get; set; }
        public Query query { get; set; }
        public Info info { get; set; }
        public string date { get; set; }
        public double result { get; set; }
    }

    public class Query
    {
        public string from { get; set; }
        public string to { get; set; }
        public string amount { get; set; }
    }

    public class Info
    {
        public long timestamp { get; set; }
        public double rate { get; set; }
    }
}
