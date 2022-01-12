using App_UI.Commands;
using OpenWeatherAPI;
using System;

namespace App_UI.ViewModels
{
    public class ConfigurationViewModel : BaseViewModel
    {
        private string apiKey;

        public string ApiKey
        {
            get => apiKey;
            set
            {
                apiKey = value;
                OnPropertyChanged();
                TestConfigurationCommand?.RaiseCanExecuteChanged();
            }
        }

        private string testResult;

        public string TestResult
        {
            get { return testResult; }
            set { 
                testResult = value;
                OnPropertyChanged();
            }
        }


        public DelegateCommand<string> SaveConfigurationCommand { get; set; }
        public DelegateCommand<string> TestConfigurationCommand { get; set; }


        public ConfigurationViewModel()
        {
            Name = GetType().Name;

            ApiKey = GetApiKey();

            SaveConfigurationCommand = new DelegateCommand<string>(SaveConfiguration);
            TestConfigurationCommand = new DelegateCommand<string>(TestConfiguration, CanTest);
        }

        private async void TestConfiguration(string obj)
        {
            ApiHelper.InitializeClient();
            OpenWeatherProcessor.Instance.ApiKey = AppConfiguration.GetValue("apiKey");
            var result = await OpenWeatherProcessor.Instance.GetOneCallAsync();

            TestResult = result == null ? "Not working" : result.ToString();
        }

        private bool CanTest(string obj)
        {
            return !string.IsNullOrEmpty(ApiKey);
        }

        private void SaveConfiguration(string obj)
        {
            Properties.Settings.Default.apiKey = ApiKey;
            Properties.Settings.Default.Save();
        }

        private string GetApiKey()
        {
            return Properties.Settings.Default.apiKey;
        }

    }
}
