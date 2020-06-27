using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TravelFriend.Windows.Http.BreakPoint;

namespace TravelFriend.Windows.Http.Album
{
    public class UploadAlbumFileRequest : BreakPointUploadRequest
    {
        public UploadAlbumFileRequest(string targetId, string albumId, AlbumType albumType, string fileName, FileType fileType, string identifier,
                                      int totalSize, int totalChunks, int chunkNumber, int chunkSize, int currentChunkSize)
                                    : base(ApiUtils.BreakPointUpload, fileName, fileType, identifier, totalSize, totalChunks, chunkNumber, chunkSize, currentChunkSize)
        {
            TargetId = targetId;
            AlbumId = albumId;
            AlbumType = albumType;
        }

        [JsonProperty("targetid")]
        public string TargetId { get; set; }
        [JsonProperty("albumid")]
        public string AlbumId { get; set; }
        [JsonProperty("albumtype")]
        public AlbumType AlbumType { get; set; }
    }
}
