using System;

using OpenTK.Graphics;
using OpenTK.Graphics.ES30;
using OpenTK.Platform.Android;
using OpenTK;

using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Mono.Samples.TexturedCube {

	class PaintingView : AndroidGameView
	{
		float prevX, prevY;
		float downX, downY;
		bool setViewport = true;

		Cube cube = new Cube ();

		public PaintingView (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			Initialize ();
		}

		public PaintingView (IntPtr handle, Android.Runtime.JniHandleOwnership transfer)
			: base (handle, transfer)
		{
			Initialize ();
		}

		private void Initialize ()
		{
			AutoSetContextOnRenderFrame = false;
			RenderOnUIThread = false;
			Resize += delegate {
				cube.SetupProjection (Width, Height);
				setViewport = true;
			};
		}

		// This method is called everytime the context needs
		// to be recreated. Use it to set any egl-specific settings
		// prior to context creation
		protected override void CreateFrameBuffer ()
		{
			ContextRenderingApi = GLVersion.ES3;

			try {
				Log.Verbose ("TexturedCube", "Loading with high quality settings");

				GraphicsMode = new GraphicsMode (new ColorFormat (32), 24, 0, 4); 
				// if you don't call this, the context won't be created
				base.CreateFrameBuffer ();
				return;
			} catch (Exception ex) {
				Log.Verbose ("TexturedCube", "{0}", ex);
			}

			// the default GraphicsMode that is set consists of (16, 16, 0, 0, 2, false)
			try {
				Log.Verbose ("TexturedCube", "Loading with default settings");

				// if you don't call this, the context won't be created
				base.CreateFrameBuffer ();
				return;
			} catch (Exception ex) {
				Log.Verbose ("TexturedCube", "{0}", ex);
			}

			// Fallback modes
			// If the first attempt at initializing the surface with a default graphics
			// mode fails, then the app can try different configurations. Devices will
			// support different modes, and what is valid for one might not be valid for
			// another. If all options fail, you can set all values to 0, which will
			// ask for the first available configuration the device has without any
			// filtering.
			// After a successful call to base.CreateFrameBuffer(), the GraphicsMode
			// object will have its values filled with the actual values that the
			// device returned.


			// This is a setting that asks for any available 16-bit color mode with no
			// other filters. It passes 0 to the buffers parameter, which is an invalid
			// setting in the default OpenTK implementation but is valid in some
			// Android implementations, so the AndroidGraphicsMode object allows it.
			try {
				Log.Verbose ("TexturedCube", "Loading with custom Android settings (low mode)");
				GraphicsMode = new AndroidGraphicsMode (16, 0, 0, 0, 0, false);

				// if you don't call this, the context won't be created
				base.CreateFrameBuffer ();
				return;
			} catch (Exception ex) {
				Log.Verbose ("TexturedCube", "{0}", ex);
			}

			// this is a setting that doesn't specify any color values. Certain devices
			// return invalid graphics modes when any color level is requested, and in
			// those cases, the only way to get a valid mode is to not specify anything,
			// even requesting a default value of 0 would return an invalid mode.
			try {
				Log.Verbose ("TexturedCube", "Loading with no Android settings");
				GraphicsMode = new AndroidGraphicsMode (0, 4, 0, 0, 0, false);

				// if you don't call this, the context won't be created
				base.CreateFrameBuffer ();
				return;
			} catch (Exception ex) {
				Log.Verbose ("TexturedCube", "{0}", ex);
			}
			throw new Exception ("Can't load egl, aborting");
		}

		void LoadBitmapData (int texId)
		{
			Bitmap b = BitmapFactory.DecodeResource (Context.Resources, Resource.Drawable.texture1);
			Android.Opengl.GLUtils.TexImage2D ((int)All.Texture2D, 0, b, 0); 
			b.Recycle ();
		}

		protected override void OnContextSet (EventArgs e)
		{
			base.OnContextSet (e);
			Console.WriteLine ("OpenGL version: {0} GLSL version: {1}", GL.GetString (StringName.Version), GL.GetString (StringName.ShadingLanguageVersion));
			cube.Initialize ();
			cube.LoadTexture (LoadBitmapData);
		}

		protected override void OnRenderThreadExited (EventArgs e)
		{
			base.OnRenderThreadExited (e);

			global::Android.App.Application.SynchronizationContext.Send (_ => {
				Console.WriteLine ("render thread exited\nexception:\n{0}", RenderThreadException);
				TextView view = ((LinearLayout) Parent).FindViewById (Resource.Id.TextNotSupported) as TextView;
				view.LayoutParameters = new LinearLayout.LayoutParams (LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
				view.Visibility = ViewStates.Visible;
				Parent.RequestLayout ();
			}, null);
		}

		protected override void OnLoad (EventArgs e)
		{
			cube.SetupProjection (Width, Height);

			Run (60);
		}

		void Render ()
		{
			cube.Render ();
			SwapBuffers ();
		}

		bool touch = false;
		bool touchDown = false;

		public override bool OnTouchEvent (MotionEvent e)
		{
			base.OnTouchEvent (e);
			if (e.Action == MotionEventActions.Down) {
				downX = prevX = e.GetX ();
				downY = prevY = e.GetY ();
				touch = true;
				touchDown = true;
			}
			if (e.Action == MotionEventActions.Move) {
				float eX = e.GetX ();
				float eY = e.GetY ();
				float xDiff = (prevX - eX);
				float yDiff = (prevY - eY);
				prevX = eX;
				prevY = eY;

				cube.Move (xDiff, yDiff);
			}
			if (System.Math.Abs (downX - e.GetX ()) > 5 || System.Math.Abs (downY - e.GetY ()) > 5)
				touch = false;
			if (e.Action == MotionEventActions.Move)
				cube.SetupProjection (Width, Height);
			else if (e.Action == MotionEventActions.Up) {
				if (touch)
					cube.ToggleTexture ();
				touchDown = false;
			}

			return true;
		}

		protected override void OnRenderFrame (FrameEventArgs e)
		{
			base.OnRenderFrame (e);
			if (!touchDown)
				cube.UpdateWorld ();
			if (setViewport) {
				setViewport = false;
				GL.Viewport (0, 0, Width, Height);
			}
			cube.Render ();
			SwapBuffers ();
		}

		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);
			cube.DeleteTexture ();
		}
	}
}
