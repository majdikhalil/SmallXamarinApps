// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Example_ContentControls.Screens.iPhone.Maps {
	
	
	// Base type probably should be MonoTouch.UIKit.UIViewController or subclass
	[Foundation.Register("BasicMapScreen")]
	public partial class BasicMapScreen {
		
		private UIKit.UIView __mt_view;
		
		private MapKit.MKMapView __mt_mapMain;
		
		private UIKit.UISegmentedControl __mt_sgmtMapType;
		
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
		
		[Foundation.Connect("mapMain")]
		private MapKit.MKMapView mapMain {
			get {
				this.__mt_mapMain = ((MapKit.MKMapView)(this.GetNativeField("mapMain")));
				return this.__mt_mapMain;
			}
			set {
				this.__mt_mapMain = value;
				this.SetNativeField("mapMain", value);
			}
		}
		
		[Foundation.Connect("sgmtMapType")]
		private UIKit.UISegmentedControl sgmtMapType {
			get {
				this.__mt_sgmtMapType = ((UIKit.UISegmentedControl)(this.GetNativeField("sgmtMapType")));
				return this.__mt_sgmtMapType;
			}
			set {
				this.__mt_sgmtMapType = value;
				this.SetNativeField("sgmtMapType", value);
			}
		}
	}
}
