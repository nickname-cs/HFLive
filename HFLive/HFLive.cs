using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using NAudio.Wave;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using Imgur.API.Models;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API;

namespace HFLive
{
    public partial class HFLive : Form
    {
        string CURRENT_VERSION = "1.6"; 

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private BufferedWaveProvider bufferedWaveProvider;
        private IWavePlayer waveOut;
        private volatile StreamingPlaybackState playbackState;
        private volatile bool fullyDownloaded;
        private HttpWebRequest webRequest;
        private VolumeWaveProvider16 volumeProvider;
        delegate void ShowErrorDelegate(string message);
        RadioStream rs = new RadioStream(liveRadioURL);
        const string liveRadioURL = "http://epsilon.shoutca.st:8151/live";
        volatile bool Metarun = true;

        public HFLive()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_RESTORE = 0xF120;

            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_RESTORE)
            {
                Animations.FadeIn(this);
            }

            base.WndProc(ref m);
        }

        enum StreamingPlaybackState
        {
            Stopped,
            Playing,
            Buffering,
            Paused
        }

        public void UpdateMyLabel(string title, string artist )
        {
            Invoke(new MethodInvoker(() => labelTitle.Text = title));
            Invoke(new MethodInvoker(() => labelArtist.Text = artist));
        }
        private void StreamWorker()
        {       
            if(rs.Connect())
            {
                rs.Caller = this;
                byte[] buffer = new byte[512];

                while (Metarun) rs.Read(buffer, 0, buffer.Length);
            }
        }
        private void ShowError(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ShowErrorDelegate(ShowError), message);
            }
            else
            {
                MessageBox.Show(message);
            }
        }

        private void StreamMp3(object state)
        {
            fullyDownloaded = false;
            var url = (string)state;
            webRequest = (HttpWebRequest)WebRequest.Create(url);

            HttpWebResponse resp;
            try
            {
                resp = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Status != WebExceptionStatus.RequestCanceled)
                {
                    ShowError(e.Message);
                }
                return;
            }
            var buffer = new byte[16384 * 4];

            IMp3FrameDecompressor decompressor = null;
            try
            {
                using (var responseStream = resp.GetResponseStream())
                {
                    var readFullyStream = new ReadFullyStream(responseStream);
                    do
                    {
                        if (IsBufferNearlyFull)
                        {
                            Debug.WriteLine("Buffer getting full, taking a break");
                            Thread.Sleep(500);
                        }
                        else
                        {
                            Mp3Frame frame;
                            try
                            {
                                frame = Mp3Frame.LoadFromStream(readFullyStream);
                            }
                            catch (EndOfStreamException)
                            {
                                fullyDownloaded = true;
                                break;
                            }
                            catch (WebException)
                            {
                                break;
                            }
                            if (decompressor == null)
                            {
                                decompressor = CreateFrameDecompressor(frame);
                                bufferedWaveProvider = new BufferedWaveProvider(decompressor.OutputFormat);
                                bufferedWaveProvider.BufferDuration = TimeSpan.FromSeconds(20);
                            }
                            int decompressed = decompressor.DecompressFrame(frame, buffer, 0);
                            bufferedWaveProvider.AddSamples(buffer, 0, decompressed);
                        }

                    } while (playbackState != StreamingPlaybackState.Stopped);
                    Debug.WriteLine("Exiting");
                    decompressor.Dispose();
                }
            }
            finally
            {
                if (decompressor != null)
                {
                    decompressor.Dispose();
                }
            }
        }

        private static IMp3FrameDecompressor CreateFrameDecompressor(Mp3Frame frame)
        {
            WaveFormat waveFormat = new Mp3WaveFormat(frame.SampleRate, frame.ChannelMode == ChannelMode.Mono ? 1 : 2,
                frame.FrameLength, frame.BitRate);
            return new AcmMp3FrameDecompressor(waveFormat);
        }

        private bool IsBufferNearlyFull
        {
            get
            {
                return bufferedWaveProvider != null &&
                       bufferedWaveProvider.BufferLength - bufferedWaveProvider.BufferedBytes
                       < bufferedWaveProvider.WaveFormat.AverageBytesPerSecond / 4;
            }
        }

        private void StopPlayback()
        {
            Properties.Settings.Default.Volume = volumeProvider.Volume;
            Properties.Settings.Default.Save();

            if (playbackState != StreamingPlaybackState.Stopped)
            {
                if (!fullyDownloaded)
                {
                    webRequest.Abort();
                }

                playbackState = StreamingPlaybackState.Stopped;
                if (waveOut != null)
                {
                    waveOut.Stop();
                    waveOut.Dispose();
                    waveOut = null;
                }
                timer1.Enabled = false;
                Metarun = false;
                Thread.Sleep(500);
            }
        }

        private void Play()
        {
            waveOut.Play();
            Debug.WriteLine(String.Format("Started playing, waveOut.PlaybackState={0}", waveOut.PlaybackState));
            playbackState = StreamingPlaybackState.Playing;
        }

        private void Pause()
        {
            playbackState = StreamingPlaybackState.Buffering;
            waveOut.Pause();
            Debug.WriteLine(String.Format("Paused to buffer, waveOut.PlaybackState={0}", waveOut.PlaybackState));
        }

        private IWavePlayer CreateWaveOut()
        {
            return new WaveOut();
        }

        private string CheckUpdates()
        {
            try
            {
                string latest = new WebClient().DownloadString("http://pastebin.com/raw/pjB1j4iB");
                var splitted = latest.Split('^');
                if (Application.ProductName == "Orcus.Administration" && splitted[0] != CURRENT_VERSION)
                {
                    return splitted[1];
                }
                else if (splitted[0] != CURRENT_VERSION)
                {
                    labelUpdate.Visible = true;
                    return splitted[2];
                }
                return null;
            }
            catch { return null; }
        }

        public async Task UploadImage(string path, string clientID = "b34dfc9f764b138", string clientSecret = "f57fc73cfd8c84b5a73ae809f7158f7ef9f60b34")
        {
            try
            {
                var client = new ImgurClient(clientID, clientSecret);
                var endpoint = new ImageEndpoint(client);
                IImage image;
                using (var fs = new FileStream(@path, FileMode.Open))
                {
                    image = await endpoint.UploadImageStreamAsync(fs);
                }
                Debug.Write("Image uploaded. Image Url: " + image.Link);
                Process.Start(image.Link);
            }
            catch (ImgurException imgurEx)
            {
                Debug.Write("An error occurred uploading an image to Imgur.");
                Debug.Write(imgurEx.Message);
            }
        }

        #region "Events"

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (playbackState == StreamingPlaybackState.Stopped)
            {
                playbackState = StreamingPlaybackState.Buffering;
                bufferedWaveProvider = null;
                Thread t = new Thread(new ThreadStart(StreamWorker)) { IsBackground = false };
                t.Start();
                ThreadPool.QueueUserWorkItem(StreamMp3, liveRadioURL);

                timer1.Enabled = true;
            }
            else if (playbackState == StreamingPlaybackState.Paused)
            {
                playbackState = StreamingPlaybackState.Buffering;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (playbackState != StreamingPlaybackState.Stopped)
            {
                if (waveOut == null && bufferedWaveProvider != null)
                {
                    Debug.WriteLine("Creating WaveOut Device");
                    waveOut = CreateWaveOut();
                    waveOut.PlaybackStopped += OnPlaybackStopped;
                    volumeProvider = new VolumeWaveProvider16(bufferedWaveProvider);
                    volumeProvider.Volume = Properties.Settings.Default.Volume;
                    waveOut.Init(volumeProvider);
                }
                else if (bufferedWaveProvider != null)
                {
                    var bufferedSeconds = bufferedWaveProvider.BufferedDuration.TotalSeconds;
                    if (bufferedSeconds < 0.5 && playbackState == StreamingPlaybackState.Playing && !fullyDownloaded)
                    {
                        Pause();
                    }
                    else if (bufferedSeconds > 4 && playbackState == StreamingPlaybackState.Buffering)
                    {
                        Play();
                    }
                    else if (fullyDownloaded && bufferedSeconds == 0)
                    {
                        Debug.WriteLine("Reached end of stream");
                        StopPlayback();
                    }
                }
            }
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            if (playbackState == StreamingPlaybackState.Playing || playbackState == StreamingPlaybackState.Buffering)
            {
                waveOut.Pause();
                Debug.WriteLine(String.Format("User requested Pause, waveOut.PlaybackState={0}", waveOut.PlaybackState));
                playbackState = StreamingPlaybackState.Paused;
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            StopPlayback();
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            Debug.WriteLine("Playback Stopped");
            if (e.Exception != null)
            {
                MessageBox.Show(String.Format("Playback Error {0}", e.Exception.Message));
            }
        }

        private void HFLive_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panelHome_Click(object sender, EventArgs e)
        {
            Animations.FadeOut(this);
        }

        private void panelPower_Click(object sender, EventArgs e)
        {
            Animations.FadeClose(this);
        }

        private void HFLive_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            StopPlayback();
        }

        private string URL;
        private void HFLive_Load(object sender, EventArgs e)
        {
            Animations.FadeIn(this);
            buttonPlay_Click(sender, e);
            URL = CheckUpdates();
        }

        private void panelUp_Click(object sender, EventArgs e)
        {
            if(volumeProvider.Volume < 1)
                volumeProvider.Volume += 0.1f;
        }

        private void panelDown_Click(object sender, EventArgs e)
        {
            if(volumeProvider.Volume != 0)
                volumeProvider.Volume -= 0.1f;
        }

        private float previous;
        private void panelMute_Click(object sender, EventArgs e)
        {
            if (volumeProvider.Volume != 0)
            {
                previous = volumeProvider.Volume;
                volumeProvider.Volume = 0;
            }
            else
                volumeProvider.Volume = previous;
        }

        private void labelUpdate_Click(object sender, EventArgs e)
        {
            Process.Start(URL);
        }

        private void labelTitle_SizeChanged(object sender, EventArgs e)
        {
            labelTitle.Left = (panelMeta.Width - labelTitle.Size.Width) / 2;
        }

        private async void panelCamera_Click(object sender, EventArgs e)
        {
            using (Bitmap bmp = new Bitmap(Width, Height))
            {
                DrawToBitmap(bmp, new Rectangle(Point.Empty, bmp.Size));
                var path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".png";
                bmp.MakeTransparent(BackColor);
                bmp.Save(path, ImageFormat.Png);
                bmp.Dispose();

                await UploadImage(path);
                new FileInfo(path).Delete();
            }
        }

        private void labelArtist_SizeChanged(object sender, EventArgs e)
        {
            labelArtist.Left = (panelMeta.Width - labelArtist.Size.Width) / 2;
        }
        #endregion
    }
}
