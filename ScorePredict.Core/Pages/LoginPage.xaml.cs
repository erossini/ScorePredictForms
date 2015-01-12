﻿using System;
using ScorePredict.Common.Injection;
using ScorePredict.Data;
using ScorePredict.Services;
using ScorePredict.Services.Client;

namespace ScorePredict.Core.Pages
{
    public partial class LoginPage
    {
        private readonly IPageHelper _pageHelper;
        private readonly ILoginUserService _loginUserService;
        private readonly ISaveUserSecurityService _saveUserSecurityService;

        public LoginPage()
        {
            InitializeComponent();

            _loginUserService = Resolver.CurrentResolver.Get<ILoginUserService>();
            _saveUserSecurityService = Resolver.CurrentResolver.Get<ISaveUserSecurityService>();
            _pageHelper = Resolver.CurrentResolver.GetInstance<IPageHelper>();
        }

        private void GoToCreateUser(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateUserPage());
        }

        private async void Login(object sender, EventArgs ev)
        {
            var errorMessage = string.Empty;
            try
            {
                var user = await _loginUserService.LoginAsync(txtUsername.Text, txtPassword.Text);
                if (user == null)
                    throw new LoginException("Invalid Username Password combination");

                if (string.IsNullOrEmpty(user.Username))
                {
                    await Navigation.PushAsync(new EnterUsernamePage(user));
                    return; // end execution
                }

                _saveUserSecurityService.SaveUser(user);
                Resolver.CurrentResolver.GetInstance<IClient>().AuthenticateUser(user);

                _pageHelper.ShowMain();
            }
            catch (LoginException lex)
            {
                errorMessage = lex.Message;
            }
            catch (Exception ex)
            {
                errorMessage = "Login did not succeed. Please try again";
            }

            if (!string.IsNullOrEmpty(errorMessage))
                await DisplayAlert("Error", errorMessage, "Ok");
        }

        private async void FacebookLogin(object sender, EventArgs ev)
        {
            string errorMessage = string.Empty;

            try
            {
                var result = await _loginUserService.LoginWithFacebookAsync();
                if (result == null)
                {
                    throw new LoginException("Failed to Login in with Facebook");
                }

                //_saveUserSecurityService.SaveUser(result);
                Resolver.CurrentResolver.GetInstance<IClient>().AuthenticateUser(result);

                /*await Navigation.PopModalAsync(true);
                if (string.IsNullOrEmpty(result.Username))
                {
                    // go to username page
                    await Navigation.PushModalAsync(new EnterUsernamePage(), true);
                }*/
            }
            catch (LoginException lex)
            {
                errorMessage = lex.Message;
            }
            catch (Exception ex)
            {
                errorMessage = "Login Failed. Please try again.";
            }

            if (!string.IsNullOrEmpty(errorMessage))
                await DisplayAlert("Error", errorMessage, "Ok");
        }
    }
}
