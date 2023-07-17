using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using Plugin.AudioRecorder;
using System.IO;

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


        public PaginaPrincipal()
        {
            InitializeComponent();
      
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

        private void btnfirmar_Clicked(object sender, EventArgs e)
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


        private void grabarvoz_Clicked(object sender, EventArgs e)
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


                    audioPlayer.Play(audioRecorderService.GetAudioFilePath());

                    txtMessage.Text = "No esta grabando";

                    txtMessage.TextColor = Color.Red;

                    btnGrabar.Text = "Grabar audio";

                    reproducir = true;
                }
                else
                {
                    await audioRecorderService.StartRecording();


                    txtMessage.Text = "Esta grabando";

                    txtMessage.TextColor = Color.Green;

                    btnGrabar.Text = "Dejar de Grabar";

                    //reproducir = false;
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

        private void btnubicaciones_Clicked(object sender, EventArgs e)
        {

        }
    }
}
