namespace FilesApi.Services;

public class FileManagerService : IFileManagerService
{
    public int GetFilesCount(string folderPath)
    {
        var files = Directory.GetFiles(folderPath);
        return files.Length;
    }

    public void RenameDirectory(string folderPath, string newFolderName)
    {
        var directoryInfo = new DirectoryInfo(folderPath);
        if (!directoryInfo.Exists)
        {
            throw new DirectoryNotFoundException($"The directory '{folderPath}' does not exist.");
        }
        var newDirectoryPath = Path.Combine(directoryInfo.Parent?.FullName ?? string.Empty, newFolderName);
        if (Directory.Exists(newDirectoryPath))
        {
            throw new IOException($"A directory with the name '{newFolderName}' already exists.");
        }
        directoryInfo.MoveTo(newDirectoryPath);
        throw new Exception("WOW");
    }
}
