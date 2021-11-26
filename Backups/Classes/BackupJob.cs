using System.Collections.Generic;
using System.IO;
using Backups.ArchiverTools;
using Backups.Repository;
using Backups.Tools;

namespace Backups.Classes
{
    public class BackupJob
    {
        public BackupJob(string backupJobName, IAlgorithm algorithm, IRepository repository)
        {
            BackupJobName = backupJobName;
            BackupJobPath = CreateBackupJobFolder(backupJobName);
            Algorithm = algorithm;
            Repository = repository;

            JobObjects = new List<JobObject>();
            RestorePoints = new List<RestorePoint>();
        }

        public string BackupJobName { get; }
        public List<JobObject> JobObjects { get; }
        public List<RestorePoint> RestorePoints { get; }
        public IAlgorithm Algorithm { get; }
        public IRepository Repository { get; }
        public string BackupJobPath { get; set; }

        public string CreateBackupJobFolder(string backupJobName)
        {
            string path = @".\";
            string subPath = $@"{backupJobName}\";
            var dirInf = new DirectoryInfo(path);
            dirInf.CreateSubdirectory(subPath);

            return path + subPath;
        }

        public JobObject AddJobObject(JobObject obj)
        {
            if (obj == null)
                throw new BackupException("Invalid file");

            JobObjects.Add(obj);
            return obj;
        }

        public void RemoveJobObject(JobObject obj)
        {
            if (JobObjects.Find(o => o.FileName == obj.FileName) == null)
                throw new BackupException("Can not to find object in collection to remove");

            JobObjects.Remove(obj);
        }

        public RestorePoint CreateRestorePoint()
        {
            var restorePoint = new RestorePoint(this);
            restorePoint.RestorePointPath = restorePoint.CreateRestorePointFolder(restorePoint.RestorePointName);

            RestorePoints.Add(restorePoint);
            restorePoint.AddJobObjects(JobObjects);

            Algorithm.CreateArchive(restorePoint, Repository);
            return restorePoint;
        }
    }
}