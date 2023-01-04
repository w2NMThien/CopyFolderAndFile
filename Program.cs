using System.Text.Json;

var jsonFile = "FilePath.json";
var text = File.ReadAllText(jsonFile);
var json = JsonSerializer.Deserialize<FileClass>(text);
var info = new DirectoryInfo(json.SourcePath);
var infos = info.GetDirectories();
var sourceFiles = Directory.GetFiles(json.SourcePath);

// Copy file outside folder
CopyFile(sourceFiles, json.DestinationPath);

// Copy file inside folder
foreach (var item in infos)
{
    var path = Path.Combine(json.DestinationPath, item.Name);
    if (Directory.Exists(path) == false) Directory.CreateDirectory(path);

    var files = Directory.GetFiles(item.ToString());
    CopyFile(files, path);
}

void CopyFile(string[] sourcePath, string destinationPath)
{
    foreach (var sourceFile in sourcePath)
    {
        var path = Path.Combine(destinationPath, Path.GetFileName(sourceFile));
        if (File.Exists(path)) continue;
        File.Copy(sourceFile, path, true);
    }
}

/// <summary>
/// File Class
/// </summary>
class FileClass
{
    public string SourcePath { get; set; }
    public string DestinationPath { get; set; }
}
