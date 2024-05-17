using Microsoft.Extensions.Configuration;
using R2.ManagerFiles.App;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            var builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();

            var folderFileService = new FolderFilesService(config);

            ConsoleWriteUtil.Write("Watched folders:");

            foreach (var watchedFolder in folderFileService.WatchedFolders)
                ConsoleWriteUtil.Write($"- {watchedFolder}");

            ConsoleWriteUtil.Write();

            ConsoleWriteUtil.Write("Files to be deleted:");

            foreach (var fileToBeDeleted in folderFileService.FilesToBeDeleted)
                ConsoleWriteUtil.Write($"- {fileToBeDeleted}");

            ConsoleWriteUtil.Write();

            ConsoleWriteUtil.Write("Folders to be deleted:");

            foreach (var folderToBeDeleted in folderFileService.FoldersToBeDeleted)
                ConsoleWriteUtil.Write($"- {folderToBeDeleted}");

            ConsoleWriteUtil.Write();

            ConsoleWriteUtil.Write("CONTINUE? Y/N:", ConsoleColor.Green);

            var keyPressed = Console.ReadKey().Key;
            ConsoleWriteUtil.Write();
            ConsoleWriteUtil.Write();

            if (keyPressed != ConsoleKey.N && keyPressed != ConsoleKey.Y)
                throw new Exception("You must be press y or n");


            if (keyPressed == ConsoleKey.Y)
            {
                var result = folderFileService.SearchInFolders();
                ConsoleWriteUtil.Write();
                ConsoleWriteUtil.Write($"Deleted folders: {folderFileService.DeletedFoldersQuantity}", ConsoleColor.Green);
                ConsoleWriteUtil.Write($"Deleted files: {folderFileService.DeletedFilesQuantity}", ConsoleColor.Green);
            }           
        }
        catch (Exception e)
        {
            ConsoleWriteUtil.Write($"Error: {e.Message}", ConsoleColor.Red);
        }
        finally
        {
            ConsoleWriteUtil.Write();
            ConsoleWriteUtil.Write("----- FINISHED -----", ConsoleColor.Green);
            ConsoleWriteUtil.Write("Press any button to close app");
            Console.ReadLine();
        }
    }
}