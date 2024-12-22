using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace laba12
{
    public class MDALog
    {
        public void WriteInFile(string text, string filePath = "mdalogfile.txt")
        {
            using StreamWriter writer = new StreamWriter(filePath, true);
            writer.WriteLine($"{DateTime.Now}: \n{text}");
            writer.Close();
        }
        public void ClearFile(string filePath)
        {
            using StreamWriter writer = new StreamWriter(filePath, false);
            writer.WriteLine("");
        }
        public void ReadFromFile(string filePath)
        {
            using StreamReader reader = new StreamReader(filePath);
            string line = reader.ReadToEnd();
            Console.WriteLine(line);
            reader.Close();
        }
        public void SearchInfo(string keyword, string filePath)
        {
            using StreamReader reader = new StreamReader(filePath);
            string line;
            bool found = false;
            while((line = reader.ReadLine()) != null)
            {
                if(line.Contains(keyword))
                {
                    Console.WriteLine(line);
                    found = true;
                }
            }
            if(!found)
            {
                Console.WriteLine("По заданному ключевому слову ничего не найдено");
            }
        }
    }
    public class MDADiskInfo
    {
        public void InfoAboutDrive(MDALog logger)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine($"Название диска: {drive.Name}");
                Console.WriteLine($"Тип диска: {drive.DriveType}");
                if (drive.IsReady)
                {
                    Console.WriteLine($"Объем диска: {drive.TotalSize}");
                    Console.WriteLine($"Свободное пространство на диске: {drive.TotalFreeSpace}");
                    Console.WriteLine($"Метка: {drive.VolumeLabel}");
                    Console.WriteLine($"Файловая система: {drive.DriveFormat}");
                    string logText =
                        $"Название диска: {drive.Name}\n" +
                        $"Тип диска: {drive.DriveType}\n" +
                        $"Объем диска: {drive.TotalSize}\n" +
                        $"Свободное пространство на диске: {drive.TotalFreeSpace}\n" +
                        $"Метка: {drive.VolumeLabel}\n" +
                        $"Файловая система: {drive.DriveFormat}\n";
                    logger.WriteInFile(logText);
                }
            }
        }
    }
    public class MDAFileInfo
    {
        public void FileInfo(string filePath, MDALog logger)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if(fileInfo.Exists)
            {
                Console.WriteLine($"Путь к файлу: {fileInfo.FullName}");
                Console.WriteLine($"Имя файла: {fileInfo.Name}");
                Console.WriteLine($"Размер файла: {fileInfo.Length}");
                Console.WriteLine($"Расширение файла: {fileInfo.Extension}");
                Console.WriteLine($"Дата создания файла: {fileInfo.CreationTime}");
                Console.WriteLine($"Дата изменения файла: {fileInfo.LastWriteTime}");
                string logText = 
                    $"Путь к файлу: {fileInfo.FullName}\n" +
                    $"Имя файла: {fileInfo.Name}\n" +
                    $"Размер файла: {fileInfo.Length}\n" +
                    $"Расширение файла: {fileInfo.Extension}\n" +
                    $"Дата создания файла: {fileInfo.CreationTime}\n" +
                    $"Дата изменения файла: {fileInfo.LastWriteTime}\n";
                logger.WriteInFile(logText);
            }
        }
    }
    public class MDADirInfo
    {
        public void PrintInfoAboutDirectories(MDALog logger, string directoryName = "D:\\")
        {
            DirectoryInfo dirInfo = new DirectoryInfo(directoryName);
            Console.WriteLine($"Время создания директория: {dirInfo.CreationTime}");
            Console.WriteLine($"Список родительских директориев: {dirInfo.Parent}");
            Console.WriteLine($"Количество файлов: {dirInfo.GetFiles().Length}");
            Console.WriteLine($"Количество поддиректориев: {dirInfo.GetDirectories().Length}");
            string logText =
                $"Время создания директория: {dirInfo.CreationTime}\n" +
                $"Список родительских директориев: {dirInfo.Parent}\n" +
                $"Количество файлов: {dirInfo.GetFiles().Length}\n" +
                $"Количество поддиректориев: {dirInfo.GetDirectories().Length}\n";
            logger.WriteInFile(logText);
        }
    }
    public class MDAFileManager
    {
        public void listWithFilesAndDirectories(string driveName)
        {
            if(DriveInfo.GetDrives().Any(d=> d.Name == driveName))
            {
                var dir = Directory.GetDirectories(driveName);
                var files = Directory.GetFiles(driveName);
                Directory.CreateDirectory("MDAInspect");
                var filePath = "MDAInspect/mdadirinfo.txt";
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("Директории: ");
                    foreach (var directories in dir)
                    {
                        writer.WriteLine(directories);
                    }
                    writer.WriteLine("Файлы: ");
                    foreach(var file  in files)
                    {
                        writer.WriteLine(file);
                    }
                }
                string copyFile = "MDAInspect/mdadirinfo_copy.txt";
                File.Copy(filePath, copyFile);
                File.Delete(filePath);
            }
            else
            {
                Console.WriteLine("Такого диска нет");
            }
        }
        public void CopyFilesWithExtension(string directory,  string extension)
        {
            Directory.CreateDirectory("MDAFiles");
            var files = Directory.GetFiles(directory, $"*{extension}");
            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                string newFile = Path.Combine("MDAFiles", fileName);
                File.Copy(file, newFile);
            }
            Directory.Move("MDAFiles", "MDAInspect/MDAFiles");
        }
        public void CreateAndExtractArchive()
        {
            string path = "MDAInspect/MDAFiles";
            string zipPath = "MDAInspect/MDAFiles.zip";
            string extractDir = "MDAExtract";
            ZipFile.CreateFromDirectory(path, zipPath);
            ZipFile.ExtractToDirectory(zipPath, extractDir);
        }
    }
    class Program
    {
        static void Main(string[] args )
        {
            MDALog logger = new MDALog();
            logger.ClearFile("mdalogfile.txt");

            MDADiskInfo diskInfo = new MDADiskInfo();
            diskInfo.InfoAboutDrive(logger);

            MDAFileInfo fileInfo = new MDAFileInfo();
            fileInfo.FileInfo("mdalogfile.txt", logger);

            MDADirInfo dirInfo = new MDADirInfo();
            dirInfo.PrintInfoAboutDirectories(logger, "D:\\Денис");

            MDAFileManager fileManager = new MDAFileManager();
            fileManager.listWithFilesAndDirectories("D:\\");
            fileManager.CopyFilesWithExtension("D:\\Виртуалка", ".mp4");
            fileManager.CreateAndExtractArchive();

            logger.SearchInfo("Fixed", "mdalogfile.txt");
        }
    }
}
