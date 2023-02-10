using System;

namespace YouthActionDotNet.Models{
    public class File{
        public File(){
            FileId = Guid.NewGuid().ToString();
        }
        public string FileId { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string FileMIME { get; set; }
        public string FileExt { get; set; }
    }
}