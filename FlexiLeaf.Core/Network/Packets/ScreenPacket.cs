using System.Drawing;
using System.Drawing.Imaging;


namespace FlexiLeaf.Core.Network.Packets
{
    public class ScreenPacket : Packet
    {
        public static int Id => 1;

        public override int MessageId => Id;

        public bool ShowScreen { get; set; } = false;
        public byte[] ImageArray { get; set; } = new byte[0];


        public ScreenPacket() { }
        public ScreenPacket(bool showScreen)
        {
            ShowScreen = showScreen;
        }

        public static ScreenPacket TakeScreen()
        {
            var screenPacket = new ScreenPacket(true);
            var screenImage = CaptureScreen();
            screenPacket.ImageArray = BitmapToByteArray(screenImage);
            return screenPacket;
        }

        public static Bitmap CaptureScreen()
        {
            Bitmap bitmap = new Bitmap(1920, 1080);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            }

            return bitmap;
        }

        public static byte[] BitmapToByteArray(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }

        public static Bitmap ByteArrayToBitmap(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                return new Bitmap(stream);
            }
        }

    }
}
