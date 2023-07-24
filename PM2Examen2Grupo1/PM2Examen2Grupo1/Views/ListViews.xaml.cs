using Acr.UserDialogs;
using Plugin.AudioRecorder;
using PM2Examen2Grupo1.Controller;
using PM2Examen2Grupo1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Android.App;
using PM2Examen2Grupo1.Converter;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using PM2Examen2Grupo1.Services;

namespace PM2Examen2Grupo1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViews : ContentPage
    {
        public Sitios Site;
        byte[] audioBytes;
        byte[] FotoBytes;

        PlayService playbyte = new PlayService();

        bool editando = false;
        Base64Converter converter = new Base64Converter();

        private readonly AudioPlayer audioPlayer = new AudioPlayer();
        public ListViews()
        {
            InitializeComponent();
            llamarAPi();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (editando)
            {
                llamarAPi();

                editando = false;

                Site = null;
            }
        }







        private void BtnAtras(object sender, EventArgs e)
        {
            OnBackButtonPressed();
        }

        private void Eliminar_Clicked(object sender, EventArgs e)
        {
            // Implementa el código para eliminar aquí
        }

        private void Actualizar_Clicked(object sender, EventArgs e)
        {

            if (Site == null) {

                new NavigationPage(new UpdateSite(Site));

            }

           


           // new NavigationPage(new Views.UpdateSite(listSites.ItemSelected));
        }

        private void VerMapa_Clicked(object sender, EventArgs e)
        {
            // Implementa el código para ver el mapa aquí
        }

        private void EscucharAudio_Clicked(object sender, EventArgs e)
        {
            // Implementa el código para escuchar el audio aquí
        }
         
        private void Button_Clicked (object sender, EventArgs e)
        {
            
        }

        private void PlayButton_Clicked(object sender, EventArgs e)
        {
            if (Site != null)
            {

                playbyte.Play(Site.audio);

            }
            else
            {

                DisplayAlert("Aviso", "Debe Seleccionar un Sitio", "Ok");

            }


        }


        public async void llamarAPi()
        {

            try
            {
                var current = Connectivity.NetworkAccess;

                if (current != NetworkAccess.Internet)
                {
                    Message("Advertencia", "Actualmente no cuenta con acceso a internet");
                    return;
                }
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri("http://3.14.29.24/api_Rest/SitiosGetList.php");
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accept", "application/json");

                var cliente = new HttpClient();
                HttpResponseMessage response = await cliente.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    
                   var resultado = JsonConvert.DeserializeObject<List<Sitios>>(content);


                    foreach (var sitio in resultado)
                    {
                        sitio.audio = converter.ConvertBase64ToByteArray(sitio.Audiofile);
                        sitio.foto = converter.ConvertBase64ToByteArray(sitio.Firmadigital);

  
                     };
                    //Envio de los datos a un ListView 
                    listSites.ItemsSource = resultado;
                        


                 }

                    

                
            }
            catch(Exception ex)
            {
                await DisplayAlert("Äviso", $"{ex}", "Ok");
            }

        }




        private void listSites_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                Site = e.Item as Sitios;

                
            }
            catch (Exception ex)
            {
                Message("Error:", ex.Message);
            }
        }


        
        private async void btnDelete_Clicked (object sender, EventArgs e)
        {
            try
            {
                if (Site == null)
                {
                    Message("Aviso", "Seleccione un sitio");
                    return;
                }

                bool response = await DisplayAlert("Aviso", "Seleccione la accion que desea realizar", "Eliminar", "Actualizar");

                if (response)
                {
                    //Delete
                    var sit = Site;
                    DeleteSite(sit);
                }
                else
                {

                    editando = true;

                    await Navigation.PushModalAsync(new UpdateSite(Site));


                }
            }
            catch (Exception ex)
            {
                Message("Error:", ex.Message);
            }
        }

        private void btnViewListen_Clicked(Object sender, EventArgs e)
        {
            if (Site != null)
            {

                playbyte.Play(Site.audio);

            }
            else
            {

                DisplayAlert("Aviso", "Debe Seleccionar un Sitio", "Ok");

            }
        }

        private void btnViewListen_Clicked_1(object sender, EventArgs e)
        {
            if(Site != null)
            {
               
                playbyte.Play(Site.audio);

            }
            else
            {

                DisplayAlert("Aviso", "Debe Seleccionar un Sitio", "Ok");

            }


            
        }


        private async void DeleteSite(Sitios site)
        {
            var status = await DisplayAlert("Aviso", $"¿Desea eliminar el sitio con Descripcion: {site.Descripcion}?", "SI", "NO");

            if (status)
            {
                var result = await ControllerSitios.DeleteSite(site.Id.ToString());

                if (result)
                {
                    Message("Aviso", "El sitio fue eliminado correctamente");
                    Site = null;
                    llamarAPi();

                    Site = null;
                }
                else
                {
                    Message("Aviso", "No se pudo eliminar el sitio");
                }
            }
        }


        public void UpdateSite(Sitios sitio)
        {
            InitializeComponent();
            this.Site = sitio;
        }

        private async void Message(string title, string message)
        {
            await DisplayAlert(title, message, "OK");
        }

        private void btnViewMapa_Clicked_1(object sender, EventArgs e)
        {
            openMapa();
        }

        public async void openMapa() {
            try {

                if (Site != null) {

                    var location = new Location(Site.Latitud, Site.Longitud);
                    var option = new MapLaunchOptions { Name = "Sitios",
                        NavigationMode = NavigationMode.Driving
                    };

                    await Map.OpenAsync(location, option);

                }
                else
                {

                  await DisplayAlert("Aviso", "No ha Seleccionado un Sitio", "Ok");
                }



            } catch(Exception ex) { 
            
            
            }


        }

    }

    
}