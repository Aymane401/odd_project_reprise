using App_UI.Commands;
using App_UI.Models;
using App_UI.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace App_UI.ViewModels
{
    public class ElementsViewModel : BaseViewModel
    {
        #region Membres
        private IDataService<Element> dataService;
        IDialogService confirmDeleteDialog;
        Element originalData;
        #endregion

        #region Expression Régulière

        public Regex ReSymbol { get; set; }

        #endregion

        #region Propriétés
        private ObservableCollection<Element> data;

        public ObservableCollection<Element> Data
        {
            get { return data; }
            set
            {
                data = value;
                OnPropertyChanged();
            }
        }

        private Element selectedElement;

        public Element SelectedElement
        {
            get { return selectedElement; }
            set {
                /// On fait une copie de l'objet sélectionné
                /// pour permettre de retourner à la valeur originale
                /// si on veut annuler
                originalData = value;
                selectedElement = (Element)value?.Clone();

                IsSaveable = false;
                IsDirty = false;

                OnPropertyChanged();
                updateProperties();

                DeleteCommand?.RaiseCanExecuteChanged();
            }
        }

        public string ElementName
        {
            get { 
                return SelectedElement?.Name; 
            }
            set { 
                SelectedElement.Name = value;
                SelectedPropertyChanged();
            }
        }


        public int? AtomicNumber
        {
            get { return SelectedElement?.AtomicNumber; }
            set {
                SelectedElement.AtomicNumber = (int)value;
                SelectedPropertyChanged();
            }
        }

        public string Symbol
        {
            get => SelectedElement?.Symbol;
            set
            {
                SelectedElement.Symbol = value;
                SelectedPropertyChanged();
            }
        }

        public double? AtomicWeight
        {
            get => SelectedElement?.AtomicWeight;
            set {
                SelectedElement.AtomicWeight = (double)value;
                SelectedPropertyChanged();
            }
        }

        public string Phase
        {
            get => SelectedElement?.Phase;
            set {
                SelectedElement.Phase = value;
                SelectedPropertyChanged();
            }
        }

        public string Type
        {
            get => SelectedElement?.Type;
            set {
                SelectedElement.Type = value;
                SelectedPropertyChanged();
            }
        }

        public double? MeltingPoint
        {
            get => SelectedElement?.MeltingPoint;
            set {
                SelectedElement.MeltingPoint = (double)value;
                SelectedPropertyChanged();
            }
        }

        public double? BoilingPoint
        {
            get => SelectedElement?.BoilingPoint; set
            {
                SelectedElement.BoilingPoint = (double)value;
                SelectedPropertyChanged();
            }
        }

        public double? Density
        {
            get => SelectedElement?.Density;
            set
            {
                SelectedElement.Density = (double)value;
                SelectedPropertyChanged();
            }
        }

        public string Discoverer
        {
            get => SelectedElement?.Discoverer;
            set
            {
                SelectedElement.Discoverer = value;
                SelectedPropertyChanged();
            }
        }

        public int? Discovery
        {
            get => SelectedElement?.Discovery;
            set
            {
                SelectedElement.Discovery = (int)value;
                SelectedPropertyChanged();
            }
        }

        private bool isDirty = false;
        public bool IsDirty
        {
            get => isDirty;
            private set
            {
                isDirty = value;
                OnPropertyChanged();
                ValidateDataCommand?.RaiseCanExecuteChanged();
            }
        }

        private bool isSaveable = false;
        public bool IsSaveable
        {
            get => isSaveable;
            private set
            {
                isSaveable = value;
                SaveCommand?.RaiseCanExecuteChanged();
                CancelCommand?.RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        private string message;

        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(MessageVisibility));
            }
        }

        public bool MessageVisibility
        {
            get => !string.IsNullOrEmpty(Message);
        }

        #endregion

        #region Commandes
        public DelegateCommand<string> ValidateDataCommand { get; set; }
        public DelegateCommand<string> SaveCommand { get; set; }
        public DelegateCommand<string> DeleteCommand { get; set; }
        public DelegateCommand<string> CancelCommand { get; set; }
        #endregion

        public ElementsViewModel(IDataService<Element> _dataService)
        {
            Name = nameof(ElementsViewModel);
            dataService = _dataService;
            data = new ObservableCollection<Element>(dataService.GetAll());
            SelectedElement = data[0];

            ValidateDataCommand = new DelegateCommand<string>(ValidateData, CanValidate);
            SaveCommand = new DelegateCommand<string>(SaveData, CanSave);
            CancelCommand = new DelegateCommand<string>(CancelChange, CanCancel);
            DeleteCommand = new DelegateCommand<string>(DeleteData, CanDelete);

            initRegex();
        }

        void initRegex()
        {
            /// TODO 04 : Validation de l'information
            ReSymbol = new Regex(@"");
        }

        private void ValidateData(string param)
        {
            Message = "";

            if (string.IsNullOrEmpty(Symbol) || string.IsNullOrEmpty(Name) || AtomicNumber == 0)
            {
                Message = "Plusieurs champs sont vides ou invalides";
                return;
            }

            if (!ReSymbol.IsMatch(Symbol))
            {
                Message += "Le symbol chimique de l'élément est invalide." + Environment.NewLine;
            }

            if (Discovery < 0 )
            {
                Message += "L'année de découverte est invalide." + Environment.NewLine;
            }

            IsDirty = Message != "";

            IsSaveable = Message == "";
        }


        public void UpdateData(IDataService<Element> dataService)
        {
            Data = new ObservableCollection<Element>(dataService.GetAll());
            SelectedElement = data[0];
        }

        public void CreateEmptyRecord()
        {
            var r = new Element();

            SelectedElement = r;
        }

        public void SetDeleteDialog(IDialogService _confirmDeleteDialog)
        {
            confirmDeleteDialog = _confirmDeleteDialog;
        }

        private void DeleteData(string obj)
        {
            if (SelectedElement == null) return;

            if (confirmDeleteDialog.ShowDialog() == true)
            {
                Data.Remove(originalData);
                SelectedElement = data[0];
            }
        }

        private bool CanDelete(string obj)
        {
            return SelectedElement != null;
        }

        private bool CanCancel(string obj)
        {
            return IsSaveable;
        }

        private void CancelChange(string obj)
        {
            SelectedElement = originalData;

            IsSaveable = false;
        }

        private bool CanSave(string obj)
        {
            return IsSaveable && SelectedElement != null;
        }


        /// <summary>
        /// La méthode doit mettre à jour l'information de l'objet sélectionné
        /// Attention! Il ne s'agit de sauvegarder dans un fichier
        /// </summary>
        /// <param name="obj"></param>
        private void SaveData(string obj)
        {
            ElementDataService.Instance.Update(SelectedElement);

            if (Data.FirstOrDefault(p => p.Id == SelectedElement.Id) == null)
            {
                Data.Add(SelectedElement);
            }

            OnPropertyChanged(nameof(Data));
            OnPropertyChanged(nameof(SelectedElement));
            IsSaveable = false;
        }

        private bool CanValidate(string obj)
        {
            return IsDirty;
        }

        private void updateProperties()
        {
            OnPropertyChanged(nameof(ElementName));
            OnPropertyChanged(nameof(AtomicNumber));
            OnPropertyChanged(nameof(Symbol));
            OnPropertyChanged(nameof(AtomicWeight));
            OnPropertyChanged(nameof(Phase));
            OnPropertyChanged(nameof(Type));
            OnPropertyChanged(nameof(MeltingPoint));
            OnPropertyChanged(nameof(BoilingPoint));
            OnPropertyChanged(nameof(Density));
            OnPropertyChanged(nameof(Discoverer));
            OnPropertyChanged(nameof(Discovery));
        }

        protected virtual void SelectedPropertyChanged([CallerMemberName] string propertyName = null)
        {
            IsDirty = true;
            OnPropertyChanged(propertyName);
        }
    }
}
