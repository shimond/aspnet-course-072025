﻿namespace FilesApi.Model.Requests;

public record RenameFolderRequest(string OldName, string NewName);

// relace to record.
//public class RenameFolderRequest
//{
//    public required string OldName { get; set; }
//    public required string NewName { get; set; }
//}



//public class RenameFolderRequest
//{
//    public string OldName { get;  }
//    public string NewName { get;  }


//    public RenameFolderRequest(string oldName, string newName)
//    {
//        OldName = oldName;
//        NewName = newName;
//    }
//}




