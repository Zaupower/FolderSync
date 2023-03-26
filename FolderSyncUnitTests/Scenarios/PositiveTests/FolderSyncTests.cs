using FolderSync.LoggerC;
using FolderSync.Sync;
using NUnit.Framework;

namespace FolderSyncUnitTests.Scenarios.PositiveTests
{
    [TestFixture]
    internal class FolderSyncTests : SyncFoldersBaseTest
    {
        private FolderSyncRoutine folderSync = new FolderSyncRoutine();
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Sync_EqualContent_SourceAndReplica()
        {
            //Precondition
            var sourceDir = new DirectoryInfo(_source);
            sourceDir.CreateSubdirectory("s1");

            var replicaDir = new DirectoryInfo(_replica);
            sourceDir.CreateSubdirectory("s1");

            string sourceExpectedContent= File.ReadAllText(_source);
            string replicaExpectedContent = File.ReadAllText(_replica);
            //Action
            folderSync.SyncRoutineStart( _source, _replica, _logger);

            string sourceActualContent = File.ReadAllText(_source);
            string replicaActualContent = File.ReadAllText(_replica);
            //Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(sourceExpectedContent, replicaExpectedContent);
            });
        }
    }
}
