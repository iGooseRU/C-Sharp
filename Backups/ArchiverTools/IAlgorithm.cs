using Backups.Classes;

namespace Backups.ArchiverTools
{
    public interface IAlgorithm
    {
        void CreateArchive(RestorePoint restorePoint);
    }
}