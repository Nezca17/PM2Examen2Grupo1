using System;
using Xamarin.Essentials;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Acr.UserDialogs;
using AndroidX.Core.App;
using AndroidX.Core.Content;

using Android;
using System.IO;

namespace PM2Examen2Grupo1.Droid
{
    [Activity(Label = "PM2Examen2Grupo1", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        private const int RequestStoragePermissionCode = 1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            UserDialogs.Init(this);
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            CheckAndRequestStoragePermissions();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void CheckAndRequestStoragePermissions()
        {
            string[] permissions = new string[]
            {
                Manifest.Permission.ReadExternalStorage,
                Manifest.Permission.WriteExternalStorage
            };

            // Verificar si los permisos ya han sido concedidos
            if (ContextCompat.CheckSelfPermission(this, permissions[0]) == (int)Permission.Granted &&
                ContextCompat.CheckSelfPermission(this, permissions[1]) == (int)Permission.Granted)
            {
                string customPath = "/storage/emulated/0/Android/data/com.companyname.ejercicio2_4/files/";
                string fullPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, customPath);

            }
            else
            {
                // Solicitar permisos
                ActivityCompat.RequestPermissions(this, permissions, RequestStoragePermissionCode);
            }
        }


    }

}