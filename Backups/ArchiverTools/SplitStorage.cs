using System.IO.Compression;
using Backups.Classes;
using Backups.Repository;
using Backups.Tools;

namespace Backups.ArchiverTools
{
    public class SplitStorage : IAlgorithm
    {
        public void CreateArchive(RestorePoint restorePoint, IRepository repository)
        {
            repository.SplitStorageArchive(restorePoint);
        }
    }
}