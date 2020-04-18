﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Android.V8;
using WebAtoms;

namespace DroidV8Test.Droid
{
    public class AndroidSystem
    {

        public Activity Activity { get; set; }

        public void Log(string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
        }
    }

    [Activity(Label = "DroidV8Test", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        private JSContext jc;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());


            var sys = new AndroidSystem();
            sys.Activity = this;
            jc = new JSContext(true);

            jc["system"] = jc.Serialize(sys, SerializationMode.WeakReference);

            jc["log"] = jc.CreateFunction(0, (c, a) => {
                System.Diagnostics.Debug.WriteLine(a[0]);
                return c.Undefined;
            }, "systemLogger");

            jc.Evaluate("console.log('hey')");
            // jc.Evaluate("setTimeout(function() { console.log('1'); },1);");
            // jc.Evaluate(@"new Promise(function(r,e) {})");

            //this.RunOnUiThread(async () => {

            //    await BaseTest.RunAll();

            //});

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}