using System.IO.Compression;
using Backups.Classes;
using Backups.Tools;

namespace Backups.ArchiverTools // New archives for every storage
{
    public class SplitStorage : IAlgorithm
    {
        public void CreateArchive(RestorePoint restorePoint)
        {
            foreach (JobObject obj in restorePoint.JobObjects)
            {
                string archivePath = restorePoint.RestorePointPath + $@"\{obj.FileName}.zip";
                using (ZipArchive zipArchive = ZipFile.Open(archivePath, ZipArchiveMode.Create))
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