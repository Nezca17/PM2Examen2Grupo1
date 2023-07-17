using Plugin.Media.Abstractions;
using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PM2Examen2Grupo1.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2Examen2Grupo1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaginaPrincipal : ContentPage
	{
		public PaginaPrincipal ()
		{
			InitializeComponent ();
            LocalizameCommand = new Command(Localizar);
        }

       // public String Getimage64() 
        //{
           // if (firma != null)
            //{ 
              //using (MemoryStream memory = new MemoryStream))
               // {
                  //  Stream stream = firma.GetStream();
                    //Stream.CopyTo(memory);
                    //byte[] fotobyte = memory.ToArray();

                  //  String Base64 = Convert.ToBase64String(fotobyte);
                  //  return Base64;
               // }
            //}
           // return null;
       // }

        private async void btnfirmar_Clicked(object sender, EventArgs e)
        {
            Stream image = await PadView.GetImageStreamAsync(SignatureImageFormat.Jpeg);
        }

        private void grabarvoz_Clicked(object sender, EventArgs e)
        {

        }

        private void detenervoz_Clicked(object sender, EventArgs e)
        {

        }

        private void btnsalvar_Clicked(object sender, EventArgs e)
        {


        }

        private void btnubicaciones_Clicked(object sender, EventArgs e)
        {

        }

        private async void Localizar()
        {
            try
            {
                var localizacion = await Geolocation.GetLastKnownLocationAsync();
                if (localizacion == null)
                {
                    localizacion = await Geolocation.GetLocationAsync(new GeolocationRequest()
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(25)
                    });
                }
                if (localizacion == null)
                {
                    Error = "No se donde estoy";
                }
                else
                {
                    Longitud = localizacion.Longitude;
                    Latitud = localizacion.Latitude;
                }

            }
            catch (Exception e)
            {

                Console.WriteLine(e.StackTrace);
            }

        }

    }

}