namespace BankImporter.Core.Tests.Importers.MbankTransferHistory
{
    using System.Linq;
    using Xunit;
    public class WhenParsingMBankTransferHistoryFile
    {
        [Fact]
        public void With_File_Without_Proper_History_Section_Then_Result_Success_False()
        {
            using (var fixture = new WhenParsingMBankTransferHistoryFileFixture())
            {
                //When
                var result = fixture.Sut.ParseHistoryFile("Invalid file content");

                //Then
                Assert.False(result.Success);
            }
        }

        [Fact]
        public void With_Valid_File_Then_Result_Success()
        {
            using (var fixture = new WhenParsingMBankTransferHistoryFileFixture())
            {
                //Given
                var fileContent = fixture.ValidMBankTransferHistoryFile;

                //When
                var result = fixture.Sut.ParseHistoryFile(fileContent);

                //Then
                Assert.True(result.Success);
            }
        }

        [Fact]
        public void With_Invalid_File_Then_Result_Success_False()
        {
            using (var fixture = new WhenParsingMBankTransferHistoryFileFixture())
            {
                //Given
                var fileContent = fixture.InvalidMBankTransferHistoryFile;

                //When
                var result = fixture.Sut.ParseHistoryFile(fileContent);

                //Then
                Assert.False(result.Success);
            }
        }

        [Fact]
        public void With_Valid_File_Then_Result_Contains_Proper_Number_Of_Items()
        {
            using (var fixture = new WhenParsingMBankTransferHistoryFileFixture())
            {
                //Given 
                var fileContent = fixture.ValidMBankTransferHistoryFile;

                //When
                var result = fixture.Sut.ParseHistoryFile(fileContent);

                //Then
                Assert.Equal(3, result.Items.Count());
            }
        }

        [Fact]
        public void With_Empty_File_Content_Then_Result_Success_False()
        {
            using (var fixture = new WhenParsingMBankTransferHistoryFileFixture())
            {
                //When
                var result = fixture.Sut.ParseHistoryFile(string.Empty);

                //Then
                Assert.False(result.Success);
            }
        }

        [Fact]
        public void With_Null_File_Content_Then_Result_Success_False()
        {
            using (var fixture = new WhenParsingMBankTransferHistoryFileFixture())
            {
                //When
                var result = fixture.Sut.ParseHistoryFile(null);

                //Then
                Assert.False(result.Success);
            }
        }
    }
}