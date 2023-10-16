namespace TestProduct.Model
{
    public class RequestMessage
    {
        public string? Comment { get; set; }
        public DateTime DateComment { get; set; }
        public int id { get; set; }
        public int idRequestRepair { get; set; }
        public int? idWriter { get; set; }
        public bool? Type { get; set; }
        public string? FIO { get; set; }
    }
}
