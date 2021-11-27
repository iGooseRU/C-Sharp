using System.IO;
using Backups.ArchiverTools;
using Backups.Classes;
using Backups.Repository;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupTest
    {
        private FileInfo _file1;
        private FileInfo _file2;
        private FileInfo _file3;

        [SetUp]
        public void Setup()
        {
            File.Create("FileA").Close();
            File.Create("FileB").Close();
            File.Create("FileC").Close();
            _file1 = new FileInfo("FileA");
            _file2 = new FileInfo("FileB");
            _file3 = new FileInfo("FileC");

        }

        [Test]
        public void VirtualSplitStorageTest()
        {
            var bJob = new BackupJob("firstTestJob", new SplitStorage(), new VirtualRepository());
            var firstObj = new JobObject(_file1);
            var secondObj = new JobObject(_file2);
            bJob.AddJobObject(firstObj);
            bJob.AddJobObject(secondObj);
            var firstRestorePoint = bJob.CreateRestorePoint();
            bJob.RemoveJobObject(secondObj);
            var secondRestorePoint = bJob.CreateRestorePoint();
            Assert.AreEqual(secondRestorePoint.Storages.Count, 1);
        }

        [Test]
        public void FileSystemSplitStorageTest()
        {
            var bJob = new BackupJob("secondTestJob", new SplitStorage(), new FileSystemRepository());
            var firstObj = new JobObject(_file1);
            var secondObj = new JobObject(_file2);
            bJob.AddJobObject(firstObj);
            bJob.AddJobObject(secondObj);
            var firstRestorePoint = bJob.CreateRestorePoint();
            bJob.RemoveJobObject(secondObj);
            var secondRestorePoint = bJob.CreateRestorePoint();
            Assert.AreEqual(secondRestorePoint.Storages.Count, 1);
        }
    }
}