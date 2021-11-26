using System.Reflection;
using Backups.Classes;
using Backups.Repository;

namespace Backups.ArchiverTools
{
    public interface IAlgorithm
    {
        void CreateArchive(RestorePoint restorePoint, IRepository repository);
    }
}