using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_OpenCV_Player
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void player()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string fullpath = System.IO.Path.Combine(path, "cheerleading_0001.mp4");
            VideoCapture video = VideoCapture.FromFile(fullpath);

            if (!video.IsOpened())
            {
                MessageBox.Show("not open");
                return;
            }

            Mat frame = new Mat();

            video.Set(CaptureProperty.FrameWidth, video.FrameWidth);
            video.Set(CaptureProperty.FrameHeight, video.FrameHeight);
            double fps = video.Get(CaptureProperty.Fps);
            
            while (true)
            {
                if (video.Read(frame))
                {
                    if (frame.Width == 0 && frame.Height == 0)
                        break;

                    Cv2.ImShow("00", frame);
                    WriteableBitmap wb = new WriteableBitmap(video.FrameWidth, video.FrameHeight, 96, 96, PixelFormats.Bgr24, null);
                    WriteableBitmapConverter.ToWriteableBitmap(frame, wb);

                    Dispatcher.Invoke(new Action(delegate ()
                    {
                        img_player.Source = wb;
                    }));

                    if (Cv2.WaitKey(10) == 27)
                    {
                        break;
                    }
                }
            }
        }

        private void btn_player_Click(object sender, RoutedEventArgs e)
        {
            //Task t1 = new Task(new Action(player));
            //t1.Start();
            player();
        }
    }
}
