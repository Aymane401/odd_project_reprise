using System.Windows;

namespace App_UI.Services
{
    public class MessageBoxDialogService : IDialogService
    {
        public string Message { get; set; }
        public string Caption { get; set; }
        public MessageBoxButton Buttons { get; set; }

        public bool? ShowDialog()
        {
            return MessageBox.Show(Message, Caption, Buttons) == MessageBoxResult.Yes;
        }
    }
}
