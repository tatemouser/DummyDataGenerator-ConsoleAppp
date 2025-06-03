using Xunit;
using DummyDataGenerator.Services;

namespace DummyDataGenerator.Tests.Unit
{
    /// <summary>
    /// These tests make sure your preview and delete methods don't throw
    /// exceptions if folders or files are missing — useful for user-facing actions.
    /// </summary>
    public class FileOperationTests
    {
        [Fact]
        public void SqlDataDownloader_DeleteLatest_HandlesNonExistentFolder()
        {
            var downloader = new SqlDataDownloader();
            var exception = Record.Exception(() => downloader.DeleteLatest());

            Assert.Null(exception); // Passes if no exception is thrown
        }

        [Fact]
        public void SqlDataPreviewer_Preview_HandlesNonExistentFolder()
        {
            var previewer = new SqlDataPreviewer();
            var exception = Record.Exception(() => previewer.Preview());

            Assert.Null(exception); // Safe to call without files present
        }
    }
}
