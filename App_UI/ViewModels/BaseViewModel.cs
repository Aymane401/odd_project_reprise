using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace App_UI.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
