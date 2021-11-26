using System.IO;
using System.IO.Compression;
using Backups.Classes;
using Backups.Repository;

namespace Backups.ArchiverTools
{
    public class SingleStorage : IAlgorithm
    {
        public void CreateArchive(RestorePoint restorePoint, IRepository repository, string filePath)
        {
            string archivePath = restorePoint.RestorePointPath + $@"\{restorePoint.RestorePointName}.zip";
            foreach (JobObject obj in restorePoint.JobObjects)
            {
                using (ZipArchive zipArchive = ZipFile.Open(archivePath, ZipArchiveMode.Update))
                {
                    repository.SaveData(obj, zipArchive, filePath);
                }

                repository.MakeStorage(restorePoint, archivePath);
            }
        }
    }
}