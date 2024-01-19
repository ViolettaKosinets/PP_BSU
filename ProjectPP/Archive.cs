using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPP
{
    public class Archive
    {
        public static void archive(string zipFile, string extractPath)
        {
            using (FileStream sourceStream = new FileStream(zipFile, FileMode.Open))
            {
                using (FileStream compressedStream = File.Create(extractPath))
                {
                    using (ZipArchive archive = new ZipArchive(compressedStream, ZipArchiveMode.Create))
                    {
                        ZipArchiveEntry entry = archive.CreateEntry(Path.GetFileName(zipFile));
                        using (Stream entryStream = entry.Open())
                        {
                            sourceStream.CopyTo(entryStream);
                        }
                    }
                }
            }
        }

        public static void unzip(string zipFilePath)
        {
            string extractPath = "C:\\Users\\Asus\\пп\\ProjectPP\\bin\\Debug\\net8.0-windows";
            ZipFile.ExtractToDirectory(zipFilePath, extractPath);
        }
    }
}
