using System.IO;
using System.IO.Compression;
using Backups.Classes;

namespace Backups.ArchiverTools // All storages in one archive
{
    public class SingleStorage : IAlgorithm
    {
        public void CreateArchive(RestorePoint restorePoint)
        {
            string archivePath = restorePoint.RestorePointPath + $@"\{restorePoint.RestorePointName}.zip";
            foreach (JobObject obj in restorePoint.JobObjects)
            {
                using (ZipArchive zipArchive = ZipFile.Open(archivePath, ZipArchiveMode.Update))
                {
                    string pathToFileToAdd = obj.FilePath;
                    string nameOfFileToAdd = obj.FileName;

                    zipArchive.CreateEntryFromFile(pathToFileToAdd, nameOfFileToAdd);
                }

                var storage = new Storage(archivePath);
                restorePoint.Storages.Add(storage);
            }
        }
    }
}