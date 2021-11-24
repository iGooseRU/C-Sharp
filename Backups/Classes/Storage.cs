using System.IO;

namespace Backups.Classes
{
    public class Storage
    {
        public Storage(string path)
        {
            FilePath = path;
        }

        public string FilePath { get; set; }
    }
}