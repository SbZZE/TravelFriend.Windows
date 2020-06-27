using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TravelFriend.Windows.Http.BreakPoint
{
    public class BreakPointUploadRequest : HttpRequest
    {
        public BreakPointUploadRequest(string url, string fileName, FileType fileType, string identifier, int totalSize, int totalChunks,
                                       int chunkNumber, int chunkSize, int currentChunkSize)
                                       : base(url)
        {
            FileName = fileName;
            FileType = fileType;
            Identifier = identifier;
            TotalSize = totalSize;
            TotalChunks = totalChunks;
            ChunkNumber = chunkNumber;
            ChunkSize = chunkSize;
            CurrentChunkSize = currentChunkSize;
        }

        [JsonProperty("filename")]
        public string FileName { get; set; }
        [JsonProperty("filetype")]
        public FileType FileType { get; set; }
        [JsonProperty("identifier")]
        public string Identifier { get; set; }
        [JsonProperty("totalsize")]
        public int TotalSize { get; set; }
        [JsonProperty("totalchunks")]
        public int TotalChunks { get; set; }
        [JsonProperty("chunknumber")]
        public int ChunkNumber { get; set; }
        [JsonProperty("chunksize")]
        public int ChunkSize { get; set; }
        [JsonProperty("currentchunksize")]
        public int CurrentChunkSize { get; set; }
    }
}
