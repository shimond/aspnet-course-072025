using Microsoft.AspNetCore.Http.HttpResults;

namespace FilesApi.Apis;

public static class FilesApi
{
    public static IEndpointRouteBuilder MapFilesApi(this IEndpointRouteBuilder app)
    {
        var filesGroup = app.MapGroup("api/files");

        filesGroup.MapPost("rename", Rename);
        filesGroup.MapGet("{folderPath}", GetFilesCount);

        return app;
    }

    static Results<Ok<int>, NotFound> GetFilesCount(string folderPath, ILoggerFactory loggerFactory, IFileManagerService fileManagerService)
    {
        var logger = loggerFactory.CreateLogger(nameof(FilesApi));
        try
        {
            var res = fileManagerService.GetFilesCount(folderPath);
            return TypedResults.Ok(res);

        }
        catch (DirectoryNotFoundException)
        {
            return TypedResults.NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while getting files count for folder: {FolderPath}", folderPath);
            throw;
        }
    }
    static Results<Ok<string>, NotFound, Conflict<string>> Rename(RenameFolderRequest request, IFileManagerService fileManagerService)
    {
        try
        {
            var recordToUse = request;
            if (request.OldName == request.NewName)
            {
                recordToUse = request with { NewName = $"{request.NewName}_renamed" };
            }

            fileManagerService.RenameDirectory(recordToUse.OldName, recordToUse.NewName);
            return TypedResults.Ok(request.NewName);
        }
        catch (DirectoryNotFoundException)
        {
            return TypedResults.NotFound();
        }
        catch (IOException)
        {
            // 409
            return TypedResults.Conflict($"A directory with the name '{request.NewName}' already exists.");
        }

    }
}
