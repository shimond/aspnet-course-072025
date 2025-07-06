using FilesApi.Contracts;
using FilesApi.Model.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesManagerController(IFileManagerService fileManagerService) : ControllerBase
    {

        // retrun relevant status code foreach request and error handling


        [HttpGet("filesCount/{folderPath}")]
        public int GetFilesCount(string folderPath)
        {
            return fileManagerService.GetFilesCount(folderPath);
        }

        [HttpPost("Rename")]
        public bool RenameDirectory(RenameFolderRequest request)
        {
            try
            {
                fileManagerService.RenameDirectory(request.OldName, request.NewName);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
