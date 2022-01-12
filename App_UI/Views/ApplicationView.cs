using App_UI.Services;
using App_UI.ViewModels;
using System.Windows;

namespace App_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ApplicationView : Window
    {
        ApplicationViewModel vm;

        public ApplicationView()
        {
            InitializeComponent();

            FileDialogService openFileDialog = new FileDialogService(true);
            FileDialogService saveFileDialog = new FileDialogService(false);
            MessageBoxDialogService confirmDeleteDialog = new MessageBoxDialogService();

            confirmDeleteDialog.Caption = "Avertissement!";
            confirmDeleteDialog.Message = "Êtes-vous certain de vouloir supprimer l'enregistrement?";
            confirmDeleteDialog.Buttons = MessageBoxButton.YesNo;            

            vm = new ApplicationViewModel(openFileDialog, saveFileDialog, confirmDeleteDialog);

            DataContext = vm;
        }

    }
}
