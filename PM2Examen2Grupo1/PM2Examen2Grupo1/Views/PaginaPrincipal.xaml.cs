﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PM2Examen2Grupo1.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2Examen2Grupo1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaginaPrincipal 
    {
        public Command LocalizameCommand { get; set; }
        private LocalizacionModel1 ObjLocalizar;
        public PaginaPrincipal ()
		{
			InitializeComponent ();
            LocalizameCommand = new Command(Localizar);

            ObjLocalizar = new LocalizacionModel1();

        }

        private void btnfirmar_Clicked(object sender, EventArgs e)
        {

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
                    ObjLocalizar.Error = "No se donde estoy";
                }
                else
                {
                    ObjLocalizar.Longitud = localizacion.Longitude;
                    ObjLocalizar.Latitud = localizacion.Latitude;
                }

            }
            catch (Exception e)
            {

                Console.WriteLine(e.StackTrace);
            }

        }

    }

}