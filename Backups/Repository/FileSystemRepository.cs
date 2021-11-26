using System.IO.Compression;
using Backups.Classes;

namespace Backups.Repository
{
    public class FileSystemRepository : IRepository
    {
        public void MakeStorage(RestorePoint restorePoint, string archivePath)
        {
            var storage = new Storage(archivePath);
            restorePoint.AddStorage(storage);
        }

        public void SaveData(JobObject obj, ZipArchive zipArchive, string newFilePath)
        {
            obj.FilePath = newFilePath;
            string pathToFileToAdd = obj.FilePath;
            string nameOfFileToAdd = obj.FileName;

            zipArchive.CreateEntryFromFile(pathToFileToAdd, nameOfFileToAdd);
        }
    }
}