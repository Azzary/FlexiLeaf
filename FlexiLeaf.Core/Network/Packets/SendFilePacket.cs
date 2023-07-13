using System;
using System.IO;

namespace FlexiLeaf.Core.Network.Packets
{
    public class SendFilePacket : Packet
    {
        private static int maxChunkSize = 1 * 1024 * 1024;

        public SendFilePacket() { }

        public SendFilePacket(string filePath)
        {
            this.GUID = Guid.NewGuid().ToString();
            this.FilePath = filePath;
        }

        public override int MessageId => Id;

        public static int Id => 6;

        public string GUID { get; set; }
        public int CurrentChunk { get; set; } = 0;
        public int TotalChunk { get; set; } = 0;
        public byte[] Data { get; set; }
        public string FilePath { get; set; }
        public string TargetDirectory { get; set; }

        public bool ReadFileInChunks()
        {
            if (TotalChunk == 0)
            {
                FileInfo fileInfo = new FileInfo(FilePath);
                TotalChunk = (int)Math.Ceiling((double)fileInfo.Length / maxChunkSize);
            }

            int bytesRead;
            byte[] buffer = new byte[maxChunkSize];

            using (FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                long seekPosition = CurrentChunk * maxChunkSize;
                fileStream.Seek(seekPosition, SeekOrigin.Begin);

                bytesRead = fileStream.Read(buffer, 0, buffer.Length);
            }

            CurrentChunk++;
            Data = new byte[bytesRead];
            Buffer.BlockCopy(buffer, 0, Data, 0, bytesRead);

            return CurrentChunk >= TotalChunk;
        }

        private static Mutex fileMutex = new Mutex();

        public async void WriteFileFromChunks()
        {
            string fileName = Path.GetFileName(FilePath);
            string path = Path.Combine(TargetDirectory, fileName);
            fileMutex.WaitOne();
            try
            {
                FileMode mode = CurrentChunk == 1 ? FileMode.Create : FileMode.Open;
                using (FileStream fileStream = new FileStream(path, mode, FileAccess.Write))
                {
                    long position = (CurrentChunk - 1) * maxChunkSize; // Utilisez maxChunkSize pour calculer la position
                    fileStream.Seek(position, SeekOrigin.Begin);
                    fileStream.Write(Data, 0, Data.Length);
                }
            }
            catch (IOException e)
            {
                fileMutex.ReleaseMutex();
                await Task.Delay(1000);
                WriteFileFromChunks();
                return;
            }
            fileMutex.ReleaseMutex();

        }

    }
}
