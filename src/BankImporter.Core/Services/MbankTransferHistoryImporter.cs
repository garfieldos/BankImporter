namespace BankImporter.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using BankImporter.Core.Model;

    public class MbankTransferHistoryImporter : ITransferHistoryImporter
    {
        private static string _historySectionMarker = "#Data operacji;#Data księgowania;#Opis operacji;#Tytuł;#Nadawca/Odbiorca;#Numer konta;#Kwota;#Saldo po operacji;";

        private static string _historyEntryRegexPattern = @"([\d]{4}-[\d]{2}-[\d]{2});([\d]{4}-[\d]{2}-[\d]{2});([^;]*);([^;]*);([^;]*);([^;]*);([^;]*);([^;]*);";

        private readonly Regex _historyEntryRegex;

        public MbankTransferHistoryImporter()
        {
            _historyEntryRegex = new Regex(_historyEntryRegexPattern);
        }
        public ParseHistoryFileResult ParseHistoryFile(string fileContent)
        {
            var resultItems = new List<HistoryItem>();
            var errors = new List<ParseError>();

            if (string.IsNullOrWhiteSpace(fileContent))
            {
                return new ParseHistoryFileResult("Given file content is empty");
            }

            var historySectionIndex = fileContent.IndexOf(_historySectionMarker);

            if (historySectionIndex == -1)
            {
                return new ParseHistoryFileResult("There is no history section marker");
            }

            var transferHistorySection = fileContent.Substring(historySectionIndex);
            var transferHistoryLines = transferHistorySection.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            var lineOffset = 1;

            var transactionHistoryItems = new List<HistoryItem>();

            for (var transferHistoryItemIndex = 1; transferHistoryItemIndex < transferHistoryLines.Length; transferHistoryItemIndex++)
            {
                var currentItem = transferHistoryLines[transferHistoryItemIndex];

                if (string.IsNullOrWhiteSpace(currentItem))
                {
                    break;
                }

                var regexMatch = _historyEntryRegex.Match(currentItem);

                if (!regexMatch.Success)
                {
                    return new ParseHistoryFileResult("Invalid transaction history format", transferHistoryItemIndex + lineOffset);
                }

                try
                {
                    transactionHistoryItems.Add(GetItemFromRegexMatch(regexMatch));
                }
                catch
                {
                    return new ParseHistoryFileResult("Invalid transaction history format", transferHistoryItemIndex + lineOffset);
                }
            }

            return new ParseHistoryFileResult()
            {
                Items = transactionHistoryItems
            };
        }

        private HistoryItem GetItemFromRegexMatch(Match match)
        {
            var result = new HistoryItem();

            result.OperationDate = DateTime.Parse(CleanCsvString(match.Groups[1].Value));
            result.OperationConfirmationDate = DateTime.Parse(CleanCsvString(match.Groups[2].Value));
            result.Description = CleanCsvString(match.Groups[3].Value);
            result.TransferTitle = CleanCsvString(match.Groups[4].Value);
            result.SenderOrReceiverName = CleanCsvString(match.Groups[5].Value);
            result.SenderOrReceiverAccount = CleanCsvString(match.Groups[6].Value);

            // we dont need to parse group[7] - bank account balance

            result.TransferAmount = decimal.Parse(CleanCsvString(match.Groups[8].Value), System.Globalization.NumberStyles.Any);

            return result;
        }

        private string CleanCsvString(string inputString)
        {
            var trimCharacters = new char[] { '\'', '"' };
            return inputString.Trim(trimCharacters);
        }
    }
}