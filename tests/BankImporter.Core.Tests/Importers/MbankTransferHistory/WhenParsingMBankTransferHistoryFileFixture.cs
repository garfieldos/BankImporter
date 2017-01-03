using System;
using System.IO;
using System.Reflection;
using BankImporter.Core.Services;

namespace BankImporter.Core.Tests.Importers.MbankTransferHistory
{
    public class WhenParsingMBankTransferHistoryFileFixture : IDisposable
    {
        public MbankTransferHistoryImporter Sut { get; private set; }

        public string ValidMBankTransferHistoryFile
        {
            get
            {
                return GetResourceFileContent("ValidMbankHistoryTransferFile.csv");
            }
        }

        public string InvalidMBankTransferHistoryFile
        {
            get
            {
                return GetResourceFileContent("InvalidMbankHistoryTransferFile.csv");
            }
        }

        public WhenParsingMBankTransferHistoryFileFixture()
        {
            Sut = new MbankTransferHistoryImporter();
        }

        private string GetResourceFileContent(string fileName)
        {
            var assembly = typeof(WhenParsingMBankTransferHistoryFileFixture).GetTypeInfo().Assembly;
            var resourceStream = assembly.GetManifestResourceStream($"BankImporter.Core.Tests.Importers.MbankTransferHistory.{fileName}");

            using (var s = new StreamReader(resourceStream))
            {
                return s.ReadToEnd();
            }
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~WhenImportingMBankTransferHistoryFileFixture() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}