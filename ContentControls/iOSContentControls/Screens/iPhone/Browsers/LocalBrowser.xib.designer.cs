// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Example_ContentControls.Screens.iPhone.Browsers {
	
	
	// Base type probably should be MonoTouch.UIKit.UIViewController or subclass
	[Foundation.Register("LocalBrowser")]
	public partial class LocalBrowser {
		
		private UIKit.UIView __mt_view;
		
		private UIKit.UIWebView __mt_webMain;
		
		#pragma warning disable 0169
		[Foundation.Connect("view")]
		private UIKit.UIView view {
			get {
				this.__mt_view = ((UIKit.UIView)(this.GetNativeField("view")));
				return this.__mt_view;
			}
			set {
				this.__mt_view = value;
				this.SetNativeField("view", value);
			}
		}
		
		[Foundation.Connect("webMain")]
		private UIKit.UIWebView webMain {
			get {
				this.__mt_webMain = ((UIKit.UIWebView)(this.GetNativeField("webMain")));
				return this.__mt_webMain;
			}
			set {
				this.__mt_webMain = value;
				this.SetNativeField("webMain", value);
			}
		}
	}
}
