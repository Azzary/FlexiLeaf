using System.Drawing;
using System.Drawing.Imaging;

namespace FlexiLeaf.Core.Network.Packets
{
    public class ScreenPacket : Packet
    {
        public static int Id => 1;

        public override int MessageId => Id;

        public short QualityPourcent { get; set; } = 50;
        public short FrameRate { get; set; } = 50;
        public bool ShowScreen { get; set; } = false;
        public byte[] ImageArray { get; set; } = Array.Empty<byte>();


        public ScreenPacket() { }
        public ScreenPacket(bool showScreen, short frameRate, short QualityPourcent)
        {
            ShowScreen = showScreen;
            this.FrameRate = frameRate;
            this.QualityPourcent = QualityPourcent;
        }

        public void TakeScreen()
        {
            var screenImage = CaptureScreen();
            ImageArray = BitmapToByteArray(screenImage);
        }

        public Bitmap CaptureScreen()
        {
            Bitmap bitmap = new(1920, 1080);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            }

            // Calculate new size based on the quality percentage
            int newWidth = bitmap.Width * QualityPourcent / 100;
            int newHeight = bitmap.Height * QualityPourcent / 100;

            // Create new bitmap with reduced size
            Bitmap reducedBitmap = new(newWidth, newHeight);
            using (Graphics graphics = Graphics.FromImage(reducedBitmap))
            {
                graphics.DrawImage(bitmap, 0, 0, newWidth, newHeight);
            }

            return reducedBitmap;
        }


        public static byte[] BitmapToByteArray(Bitmap bitmap)
        {
            using MemoryStream stream = new();
            bitmap.Save(stream, ImageFormat.Png);
            return stream.ToArray();
        }

        public static Bitmap ByteArrayToBitmap(byte[] bytes)
        {
            using MemoryStream stream = new(bytes);
            return new(stream);
        }

    }
}
