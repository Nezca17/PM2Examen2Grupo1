using Plugin.Media.Abstractions;
using Acr.UserDialogs;
using SignaturePad.Forms;
using System;
using System.Threading.Tasks;
using PM2Examen2Grupo1.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Plugin.Media;
using Plugin.AudioRecorder;
using System.IO;
using PM2Examen2Grupo1.Converter;
using System.Xml.Linq;

namespace PM2Examen2Grupo1.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaPrincipal : ContentPage
    {
        static string DEFAULTPATH = "/storage/emulated/0/Android/data/com.companyname.pm2examen2grupo1/files";

        string base64StringImagen;
        string base64StringAudio;

        private AudioPlayer audioPlayer = new AudioPlayer();
        private bool reproducir = false;
        private AudioRecorderService audioRecorderService = new AudioRecorderService()
        {
            //  FilePath = DEFAULTPATH,
            StopRecordingOnSilence = false,
            StopRecordingAfterTimeout = false
          
        };



        public Command LocalizameCommand { get; set; }
        private LocalizacionModel1 ObjLocalizar;

            public PaginaPrincipal()
            {
                InitializeComponent();
                LocalizameCommand = new Command(Localizar);

                ObjLocalizar = new LocalizacionModel1();
                Localizar();
      
             }
        //   public String Getimage64() 
        //{
        // if (firma != null)
        //{ 
        //using (MemoryStream memory = new MemoryStream))
        //  {
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

        }


        private async void btnEscuchar_Clicked(object sender, EventArgs e) {

          await  EscucharAudio();
        }


        private async void grabarvoz_Clicked(object sender, EventArgs e)
        {
           await Grabaraudio();


        }

        private async void btnsalvar_Clicked(object sender, EventArgs e)
        {

           await EnviarDatosABD();

        }

        private async void btnubicaciones_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListViews());
            
        }

        public async Task EnviarDatosABD()
        {
            try{
                /*Guardando el Pad como Imagen*/
                Stream firmaIMG = await PadView.GetImageStreamAsync(SignatureImageFormat.Jpeg, strokeColor: Color.Black, fillColor: Color.White);
                string signName ="Prueba" + ".jpeg";

                if (await OnSaveSignature(firmaIMG, signName))
                {
                    await DisplayAlert("Aviso", "Firma Guardada", "OK");
                    ClearInputs();
                }

                /*Convertir el Audio en Base 64 preparandolo para enviarlo a la BD*/

                string Audio = audioRecorderService.GetAudioFilePath(); // Reemplaza "ruta_del_archivo.txt" con la ruta real del archivo que deseas convertir
                 base64StringAudio = Base64Converter.FileToBase64(Audio);

                // Reemplaza "ruta_del_archivo.txt" con la ruta real del archivo que deseas convertir
             



                if (base64StringAudio != null)
                {
                    // Haz algo con la cadena Base64, como enviarla a través de una API o mostrarla en un cuadro de texto, etc.


                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
                System.Diagnostics.Debug.WriteLine(ex.Message);

            }
        }

        private void ClearInputs()
        {
            PadView.Clear();
            txtAdescripcion.Text = "";
           
        }

        private async Task<bool> OnSaveSignature(Stream bitmap, string filename)
        {
            var file = Path.Combine(DEFAULTPATH, filename);
            try
            {

                 base64StringImagen = Base64Converter.FileToBase64(file);


                using (var dest = File.OpenWrite(file))
                {
                    await bitmap.CopyToAsync(dest);
                }

                var mStream = (MemoryStream)bitmap;
                byte[] data = mStream.ToArray();

                /* var datosFirma = new Firma
                 {
                     id = 0,
                     name = txbName.Text,
                     descrip = txbDescrip.Text,
                     sign = data
                 };
                 await App.DBase.guardarFirma(datosFirma);*/
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return true;
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
                    ObjLocalizar.Error = "No se donde estoy";
                }
                else
                {
                    ObjLocalizar.Longitud = localizacion.Longitude;
                    ObjLocalizar.Latitud = localizacion.Latitude;
                    txtlatitud.Text=  localizacion.Latitude.ToString();
                    txtlongitud.Text = localizacion.Longitude.ToString();

                }

            }
            catch (Exception e)
            {

                Console.WriteLine(e.StackTrace);
            }

        }

        public byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public async Task Grabaraudio()
        {

            try
            {
                var status = await Permissions.RequestAsync<Permissions.Microphone>();
                var status2 = await Permissions.RequestAsync<Permissions.StorageRead>();
                var status3 = await Permissions.RequestAsync<Permissions.StorageWrite>();
                if (status != PermissionStatus.Granted && status2 != PermissionStatus.Granted && status3 != PermissionStatus.Granted)
                {
                    return; // si no tiene los permisos no avanza
                }

                if (audioRecorderService.IsRecording)
                {

                    //audioRecorderService.set
                    //audioRecorderService.FilePath = "/storage/emulated/0/Android/data/com.companyname.ejercicio2_4/files/";
                    await audioRecorderService.StopRecording();
                    await UserDialogs.Instance.AlertAsync("Aviso", $"{audioRecorderService.GetAudioFilePath()}", "Aceptar");




                    btnEscucharAudio.IsVisible = true;
                    TxtGrabacion.Text = "No esta grabando";

                    TxtGrabacion.TextColor = Color.Red;

                    btngrabarvoz.Text = "Grabar audio";

                    reproducir = true;


                }
                else
                {
                    // await UserDialogs.Instance.AlertAsync("Grabando", "Aviso", "Aceptar");

                   // audioRecorderService.FilePath = DEFAULTPATH;


                    await audioRecorderService.StartRecording();


                    //await audioRecorderService.StartRecording();


                    TxtGrabacion.Text = "Esta grabando";

                    TxtGrabacion.TextColor = Color.Green;

                    btngrabarvoz.Text = "Dejar de Grabar";

                    // reproducir = false;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
                System.Diagnostics.Debug.WriteLine(ex.Message);

            }
        }

        public async Task EscucharAudio()
        {

            try
            {

                audioPlayer.Play(audioRecorderService.GetAudioFilePath());

            }
            catch (Exception ex)
            {

                await DisplayAlert("Aviso", $"{ex}", "Ok");

            }
        }


        private Byte[] ConvertAudioToByteArray()
        {
            Stream audioFile = audioRecorderService.GetAudioFileStream();

            var mStream = new MemoryStream(File.ReadAllBytes(audioRecorderService.GetAudioFilePath()));
            var mStreama = (MemoryStream)audioFile;

            Byte[] bytes = ReadFully(audioFile);
            return bytes;
        }



    }



}