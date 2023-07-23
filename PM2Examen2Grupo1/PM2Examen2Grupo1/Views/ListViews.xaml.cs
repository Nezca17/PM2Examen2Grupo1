using Acr.UserDialogs;
using Plugin.AudioRecorder;
using PM2Examen2Grupo1.Controller;
using PM2Examen2Grupo1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2Examen2Grupo1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViews : ContentPage
    {
        public Sitios Site;

        bool editando = false;

        private readonly AudioPlayer audioPlayer = new AudioPlayer();
        public ListViews()
        {
            InitializeComponent();
           // LoadData();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (editando)
            {
              //  LoadData();

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
            // Implementa el código para actualizar aquí
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
        /*
        private async void btnDelete_Clicked (object sender, EventArgs e)
        {
            try
            {
                if (Site == null)
                {
                    Message("Aviso", "Seleccione un sitio");
                    return;
                }

                bool response = await Application.Current.MainPage.DisplayAlert("Aviso", "Seleccione la accion que desea realizar", "Eliminar", "Actualizar");

                if (response)
                {
                    //Delete
                    var sit = Site;
                    DeleteSite(Site);
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
         private void btnViewMapa_Clicked(Object sender, EventArgs e)
        {

        }

        private void btnViewListen_Clicked(Object sender, EventArgs e)
        {

        }

        private void listSites_ItemTapped (object sender, ItemTappedEventArgs e)
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

        private async void LoadData()
        {

            try
            {
                await Task.Delay(1000);

                UserDialogs.Instance.ShowLoading("Cargando", MaskType.Gradient);

                listSites.ItemsSource = await ControllerSitios.GetAllSite();

                await Task.Delay(500);
                UserDialogs.Instance.HideLoading();
              //  Message("Advertencia", listSites.ItemsSource.ToString());

                var current = Connectivity.NetworkAccess;

                if (current != NetworkAccess.Internet)
                {
                    Message("Advertencia", "Actualmente no cuenta con acceso a internet");
                    return;
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                await Task.Delay(500);

                Message("Error: ", ex.Message);
            }
        }
        /*
        private async void DeleteSite(Sitios site)
        {
            var status = await DisplayAlert("Aviso", $"¿Desea eliminar el sitio con Descripcion: {site.descripcion}?", "SI", "NO");

            if (status)
            {
                var result = await ControllerSitios.DeleteSite(site.id.ToString());

                if (result)
                {
                    //Message("Aviso", "El sitio fue eliminado correctamente");
                    Site = null;
                    LoadData();

                    Site = null;
                }
                else
                {
                    Message("Aviso", "No se pudo eliminar el sitio");
                }
            }
        }
     /*   public UpdateSite(Sitios sitio)
        {
            InitializeComponent();
           // this.sitio = sitio;
        }*/

        private async void Message(string title, string message)
        {
            await DisplayAlert(title, message, "OK");
        }

    }

    
}