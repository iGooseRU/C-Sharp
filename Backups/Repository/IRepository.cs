using System.IO.Compression;
using Backups.Classes;

namespace Backups.Repository
{
    public interface IRepository
    {
        void MakeStorage(RestorePoint restorePoint, string archivePath);
        void SaveData(JobObject obj, ZipArchive zipArchive, string newFilePath);
    }
}