using System.IO.Compression;
using Backups.Classes;
using Backups.Repository;
using Backups.Tools;

namespace Backups.ArchiverTools
{
    public class SplitStorage : IAlgorithm
    {
        public void CreateArchive(RestorePoint restorePoint, IRepository repository, string filePath)
        {
            foreach (JobObject obj in restorePoint.JobObjects)
            {
                string archivePath = restorePoint.RestorePointPath + $@"\{obj.FileName}.zip";
                using (ZipArchive zipArchive = ZipFile.Open(archivePath, ZipArchiveMode.Create))
                {
                    repository.SaveData(obj, zipArchive, filePath);
                }

                repository.MakeStorage(restorePoint, archivePath);
            }
        }
    }
}