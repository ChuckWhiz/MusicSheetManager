using System;

namespace MusicSheetManager;

public class MusicSheetMetadata
{
    public string? FileName { get; set; }
    public string? FilePath { get; set; }
    public string? FileType { get; set; }
    public long? FileSize { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? LastModified { get; set; }
}