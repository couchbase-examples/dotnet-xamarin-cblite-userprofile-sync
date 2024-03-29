﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using CouchbaseLabs.MVVM;
using UserProfileDemo.Droid.Services;
using UserProfileDemo.Repositories.Services;

namespace UserProfileDemo.Droid
{
    [Activity(
        Label = "UserProfileDemo",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity
        : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
#if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
#endif
            // tag::activate[]
            Couchbase.Lite.Support.Droid.Activate(this);
            // end::activate[]

            RegisterServices();
            LoadApplication(new App());
        }

        void RegisterServices()
        {
            ServiceContainer.Register<IDatabaseSeedService>(() => new DatabaseSeedService(ApplicationContext));
        }
    }
}