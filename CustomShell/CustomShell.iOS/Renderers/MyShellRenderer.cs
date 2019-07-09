using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomShell.AppShell), typeof(CustomShell.iOS.Renderers.MyShellRenderer))]
namespace CustomShell.iOS.Renderers
{
    public class MyShellRenderer : ShellRenderer
    {
        protected override IShellTabBarAppearanceTracker CreateTabBarAppearanceTracker()
        {
            return base.CreateTabBarAppearanceTracker();
        }
    }
}