using Microsoft.AspNetCore.Mvc;

namespace FilesApi.Controllers
{
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
            }
            catch (DirectoryNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while getting files count for folder: {FolderPath}", folderPath);
                throw;
                //throw ex;
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
                if(request.OldName == request.NewName)
                {
                    request.NewName = $"{request.NewName}_{Guid.NewGuid()}";
                }

                fileManagerService.RenameDirectory(request.OldName, request.NewName);
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

}
