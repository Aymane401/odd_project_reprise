using Ookii.Dialogs.Wpf;

namespace App_UI.Services
{
    public class FileDialogService : IFileDialogService
    {
        private readonly VistaFileDialog fileDialog;
        
        public string Filter { 
            get => fileDialog.Filter; 
            set => fileDialog.Filter = value;
        }

        public string InitialDirectory {
            get => fileDialog.InitialDirectory;
            set => fileDialog.InitialDirectory = value;
        }
        public string Filename { 
            get => fileDialog.FileName;
            set => fileDialog.FileName = value;
        }

        public FileDialogService(bool isOpenFileDialog = true)
        {
            if (isOpenFileDialog)
                fileDialog = new VistaOpenFileDialog();
            else
                fileDialog = new VistaSaveFileDialog();
        }
                

        public bool? ShowDialog()
        {
            return fileDialog.ShowDialog();
        }
    }
}
