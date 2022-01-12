using App_UI.Commands;
using App_UI.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace App_UI.ViewModels
{
    public class ApplicationViewModel : BaseViewModel
    {
        #region Membres
        // Mettre les membres ici

        private BaseViewModel currentViewModel;
        private List<BaseViewModel> viewModels;
        private ElementsViewModel elementsViewModel;

        private string filename;

        private IFileDialogService openFileDialog;
        private IFileDialogService saveFileDialog;
        private IDialogService confirmDeleteDialog;

        #endregion

        #region Propriétés
        // Mettre les propriétés ici
        /// <summary>
        /// Model actuellement affiché
        /// </summary>
        public BaseViewModel CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                currentViewModel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// String contenant le nom du fichier
        /// </summary>
        public string Filename
        {
            get
            {
                return filename;
            }
            set
            {
                filename = value;
            }
        }

        public List<BaseViewModel> ViewModels
        {
            get
            {
                if (viewModels == null)
                    viewModels = new List<BaseViewModel>();
                return viewModels;
            }
        }

        #endregion

        #region Commandes

        public DelegateCommand<string> SaveFileCommand { get; set; }
        public DelegateCommand<string> OpenFileCommand { get; set; }
        public DelegateCommand<string> NewRecordCommand { get; set; }

        /// <summary>
        /// Commande pour changer la page à afficher
        /// </summary>
        public DelegateCommand<string> ChangePageCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DelegateCommand<string> ImportCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DelegateCommand<string> ExportCommand { get; set; }

        /// <summary>
        /// TODO 01 : S'assurer que l'on puisse changer la langue de l'application
        /// </summary>
        public DelegateCommand<string> ChangeLanguageCommand { get; set; }


        #endregion


        public ApplicationViewModel(IFileDialogService _openFileDialog, 
                                    IFileDialogService _saveFileDialog, 
                                    MessageBoxDialogService _confirmDeleteDialog)
        {
            openFileDialog = _openFileDialog;
            saveFileDialog = _saveFileDialog;
            confirmDeleteDialog = _confirmDeleteDialog;

            initCommands();            
            initViewModels();
        }

        #region Méthodes

        private void initCommands()
        {
            ChangePageCommand = new DelegateCommand<string>(ChangePage);
            NewRecordCommand = new DelegateCommand<string>(RecordCreate);

            /// TODO 02 : Compléter la commande ExportDataCommand pour qu'elle appelle "ExportData"

            /// TODO 03 : Compléter la commande ExportDataCommand pour qu'elle appelle "ImportData"

            /// TODO 01b : Les commandes sont instanciées ici            
        }

        private void RecordCreate(string obj)
        {
            elementsViewModel?.CreateEmptyRecord();
        }

        private void ExportData(string obj)
        {
            if (saveFileDialog.ShowDialog() == true)
            {
                using (TextWriter tw = new StreamWriter(saveFileDialog.Filename, false))
                {
                    string output = ElementDataService.Instance.GetAllAsJson();

                    tw.Write(output);
                    tw.Close();
                }
            }
        }

        private async void ImportData(string obj)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                using (StreamReader sr = File.OpenText(openFileDialog.Filename))
                {
                    var fileContent = await sr.ReadToEndAsync();

                    ElementDataService.Instance.SetAllFromJson(fileContent);

                    elementsViewModel.UpdateData(ElementDataService.Instance);

                    sr.Close();
                }
            }
        }

        private void initViewModels()
        {
            elementsViewModel = new ElementsViewModel(ElementDataService.Instance);

            elementsViewModel.SetDeleteDialog(confirmDeleteDialog);

            CurrentViewModel = elementsViewModel;

            var configurationViewModel = new ConfigurationViewModel();

            ViewModels.Add(configurationViewModel);
            ViewModels.Add(elementsViewModel);
        }

        private void ChangePage(string name)
        {
            /// users, la page est vide.
            var vm = ViewModels.Find(vm => vm.Name == name);
            CurrentViewModel = vm;

        }

        #endregion
    }
}