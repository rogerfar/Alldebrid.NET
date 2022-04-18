using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AllDebridNET;

public class Magnet
{
    /// <summary>
    ///     Magnet id.
    /// </summary>
    [JsonProperty("id")]
    public Int64 Id { get; set; }

    /// <summary>
    ///     Magnet filename.
    /// </summary>
    [JsonProperty("filename")]
    public String Filename { get; set; }

    /// <summary>
    ///     Magnet filesize.
    /// </summary>
    [JsonProperty("size")]
    public Int64 Size { get; set; }

    [JsonProperty("hash")]
    public String Hash { get; set; }

    /// <summary>
    ///     Status in plain English.
    /// </summary>
    [JsonProperty("status")]
    public String Status { get; set; }

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
    [JsonProperty("statusCode")]
    public Int64 StatusCode { get; set; }

    /// <summary>
    ///     Downloaded data so far.
    /// </summary>
    [JsonProperty("downloaded")]
    public Int64 Downloaded { get; set; }

    /// <summary>
    ///     Uploaded data so far.
    /// </summary>
    [JsonProperty("uploaded")]
    public Int64 Uploaded { get; set; }

    /// <summary>
    ///     Seeders count.
    /// </summary>
    [JsonProperty("seeders")]
    public Int64 Seeders { get; set; }

    /// <summary>
    ///     Download speed.
    /// </summary>
    [JsonProperty("downloadSpeed")]
    public Int64 DownloadSpeed { get; set; }

    [JsonProperty("processingPerc")]
    public Int64 ProcessingPerc { get; set; }

    /// <summary>
    ///     Upload speed.
    /// </summary>
    [JsonProperty("uploadSpeed")]
    public Int64 UploadSpeed { get; set; }

    /// <summary>
    ///     Timestamp of the date of the magnet upload.
    /// </summary>
    [JsonProperty("uploadDate")]
    public Int64 UploadDate { get; set; }

    /// <summary>
    ///     Timestamp of the date of the magnet completion.
    /// </summary>
    [JsonProperty("completionDate")]
    public Int64 CompletionDate { get; set; }

    [JsonProperty("links")]
    public List<Link> Links { get; set; }

    [JsonProperty("type")]
    public String Type { get; set; }

    [JsonProperty("notified")]
    public Boolean Notified { get; set; }

    [JsonProperty("version")]
    public Int64 Version { get; set; }
}

public class Link
{
    /// <summary>
    ///     Download link.
    /// </summary>
    [JsonProperty("link")]
    public Uri LinkUrl { get; set; }

    /// <summary>
    ///     File name
    /// </summary>
    [JsonProperty("filename")]
    public String Filename { get; set; }

    /// <summary>
    ///     File size.
    /// </summary>
    [JsonProperty("size")]
    public Int64 Size { get; set; }

    [JsonProperty("files")]
    public List<File> Files { get; set; }
}

public class File
{
    [JsonProperty("n")]
    public String N { get; set; }

    [JsonProperty("e", NullValueHandling = NullValueHandling.Ignore)]
    public FileEUnion? E { get; set; }
}

public class FileE1
{
    [JsonProperty("n")]
    public String N { get; set; }

    [JsonProperty("e", NullValueHandling = NullValueHandling.Ignore)]
    public List<FileE2> E { get; set; }
}

public class FileE2
{
    [JsonProperty("n")]
    public String N { get; set; }
}

public struct FileEUnion
{
    public FileE1 FileE1;
    public List<FileE1> PurpleEArray;

    public static implicit operator FileEUnion(FileE1 fileE1) => new() { FileE1 = fileE1 };
    public static implicit operator FileEUnion(List<FileE1> PurpleEArray) => new() { PurpleEArray = PurpleEArray };
}