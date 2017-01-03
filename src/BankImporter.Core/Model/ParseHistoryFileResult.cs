namespace BankImporter.Core.Model
{
    using System.Collections.Generic;
    using System.Linq;

    public class ParseHistoryFileResult
    {
        public ParseHistoryFileResult(string generalError, int? line = null)
        {
            Items = new List<HistoryItem>();
            Errors = new List<ParseError>()
            {
                new ParseError(){
                    ErrorMessage = generalError,
                    Line = line
                }
            };
        }

        public ParseHistoryFileResult()
        {
            Items = new List<HistoryItem>();
            Errors = new List<ParseError>();
        }

        public bool Success
        {
            get
            {
                return Errors == null || !Errors.Any();
            }
        }

        public IEnumerable<HistoryItem> Items { get; set; }

        public IEnumerable<ParseError> Errors { get; set; }
    }
}