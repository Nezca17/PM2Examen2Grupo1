using Plugin.Media.Abstractions;
using Acr.UserDialogs;
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
using Xamarin.Essentials;
using Plugin.Media;
using Plugin.AudioRecorder;
using System.IO;
using Plugin.FilePicker.Abstractions;

namespace PM2Examen2Grupo1.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaPrincipal : ContentPage
    {

        private AudioRecorderService audioRecorderService = new AudioRecorderService()
        {
            StopRecordingOnSilence = false,
            StopRecordingAfterTimeout = false
        };

        private AudioPlayer audioPlayer = new AudioPlayer();
        private bool reproducir = false;

        string internalFilePath;
        string internalStoragePath;
        string selectedFolderPath;

        

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
        private void AudioCaptured(byte[] audioData)
            {
                // Aquí puedes procesar los datos de audio capturados
                // El parámetro 'audioData' contiene los datos de audio en formato de bytes

                // Ejemplo: Guardar los datos de audio en un archivo WAV
                string filePath = "audio.wav";
                System.IO.File.WriteAllBytes(filePath, audioData);

                Console.WriteLine("Audio guardado en: " + filePath);
            }

            private async void btnfirmar_Clicked(object sender, EventArgs e)
            {

            }

            private Byte[] ConvertAudioToByteArray()
            {
                Stream audioFile = audioRecorderService.GetAudioFileStream();

                //var mStream = new MemoryStream(File.ReadAllBytes(audioRecorderService.GetAudioFilePath()));
                //var mStream = (MemoryStream)audioFile;

                Byte[] bytes = ReadFully(audioFile);
                return bytes;
            }


        private async void grabarvoz_Clicked(object sender, EventArgs e)
        {
            try
            {


                var status = await Permissions.RequestAsync<Permissions.Microphone>();
                var status2 = await Permissions.RequestAsync<Permissions.StorageRead>();
                var status3 = await Permissions.RequestAsync<Permissions.StorageWrite>();
                if (status != PermissionStatus.Granted & status2 != PermissionStatus.Granted & status3 != PermissionStatus.Granted)
                {
                    return; // si no tiene los permisos no avanza
                }

                if (audioRecorderService.IsRecording)
                {
                    await audioRecorderService.StopRecording();
                    //  await UserDialogs.Instance.AlertAsync("Guardando", "Aviso", "Aceptar");
                    
                    audioPlayer.Play(audioRecorderService.GetAudioFilePath());


                    TxtGrabacion.Text = "No esta grabando";

                    TxtGrabacion.TextColor = Color.Red;

                    btngrabarvoz.Text = "Grabar audio";

                    reproducir = true;


                }
                else
                {
                    // await UserDialogs.Instance.AlertAsync("Grabando", "Aviso", "Aceptar");

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
            }
        }




        private void detenervoz_Clicked(object sender, EventArgs e)
        {
         
        }

        private void btnsalvar_Clicked(object sender, EventArgs e)
        {


        }

        private async void btnubicaciones_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListViews());
            
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


    }



}