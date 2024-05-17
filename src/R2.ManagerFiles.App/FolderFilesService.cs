using Microsoft.Extensions.Configuration;

namespace R2.ManagerFiles.App
{
    public class FolderFilesService
    {
        public IConfiguration Config { get; private set; }
        public string[] WatchedFolders { get; private set; }
        public string[] FoldersToBeDeleted { get; private set; }
        public string[] FilesToBeDeleted { get; private set; }

        public int DeletedFoldersQuantity { get; private set; }
        public int DeletedFilesQuantity { get; private set; }

        public FolderFilesService(IConfiguration _config)
        {
            Config = _config;

            WatchedFolders = Config.GetSection("WatchedFolders").Get<string[]>();
            FilesToBeDeleted = Config.GetSection("FilesToBeDeleted").Get<string[]>();
            FoldersToBeDeleted = Config.GetSection("FoldersToBeDeleted").Get<string[]>();

            if (WatchedFolders == null || WatchedFolders.Length == 0)
                throw new Exception("WatchedFolders is required");

            if (FilesToBeDeleted == null || FilesToBeDeleted.Length == 0)
                throw new Exception("FilesToBeDeleted is required");

            if (FoldersToBeDeleted == null || FoldersToBeDeleted.Length == 0)
                throw new Exception("FoldersToBeDeleted is required");

            DeletedFoldersQuantity = 0;
            DeletedFilesQuantity = 0;
        }

        public SearchInFolderDto SearchInFolders()
        {

            SearchInFolderDto result = new SearchInFolderDto();

            foreach (string folder in WatchedFolders)
            {
                if(folder == "c:" || folder == "c:\\" || folder == "d:" || folder == "d:\\")
                    throw new Exception($"You can run on c: or d: root directory");

                if (!Directory.Exists(folder))
                    throw new Exception($"{folder} not exists");

                var dir = new DirectoryInfo(folder);
                result = DeleteFolderAndFiles(folder);

                foreach (var subDir1 in dir.GetDirectories())
                {
                    var dir1 = new DirectoryInfo(subDir1.FullName);
                    result = DeleteFolderAndFiles(subDir1.FullName);

                    foreach (var subDir2 in subDir1.GetDirectories())
                    {
                        var dir2 = new DirectoryInfo(subDir2.FullName);
                        result = DeleteFolderAndFiles(subDir2.FullName);

                        foreach (var subDir3 in subDir2.GetDirectories())
                        {
                            var dir3 = new DirectoryInfo(subDir3.FullName);
                            result = DeleteFolderAndFiles(subDir3.FullName);

                            foreach (var subDir4 in subDir3.GetDirectories())
                            {
                                var dir4 = new DirectoryInfo(subDir4.FullName);
                                result = DeleteFolderAndFiles(subDir4.FullName);

                                foreach (var subDir5 in subDir4.GetDirectories())
                                {
                                    var dir5 = new DirectoryInfo(subDir5.FullName);
                                    result = DeleteFolderAndFiles(subDir5.FullName);
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        private SearchInFolderDto DeleteFolderAndFiles(string folder)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"SET FOLDER {folder}");

            var dir = new DirectoryInfo(folder);

            //SUBFOLDER1
            foreach (var subFolder1 in dir.GetDirectories())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"SET SUBFOLDER {subFolder1.FullName}");
                var subDir1 = new DirectoryInfo(subFolder1.FullName);

                if (FoldersToBeDeleted.Any(subDir1.Name.Contains))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"DELETED SUBFOLDER {subDir1.FullName}");
                    subDir1.Delete(true);
                    DeletedFoldersQuantity++;
                }
                else
                {
                    foreach (var file in subDir1.GetFiles())
                    {
                        if (FilesToBeDeleted.Any(file.Name.Contains))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"DELETED FILE {file.FullName}");
                            File.Delete(file.FullName);
                            DeletedFilesQuantity++;
                        }
                    }
                }
            }

            foreach (var file in dir.GetFiles())
            {
                if (FilesToBeDeleted.Any(file.Name.Contains))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"DELETED FILE {file.FullName}");
                    File.Delete(file.FullName);
                    DeletedFilesQuantity++;
                }
            }

            return new SearchInFolderDto(DeletedFoldersQuantity, DeletedFilesQuantity);
        }
    }
}
