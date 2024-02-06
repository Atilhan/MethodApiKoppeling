using MethodApiKoppeling.Models;
using MethodApiKoppeling.ViewModels;
using System.Xml;
using Xamarin.Essentials;
using SecureStorage = Xamarin.Essentials.SecureStorage;

namespace MethodApiKoppeling
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = new MainPageViewModel();
        }

        private async void LoginButton_Clicked(object sender, System.EventArgs e)
        {
            var loginModel = new LoginModel
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text
            };

            var response = await _viewModel.LoginAsync(loginModel);

            if (response.Body.LoginResult > 0)
            {
                // Successful login
                DisplaySavedUserData();

                // Show welcome popup
                await DisplayAlert("Welcome", $"Welcome {loginModel.Username}!", "OK");
            }
            else
            {
                // Incorrect password or username
                responseLabel.Text = "Incorrect password or username. Try again.";

                // You can also show an error popup if needed
                await DisplayAlert("Error", "Incorrect password or username. Try again.", "OK");
            }
        }

        private async void GetRoutesButton_Clicked(object sender, System.EventArgs e)
        {
            int driverId = 120;
            var routes = await _viewModel.GetDrivableRoutesAsync(driverId);
            XmlDocument result= new XmlDocument();
            result.LoadXml(routes.Body.GetDrivableRoutesResult);
            responseLabel.Text = result.SelectSingleNode("/NewDataSet/Table/displayroute").InnerText;
        }

        private void DisplaySavedUserData()
        {
            var savedUserCredentials = _viewModel.GetSavedUserCredentials();

            if (savedUserCredentials != null)
            {
                // Display the saved user data on the screen
                usernameEntry.Text = savedUserCredentials.Username;
                passwordEntry.Text = savedUserCredentials.Password;

                // Display retrieved user credentials in the resultLabel
                resultLabel.Text = $"Retrieved Value: Username={savedUserCredentials.Username}, Password={savedUserCredentials.Password}";
            }
        }
    }
}