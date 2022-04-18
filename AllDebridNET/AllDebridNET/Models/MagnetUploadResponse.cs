using System.Collections.Generic;
using Newtonsoft.Json;

namespace AllDebridNET;

internal class MagnetUploadResponse
{
    [JsonProperty("magnets")]
    public List<MagnetAddResult> Magnets { get; set; }

    [JsonProperty("files")]
    public List<MagnetAddResult> Files { get; set; }
}