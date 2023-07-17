using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PM2Examen2Grupo1.Models
{
    public class LocalizacionModel : INotifyPropertyChanged
    {
        private string error;

        public string Error
        {
            get { return error; }
            set
            {
                error = value;
                OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }
        private double longitud;

        public double Longitud
        {
            get { return longitud; }
            set
            {
                longitud = value;
                OnPropertyChanged();
            }
        }
        private double latitud;

        public event PropertyChangedEventHandler PropertyChanged;

        public double Latitud
        {
            get { return latitud; }
            set
            {
                latitud = value;
                OnPropertyChanged();
            }
        }

    }
}

