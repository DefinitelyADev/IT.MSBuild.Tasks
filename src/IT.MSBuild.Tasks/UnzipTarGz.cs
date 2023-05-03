using System.Text;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using Microsoft.Build.Framework;

namespace IT.MSBuild.Tasks;

public class UnzipTarGz : Microsoft.Build.Utilities.Task
{
    #region Properties

    /// <summary>
    /// Gets or sets the files to extract.
    /// </summary>
    /// <value>The files to extract.</value>
    [Required]
    public string[] SourceFiles { get; set; } = null!;

    /// <summary>
    /// Gets or sets the path to extract the contents of the files.
    /// </summary>
    /// <value>The path to extract the contents of the files.</value>
    [Required]
    public string DestinationFolder { get; set; } = null!;

    #endregion

    public override bool Execute()
    {
        foreach (var sourceFile in SourceFiles)
        {
            Console.WriteLine($"Unpacking \"{sourceFile}\" to \" DestinationFolder\"");
            Unzip(sourceFile);
        }

        return true;
    }

    private void Unzip(string sourceFile)
    {
        using var gZipInputStream = new GZipInputStream(File.OpenRead(sourceFile));
        using var tarArchive = TarArchive.CreateInputTarArchive(gZipInputStream, Encoding.Default);
        tarArchive.ExtractContents(DestinationFolder);
    }
}
