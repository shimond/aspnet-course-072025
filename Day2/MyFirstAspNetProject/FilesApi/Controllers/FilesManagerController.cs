using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


// 1. using Microsoft.AspNetCore.Mvc - MVC  legacy framework
// 2. return type ActionResult<T> - MVC  legacy framework not validate on compile time
// 3. Controller is a class that inherits from ControllerBase - MVC  legacy framework

namespace FilesApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilesManagerController(
    ILogger<FilesManagerController> logger,
    IFileManagerService fileManagerService) : ControllerBase
{

    [HttpGet("filesCount/{folderPath}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public ActionResult<int> GetFilesCount(string folderPath)
    {
        try
        {
            var res = fileManagerService.GetFilesCount(folderPath);
            return Ok(res);
            //return Ok(new Random());
        }
        catch (DirectoryNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while getting files count for folder: {FolderPath}", folderPath);
            throw;
        }

    }

    [HttpPost("Rename")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public ActionResult<string> RenameDirectory(RenameFolderRequest request)
    {
        try
        {
            var recordToUse = request;
            if(request.OldName == request.NewName)
            {
                recordToUse = request with { NewName = $"{request.NewName}_renamed" };
            }

            fileManagerService.RenameDirectory(recordToUse.OldName, recordToUse.NewName);
            return Ok(request.NewName);
        }
        catch (DirectoryNotFoundException)
        {
            return NotFound();
        }
        catch (IOException)
        {
            // 409
            return Conflict($"A directory with the name '{request.NewName}' already exists.");
        }

    }
}
