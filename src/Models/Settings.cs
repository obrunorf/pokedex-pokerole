using System.ComponentModel;

namespace Pokedex.Pokerole.Models
{
    public class Settings : INotifyPropertyChanged
    {
        public SelectableJsonFile selectedFile { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}