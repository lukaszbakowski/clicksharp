using System.Text;

namespace ClickSharp.Helpers
{
    public static class Extensions
    {
        public static void Swap<T>(this List<T> list, int i, int j)
        {
            (list[i], list[j]) = (list[j], list[i]);
        }
        public static bool IsImage(this byte[] fileBytes)
        {
            var headers = new List<byte[]>
        {
            Encoding.ASCII.GetBytes("BM"),      // BMP
            Encoding.ASCII.GetBytes("GIF"),     // GIF
            new byte[] { 137, 80, 78, 71 },     // PNG
            new byte[] { 73, 73, 42 },          // TIFF
            new byte[] { 77, 77, 42 },          // TIFF
            new byte[] { 255, 216, 255, 224 },  // JPEG
            new byte[] { 255, 216, 255, 225 }   // JPEG CANON
        };

            return headers.Any(x => x.SequenceEqual(fileBytes.Take(x.Length)));
        }
    }
}
