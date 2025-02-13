using System.Text.Json.Serialization;

namespace AllDebridNET;

public class Magnet
{
    /// <summary>
    ///     Magnet id.
    /// </summary>
    [JsonPropertyName("id")]
    public Int64 Id { get; set; }

    /// <summary>
    ///     Magnet filename.
    /// </summary>
    [JsonPropertyName("filename")]
    public String? Filename { get; set; } = null!;

    /// <summary>
    ///     Magnet filesize.
    /// </summary>
    [JsonPropertyName("size")]
    public Int64? Size { get; set; }

    [JsonPropertyName("hash")]
    public String? Hash { get; set; } = null!;

    /// <summary>
    ///     Status in plain English.
    /// </summary>
    [JsonPropertyName("status")]
    public String? Status { get; set; } = null!;

    /// <summary>
    ///     Status code.
    ///     0	Processing	In Queue.
    ///     1	Processing	Downloading.
    ///     2	Processing	Compressing / Moving.
    ///     3	Processing	Uploading.
    ///     4	Finished	Ready.
    ///     5	Error	Upload fail.
    ///     6	Error	Internal error on unpacking.
    ///     7	Error	Not downloaded in 20 min.
    ///     8	Error	File too big.
    ///     9	Error	Internal error.
    ///     10	Error	Download took more than 72h.
    ///     11	Error	Deleted on the hoster website
    /// </summary>
    [JsonPropertyName("statusCode")]
    public Int32? StatusCode { get; set; }

    /// <summary>
    ///     Downloaded data so far.
    /// </summary>
    [JsonPropertyName("downloaded")]
    public Int64? Downloaded { get; set; }

    /// <summary>
    ///     Uploaded data so far.
    /// </summary>
    [JsonPropertyName("uploaded")]
    public Int64? Uploaded { get; set; }

    /// <summary>
    ///     Seeders count.
    /// </summary>
    [JsonPropertyName("seeders")]
    public Int64? Seeders { get; set; }

    /// <summary>
    ///     Download speed.
    /// </summary>
    [JsonPropertyName("downloadSpeed")]
    public Int64? DownloadSpeed { get; set; }

    [JsonPropertyName("processingPerc")]
    public Int64? ProcessingPerc { get; set; }

    /// <summary>
    ///     Upload speed.
    /// </summary>
    [JsonPropertyName("uploadSpeed")]
    public Int64? UploadSpeed { get; set; }

    /// <summary>
    ///     Timestamp of the date of the magnet upload.
    /// </summary>
    [JsonPropertyName("uploadDate")]
    public Int64? UploadDate { get; set; }

    /// <summary>
    ///     Timestamp of the date of the magnet completion.
    /// </summary>
    [JsonPropertyName("completionDate")]
    public Int64? CompletionDate { get; set; }

    [JsonPropertyName("files")]
    [JsonConverter(typeof(ListConverter<File>))]
    public List<File>? Files { get; set; }
}

public class File
{
    [JsonPropertyName("n")]
    public String FolderOrFileName { get; set; } = null!;

    [JsonPropertyName("s")]
    public Int64? Size { get; set; }

    [JsonPropertyName("e")]
    [JsonConverter(typeof(ListConverter<File>))]
    public List<File>? SubNodes { get; set; }

    [JsonPropertyName("l")]
    public String? DownloadLink { get; set; }
}
