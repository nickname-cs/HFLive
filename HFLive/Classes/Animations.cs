using System.Threading.Tasks;
using System.Windows.Forms;

namespace HFLive
{
    class Animations
    {
        public static async void FadeIn(Form o, int interval = 25)
        {
            while (o.Opacity < 1.0)
            {
                await Task.Delay(interval);
                o.Opacity += 0.05;
            }
            o.Opacity = 1;
        }

        public static async void FadeOut(Form o, int interval = 25)
        {
            while (o.Opacity > 0.0)
            {
                await Task.Delay(interval);
                o.Opacity -= 0.05;
            }
            o.Opacity = 0;
            o.WindowState = FormWindowState.Minimized;
        }

        public static async void FadeClose(Form o, int interval = 25)
        {
            while (o.Opacity > 0.0)
            {
                await Task.Delay(interval);
                o.Opacity -= 0.05;
            }
            o.Opacity = 0;
            o.Close();
        }
    }
}
