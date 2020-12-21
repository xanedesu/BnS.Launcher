using System.Windows.Forms;

namespace Unlakki.Bns.Launcher.Components.Router
{
    public partial class RoutableComponent : UserControl
    {
        protected Router Router { get; private set; }

        public void Connect(Router router)
        {
            Router = router;
        }
    }
}
