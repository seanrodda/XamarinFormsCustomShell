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
            return new MyShellTabBarAppearanceTracker();
        }

        protected override IShellItemRenderer CreateShellItemRenderer(ShellItem item)
        {
            var renderer = new MyShellItemRenderer(this);
            renderer.ShellItem = item;
            return renderer;
        }
    }
}