namespace FilesApi.Contracts;

public interface IFileManagerService
{
    int GetFilesCount(string folderPath);
    void RenameDirectory(string folderPath, string newFolderName);
}
