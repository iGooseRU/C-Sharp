using System;
using System.Collections.Generic;
using System.IO;
using Backups.ArchiverTools;

namespace Backups.Classes
{
    public class RestorePoint
    {
        private static int _restorePointNum = 0;
        public RestorePoint(BackupJob bJob)
        {
            _restorePointNum++;

            RestorePointName = "RestorePoint_" + _restorePointNum.ToString();
            CreatingTime = DateTime.Now;

            Storages = new List<Storage>();
            JobObjects = new List<JobObject>();

            RestorePointBackupJob = bJob;
        }

        public string RestorePointName { get; }
        public string RestorePointPath { get; set; }
        public BackupJob RestorePointBackupJob { get; }
        public DateTime CreatingTime { get; }
        public List<JobObject> JobObjects { get; }
        public List<Storage> Storages { get; }

        public void AddJobObjects(IEnumerable<JobObject> objects)
        {
            JobObjects.AddRange(objects);

            foreach (JobObject obj in JobObjects)
            {
                obj.FileName += '_' + _restorePointNum.ToString();
            }
        }

        public string CreateRestorePointFolder(string restorePointName)
        {
            string path = RestorePointBackupJob.BackupJobPath;
            string subPath = $@"{restorePointName}\";
            var dirInf = new DirectoryInfo(path);
            dirInf.CreateSubdirectory(subPath);

            return path + subPath;
        }

        public void AddStorage(Storage storage)
        {
            Storages.Add(storage);
        }
    }
}