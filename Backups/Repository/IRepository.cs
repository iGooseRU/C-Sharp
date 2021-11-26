using Backups.Classes;

namespace Backups.Repository
{
    public interface IRepository
    {
        void MakeStorage(RestorePoint restorePoint, string archivePath);
        void SingleStorageArchive(RestorePoint restorePoint);
        void SplitStorageArchive(RestorePoint restorePoint);
    }
}