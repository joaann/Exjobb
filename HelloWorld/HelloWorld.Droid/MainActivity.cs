using Android.App;
using Android.Content.PM;
using Android.OS;
using HelloWorld;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XLabs.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Geolocation;
namespace HelloWorld.Droid
{
    [Activity(Label = "HelloWorld", Icon = "@drawable/icon", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : XFormsApplicationDroid
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var resolverContainer = new SimpleContainer();

            var app = new XFormsAppDroid();
            app.Init(this);

            resolverContainer.Register<IDevice>(t => AndroidDevice.CurrentDevice);
            resolverContainer.Register<IGeolocator>(t => new XLabs.Platform.Services.Geolocation.Geolocator());

            Resolver.SetResolver(resolverContainer.GetResolver());

            Forms.Init(this, bundle);

            App.Init();

            this.SetPage(App.GetMainPage());
        }
    }
}