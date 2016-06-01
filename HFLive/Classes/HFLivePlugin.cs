using Orcus.Administration.Plugins;
using System.Windows.Forms;

namespace HFLive
{
    class HFLivePlugin : IAdministrationPlugin
    {
        public void Initialize(IUiModifier uiModifier, IAdministrationControl administrationControl)
        {
            uiModifier.AddMainMenuItem(new System.Windows.Controls.MenuItem { Header = "HF Live" }, MenuEventHandler);
        }

        private void MenuEventHandler(object sender, MenuItemClickedEventArgs menuItemClickedEventArgs)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is HFLive)
                {
                    f.Focus();
                    return;
                }
            }
            new HFLive().Show();
        }
    }
}
