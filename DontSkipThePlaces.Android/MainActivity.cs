using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;
using Android.Support.V4.Content;
using Android.Support.V4.App;

namespace DontSkipThePlaces.Droid
{
    [Activity(Label = "DontSkipThePlaces", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
			const int RequestAccessFineLocation = 1;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);


			// check for acess permissions in Runtime
            var permissionCheck = ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation);
            if (!permissionCheck.Equals(Permission.Granted))
            {
                // there is no granted permission to ACCESS_FINE_LOCATION. Requesting it in runtime
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.AccessFineLocation }, RequestAccessFineLocation);
            }


            LoadApplication(new App());
        }
    }
}

