using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace HFLive
{
    public class RadioStream : Stream
    {
        private int metaInt;
        private int receivedBytes;
        private Stream myStream;
        private bool connected = false;

        private string URL { get; set; }
        public string streamTitle;
        public HFLive Caller;
        public event EventHandler StreamTitleChanged;

        public RadioStream(string url)
        {
            URL = url;
        }
        public bool Connect()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
                request.Headers.Clear();
                request.Headers.Add("Icy-MetaData", "1");
                request.KeepAlive = false;
                request.UserAgent = "WinampMPEG/5.09";

                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

                metaInt = int.Parse(resp.Headers["Icy-MetaInt"]);
                receivedBytes = 0;

                myStream = resp.GetResponseStream();

                connected = true;

            }
            catch { connected = false; }
            return connected;
        }
        private void GetMetaInfo(byte[] metaInfo)
        {
            string metaString = @Encoding.UTF8.GetString(metaInfo);

            string[] newStreamTitle = Regex.Match(metaString, "StreamTitle='(.*?)';").Groups[1].Value.Trim().Split('-');
            streamTitle = (newStreamTitle[0].Trim());
            Caller.UpdateMyLabel(newStreamTitle[1].Trim(), newStreamTitle[0].Trim());
            if (!newStreamTitle.Equals(streamTitle))
            {
                streamTitle = newStreamTitle[0].Trim();
                OnStreamTitleChanged();
            }
        }

        protected virtual void OnStreamTitleChanged()
        {
            StreamTitleChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool IsConnected
        {
            get { return connected; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            try
            {
                if (receivedBytes == metaInt)
                {
                    int metaLen = myStream.ReadByte();
                    if (metaLen > 0)
                    {
                        byte[] metaInfo = new byte[metaLen * 16];
                        int len = 0;
                        while ((len += myStream.Read(metaInfo, len, metaInfo.Length - len)) < metaInfo.Length) { }
                        GetMetaInfo(metaInfo);
                    }
                    receivedBytes = 0;
                }

                int bytesLeft = ((metaInt - receivedBytes) > count) ? count : (metaInt - receivedBytes);
                int result = myStream.Read(buffer, offset, bytesLeft);
                receivedBytes += result;
                return result;
            }
            catch (Exception e)
            {
                connected = false;
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        #region "Not Implemented"
        public override bool CanRead
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool CanSeek
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool CanWrite
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override long Length
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
