using ClickSharp.Components.Test;
using ClickSharp.Models.Auth;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;

namespace ClickSharp.Helpers
{
    public static class Extensions
    {
        public enum ImageFormat  
        {  
            bmp,
            jpg,
            jpeg,
            jpeg2,
            gif,
            tiff,
            tiff2,
            png,
            unknown
        }
        public static void Swap<T>(this List<T> list, int i, int j)
        {
            (list[i], list[j]) = (list[j], list[i]);
        }
        public static ImageFormat IsImage(this byte[] fileToValidate, string mime)
        {
            var mineType = mime.ToLower();
            var allowedImages = new Dictionary<ImageFormat,byte[]>
            {
                {ImageFormat.bmp,Encoding.ASCII.GetBytes("BM") },      // BMP
                {ImageFormat.gif,Encoding.ASCII.GetBytes("GIF")},     // GIF
                {ImageFormat.png,new byte[] { 137, 80, 78, 71 }},     // PNG
                {ImageFormat.tiff,new byte[] { 73, 73, 42 }},          // TIFF
                {ImageFormat.tiff2,new byte[] { 77, 77, 42 }},          // TIFF
                {ImageFormat.jpg,new byte[] { 0xFF, 0xD8, 0xFF }},   //JPG
                {ImageFormat.jpeg,new byte[] { 255, 216, 255 }},  // JPEG
                {ImageFormat.jpeg2,new byte[] { 255, 216, 255 }}   // JPEG CANON
                //{new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 }, ImageFormat.jpeg},
                //{new byte[] { 0x89, 0x50, 0x4E, 0x47 }, ImageFormat.png},
                //{new byte[] { 0x49, 0x49, 0x2A, 0x00 }, ImageFormat.tiff},
                //{new byte[] { 0x47, 0x49, 0x46, 0x38 }, ImageFormat.gif},
                //{new byte[] { 0x42, 0x4D }, ImageFormat.bmp}
            };
            //foreach (var hex in fileToValidate)
            //{
            ////Console.WriteLine($"mime: {mineType}");
            ////Console.WriteLine($"hex: {BitConverter.ToString(fileToValidate)}");
            //}
            foreach(var allowedImg in allowedImages)
            {

                if(mineType.Contains(allowedImg.Key.ToString()))
                    if (allowedImg.Value.SequenceEqual(fileToValidate.Take(allowedImg.Value.Length)))
                        return allowedImg.Key;
            }
            return ImageFormat.unknown;
        }
        public static void SetParent(this DraggableItemData child, DraggableItemData parent)
        {
            child.Parent = parent;
            child.MenuData.ParentId = parent.MenuData.Id;
        }
    }
}
