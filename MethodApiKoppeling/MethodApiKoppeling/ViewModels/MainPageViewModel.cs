using KPRSServiceIdeaX;
using MethodApiKoppeling.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using SecureStorage = Xamarin.Essentials.SecureStorage;
namespace MethodApiKoppeling.ViewModels
{
    public class MainPageViewModel
    {
        private readonly AppWebserviceSoapClient _apiClient;
        

        public MainPageViewModel()
        {
            _apiClient = new KPRSServiceIdeaX.AppWebserviceSoapClient(KPRSServiceIdeaX.AppWebserviceSoapClient.EndpointConfiguration.AppWebserviceSoap);
    }

        public async Task<LoginResponse> LoginAsync(LoginModel _loginModel)
        {
            // Save user credentials securely
            Microsoft.Maui.Storage.Preferences.Set("Username", _loginModel.Username);
            Microsoft.Maui.Storage.Preferences.Set("Password", _loginModel.Password);

            var a = await _apiClient.LoginAsync(_loginModel.Username, _loginModel.Password);
            return a;
        }

        public async Task<GetDrivableRoutesResponse> GetDrivableRoutesAsync(int driverId)
        {
            try
            {
                var response = await _apiClient.GetDrivableRoutesAsync(driverId);
                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public UserCredentialsModel GetSavedUserCredentials()
        {
            try
            {
                var username = Microsoft.Maui.Storage.Preferences.Get("Username", string.Empty);
                var password = Microsoft.Maui.Storage.Preferences.Get("Password", string.Empty);

                return new UserCredentialsModel
                {
                    Username = username,
                    Password = password
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
