using System.Collections.Concurrent;
using System.Runtime.InteropServices;

using Camera.MAUI;
using Camera.MAUI.ZXingHelper;
using CommunityToolkit.Maui;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Platform.Maui.UI;
using Emgu.CV.Structure;
using SkiaSharp;
using SkiaSharp.Views.Maui;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        private VideoCapture cap;
        private Mat frame;
        private bool playing;

        private ConcurrentQueue<SKBitmap> _bitmapQueue = new ConcurrentQueue<SKBitmap>();
        private SKBitmap _bitmap;

        private static object lockObject = new object();

        public MainPage()
        {
            InitializeComponent();
        //    Emgu.CV.Platform.Maui.MauiInvoke.Init();
            _ = MainPage_Load();

            //cameraView.CamerasLoaded += CameraView_CamerasLoaded;
            //cameraView.BarCodeDecoder = new CDecoder();
            //cameraView.BarcodeDetected += CameraView_BarcodeDetected;
            //cameraView.BarCodeDetectionEnabled = true;
            //cameraView.AutoStartPreview = false;

            //   cameraView.ControlBarcodeResultDuplicate = true;
        }
        //private void CameraView_CamerasLoaded(object sender, EventArgs e)
        //{
        //    if (cameraView.NumCamerasDetected > 0)
        //    {
        //        if (cameraView.NumMicrophonesDetected > 0)
        //            cameraView.Microphone = cameraView.Microphones.First();
        //        cameraView.Camera = cameraView.Cameras.First();
        //        MainThread.BeginInvokeOnMainThread(async () =>
        //        {
        //            if (await cameraView.StartCameraAsync() == CameraResult.Success)
        //            {
        //             //   controlButton.Text = "Stop";
        //                playing = true;
        //            }
                    
        //        });
        //    }
        //}
        private void CameraView_BarcodeDetected(object sender, BarcodeEventArgs args)
        {
            
            Console.WriteLine("BarcodeText=" + args.Result[0].Text);
        }
        private async Task MainPage_Load()
        {

            cap = new VideoCapture(0, VideoCapture.API.Android);  // Initialize capture from default camera
            cap.Set(CapProp.FrameCount, 10);
            cap.ImageGrabbed += _capture_ImageGrabbed;

            frame = new Mat();


            var timer = Application.Current.Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += (s, e) => DoSomething();
            timer.Start();
            cap.Start();
        }
        private void _capture_ImageGrabbed(object sender, EventArgs e)
        {
            if (frame == null)
                frame = new Mat();
            cap.Retrieve(frame);

            // Read a frame from the video capture
            //cap.Read(frame);


            if (!frame.IsEmpty)
            {
                Mat RgbMat = new Mat();
                CvInvoke.CvtColor(frame, RgbMat, ColorConversion.Yuv2BgraNv21);
                // Convert the frame to a bitmap
                var image = RgbMat.ToImage<Bgra, byte>();

                var bitmap = new SKBitmap(RgbMat.Width, RgbMat.Height, SKColorType.Bgra8888, SKAlphaType.Premul);
                // var bitmap2 = new SKBitmap(image.Width, image.Height, SKColorType.Bgra8888, SKAlphaType.Premul);

                IntPtr unmanagedPointer = Marshal.AllocHGlobal(image.Bytes.Length);
                Marshal.Copy(image.Bytes, 0, unmanagedPointer, image.Bytes.Length);

                bitmap.InstallPixels(new SKImageInfo(RgbMat.Width, RgbMat.Height, SKColorType.Bgra8888, SKAlphaType.Premul),
                    unmanagedPointer, RgbMat.Width * 4, (addr, ctx) => Marshal.FreeHGlobal(addr), null);




                if (_bitmapQueue.Count == 2) ClearQueue();
                _bitmapQueue.Enqueue(bitmap);

                Device.InvokeOnMainThreadAsync(() =>
                  {
                      ImageView.InvalidateSurface();
                  });
            }
            else
            {
                // playing = false;
            }

        }
        //private SKBitmap MatToSkBitmap(Mat mat)
        //{
        //    // 创建SKBitmap
        //    var skBitmap = new SKBitmap(mat.Width, mat.Height, SKImageInfo.PlatformColorType, SKAlphaType.Premul);


        //    // 处理颜色格式（BGR转BGRA）
        //    if (mat.NumberOfChannels == 3)
        //    {
        //        for (int y = 0; y < mat.Height; y++)
        //        {
        //            for (int x = 0; x < mat.Width; x++)
        //            {
        //                int index = (y * mat.Width + x) * 3;
        //                byte b = skBitmap.GetPixel(x, y).Blue;
        //                byte g = skBitmap.GetPixel(x, y).Green;
        //                byte r = skBitmap.GetPixel(x, y).Red;
        //                skBitmap.SetPixel(x, y, new SKColor(r, g, b, 255));
        //            }
        //        }
        //    }

        //    return skBitmap;
        //}

        void DoSomething()
        {
            playing = false;
            MainThread.BeginInvokeOnMainThread(async () =>
            {

                //Update view here
                if (playing)
                {
                    // Read a frame from the video capture
                    //cap.Read(frame);
                     cap.Read(frame);
                    
                    if (!frame.IsEmpty)
                    {
                        Mat RgbMat = new Mat();
                        CvInvoke.CvtColor(frame, RgbMat, ColorConversion.Yuv2BgraNv21);
                        // Convert the frame to a bitmap
                        var image = RgbMat.ToImage<Bgra,byte>();
                        
                        var bitmap = new SKBitmap(RgbMat.Width, RgbMat.Height, SKColorType.Bgra8888, SKAlphaType.Premul);
                       // var bitmap2 = new SKBitmap(image.Width, image.Height, SKColorType.Bgra8888, SKAlphaType.Premul);

                        IntPtr unmanagedPointer = Marshal.AllocHGlobal(image.Bytes.Length);
                        Marshal.Copy(image.Bytes, 0, unmanagedPointer, image.Bytes.Length);

                        bitmap.InstallPixels(new SKImageInfo(RgbMat.Width, RgbMat.Height, SKColorType.Bgra8888, SKAlphaType.Premul),
                            unmanagedPointer, RgbMat.Width * 4, (addr, ctx) => Marshal.FreeHGlobal(addr), null);




                        if (_bitmapQueue.Count == 2) ClearQueue();
                        _bitmapQueue.Enqueue(bitmap);

                    //    Device.InvokeOnMainThreadAsync(() =>
                  //      {
                                ImageView.InvalidateSurface();
                  //      });
                    }
                    else
                    {
                        // playing = false;
                    }
                }
            });
        }

        private void ClearQueue()
        {
            while (_bitmapQueue.TryDequeue(out var bitmap))
            {
                bitmap.Dispose();
            }
        }

        private void ImageView_PaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            if (_bitmapQueue.TryDequeue(out SKBitmap bitmap))
            {
                args.Surface.Canvas.Clear();
                args.Surface.Canvas.DrawBitmap(bitmap, new SKPoint(0, 0));
                bitmap.Dispose();
            }
        }

        protected override void OnDisappearing()
        {
            // Dispose of the VideoCapture object if it is not null
            base.OnDisappearing();
            playing = false;
            cap?.Dispose();
        }
        //private  void OnCounterClicked(object sender, EventArgs e)
        //{
        //     TakePhoto().Wait();
        //    count++;

        //    if (count == 1)
        //        .Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}
        public async Task TakePhoto()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    // save the file into local storage
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    using Stream sourceStream = await photo.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);

                    await sourceStream.CopyToAsync(localFileStream);
                }
            }
        }
    }

}
