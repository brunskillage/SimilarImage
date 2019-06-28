using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using SimilarImage.Models;

namespace SimilarImage.Services
{
    public class FileSystemService
    {
        private readonly ConfigService _configService;

        public FileSystemService(ConfigService configService)
        {
            _configService = configService;
        }

        public void EnsureFileDirectory(string filePath)
        {
            if (filePath == null)
                return;
            new FileInfo(filePath + _configService.DupesDirName).Directory.Create();
        }

        public void MoveFile(string sourceFullname, string destFolder)
        {
            if (sourceFullname == null || !File.Exists(sourceFullname))
                return;
            EnsureFileDirectory(destFolder);
            File.Move(sourceFullname, destFolder);
        }

        public string GetFileHash(string filePath)
        {
            if (filePath == null || !File.Exists(filePath))
                return string.Empty;
            using (var stream = new BufferedStream(File.OpenRead(filePath), 1200000))
            {
                var sha = new SHA256Managed();
                var checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", string.Empty);
            }
        }

        public long GetFileLength(string filePath)
        {
            if (filePath == null || !File.Exists(filePath))
                return 0;
            return new FileInfo(filePath).Length;
        }

        public DateTime GetDateCreated(string filePath)
        {
            if (filePath == null || !File.Exists(filePath))
                return DateTime.MinValue;
            return new FileInfo(filePath).CreationTime;
        }

        public bool FileExists(string file)
        {
            return File.Exists(file);
        }

        public string GetText(string file)
        {
            return File.ReadAllText(file, Encoding.UTF8);
        }

        public void Save(string file, string text)
        {
            File.WriteAllText(file, text);
        }
    }
}