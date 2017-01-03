namespace BankImporter.Core.Model
{
    public class ParseError
    {
        public int? Line { get; set; }
        public string ErrorMessage { get; set; }
    }
}