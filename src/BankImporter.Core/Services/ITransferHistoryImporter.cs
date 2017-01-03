namespace BankImporter.Core.Services
{
    using BankImporter.Core.Model;

    public interface ITransferHistoryImporter
    {
        ParseHistoryFileResult ParseHistoryFile(string fileContent);
    }
}