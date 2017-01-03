namespace BankImporter.Core.Model
{
    using System;
    public class HistoryItem
    {
        // sender
        public string SenderOrReceiverAccount {get;set;}
        public string SenderOrReceiverName { get;set;}

        // transfer details
        public string Description { get; set; }
        public string TransferTitle { get; set; }
        public decimal TransferAmount { get; set; }
        public DateTime OperationDate { get; set; }
        public DateTime OperationConfirmationDate { get; set; }
        public string ProviderType { get; set; }
    }
}