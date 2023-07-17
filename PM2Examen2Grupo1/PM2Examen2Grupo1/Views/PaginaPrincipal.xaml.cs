using Plugin.Media.Abstractions;
using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using Plugin.Media;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2Examen2Grupo1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaginaPrincipal : ContentPage
	{
        Plugin.Media.Abstractions.MediaFile firma = null;
		public PaginaPrincipal ()
		{
			InitializeComponent ();
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
    }
}