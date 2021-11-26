using System.IO.Compression;
using Backups.Classes;

namespace Backups.Repository
{
    public class FileSystemRepository
    {
        public void MakeStorage(RestorePoint restorePoint, string archivePath)
        {
            var storage = new Storage(archivePath);
            restorePoint.Storages.Add(storage);
        }

        public void SingleStorageArchive(RestorePoint restorePoint, string newObjPath)
        {
            string archivePath = restorePoint.RestorePointPath + $@"\{restorePoint.RestorePointName}.zip";
            foreach (JobObject obj in restorePoint.JobObjects)
            {
                obj.FilePath = newObjPath;
                using (ZipArchive zipArchive = ZipFile.Open(archivePath, ZipArchiveMode.Update))
                {
                    string pathToFileToAdd = obj.FilePath;
                    string nameOfFileToAdd = obj.FileName;

                    zipArchive.CreateEntryFromFile(pathToFileToAdd, nameOfFileToAdd);
                }

                MakeStorage(restorePoint, archivePath);
            }
        }

        public void SplitStorageArchive(RestorePoint restorePoint, string newObjPath)
        {
            foreach (JobObject obj in restorePoint.JobObjects)
            {
                string archivePath = restorePoint.RestorePointPath + $@"\{obj.FileName}.zip";
                obj.FilePath = newObjPath;

                using (ZipArchive zipArchive = ZipFile.Open(archivePath, ZipArchiveMode.Create))
                {
                    string pathToFileToAdd = obj.FilePath;
                    string nameOfFileToAdd = obj.FileName;

                    zipArchive.CreateEntryFromFile(pathToFileToAdd, nameOfFileToAdd);
                }

                MakeStorage(restorePoint, archivePath);
            }
        }
    }
}