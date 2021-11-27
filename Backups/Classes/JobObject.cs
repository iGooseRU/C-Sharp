using System.IO;

namespace Backups.Classes
{
    public class JobObject
    {
        public JobObject(FileInfo file)
        {
            File = file;
            FileName = file.Name;
            FilePath = Path.GetFullPath(FileName);
        }

        public FileInfo File { get; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}