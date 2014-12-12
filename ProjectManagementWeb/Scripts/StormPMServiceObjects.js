function ProjectTransferObject() {
    var obj = new Object();

    obj.ProjectID = -1;
    obj.SourceLanguageID = -1;
    obj.TargetLanguageIDs = [];
    obj.RootFolderID = -1;
    obj.Resources = [];

    return obj;
}
