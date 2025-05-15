using Camera.MAUI;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if IOS || MACCATALYST
using DecodeDataType = UIKit.UIImage;
#elif ANDROID
using DecodeDataType = Android.Graphics.Bitmap;
#elif WINDOWS
using DecodeDataType = Windows.Graphics.Imaging.SoftwareBitmap;
#else
using DecodeDataType = System.Object;
#endif
namespace MauiApp1
{
    internal class CDecoder : IBarcodeDecoder
    {
        public CDecoder()
        {
        }

        public BarcodeResult[] Decode(DecodeDataType data)
        {
            List<BarcodeResult> returnResults = new List<BarcodeResult>();
            returnResults.Add(new BarcodeResult("hello world", [0, 0], [new Point(0,0),new Point(0,10), new Point(10, 10),new Point(10, 0)],BarcodeFormat.PHARMA_CODE));
            return returnResults.ToArray();
        }

        public void SetDecodeOptions(BarcodeDecodeOptions options)
        {
           // throw new NotImplementedException();
        }
    }
}
