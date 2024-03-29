﻿using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace CustomShell.iOS.Renderers
{
    public class MyShellTabBarAppearanceTracker : IShellTabBarAppearanceTracker
    {
        UIView _blurView;
		UIView _colorView;
		UIImage _defaultBackgroundImage;
		UIColor _defaultTint;
		UIColor _defaultUnselectedTint;
		bool _disposed = false;

		public void ResetAppearance(UITabBarController controller)
		{
			if (_blurView == null)
				return;

			var tabBar = controller.TabBar;
			tabBar.BackgroundImage = _defaultBackgroundImage;
			tabBar.TintColor = _defaultTint;
			tabBar.UnselectedItemTintColor = _defaultUnselectedTint;

			_blurView.RemoveFromSuperview();
			_colorView.RemoveFromSuperview();
		}

		public void SetAppearance(UITabBarController controller, ShellAppearance appearance)
		{
			IShellAppearanceElement el = appearance;
			var backgroundColor = el.EffectiveTabBarBackgroundColor;
			var foregroundColor = el.EffectiveTabBarForegroundColor;
			var titleColor = el.EffectiveTabBarTitleColor;
			var disabledColor = el.EffectiveTabBarDisabledColor;
			var unselectedColor = el.EffectiveTabBarUnselectedColor;

			var tabBar = controller.TabBar;

			if (_blurView == null)
			{
				_defaultBackgroundImage = tabBar.BackgroundImage;
				_defaultTint = tabBar.TintColor;
				_defaultUnselectedTint = tabBar.UnselectedItemTintColor;

				var effect = UIBlurEffect.FromStyle(UIBlurEffectStyle.Regular);
				_blurView = new UIVisualEffectView(effect);
				_blurView.Frame = tabBar.Bounds;

				_colorView = new UIView(_blurView.Frame);
			}

			tabBar.BackgroundImage = new UIImage();

			tabBar.InsertSubview(_colorView, 0);
			tabBar.InsertSubview(_blurView, 0);

            var button = new UIButton(UIButtonType.Custom);
            button.SetTitle("test", UIControlState.Normal);
            button.Frame = tabBar.Bounds;
            button.Center = tabBar.Center;
            tabBar.InsertSubview(button, 1);

			if (!backgroundColor.IsDefault)
				_colorView.BackgroundColor = backgroundColor.ToUIColor();
			if (!foregroundColor.IsDefault)
				tabBar.TintColor = foregroundColor.ToUIColor();
			if (!unselectedColor.IsDefault)
				tabBar.UnselectedItemTintColor = unselectedColor.ToUIColor();
		}

		public void UpdateLayout(UITabBarController controller)
		{
			if (_blurView != null)
				_blurView.Frame = controller.TabBar.Bounds;
			if (_colorView != null)
				_colorView.Frame = _blurView.Frame;
		}

		#region IDisposable Support

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (_blurView != null)
				{
					_blurView.RemoveFromSuperview();
					_blurView.Dispose();
				}

				if (_colorView != null)
				{
					_colorView.RemoveFromSuperview();
					_colorView.Dispose();
				}

				_blurView = null;
				_colorView = null;

				_disposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
		}
#endregion
    }
}