using System.IO.Compression;
using Backups.Classes;

namespace Backups.Repository
{
    public class VirtualRepository : IRepository
    {
        public void MakeStorage(RestorePoint restorePoint, string archivePath)
        {
            var storage = new Storage(archivePath);
            restorePoint.AddStorage(storage);
        }

        public void SaveData(JobObject obj, ZipArchive zipArchive, string newFilePath)
        {
            string pathToFileToAdd = obj.FilePath;
            string nameOfFileToAdd = obj.FileName;
        }
    }
}