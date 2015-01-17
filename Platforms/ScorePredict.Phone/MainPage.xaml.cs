﻿using ScorePredict.Common.Injection;
using ScorePredict.Core;
using ScorePredict.Core.Impl;
using ScorePredict.Phone.Modules;
using ScorePredict.Services.Modules;
using Xamarin.Forms;

namespace ScorePredict.Phone
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            Forms.Init();

            var app = new ScorePredictApplication(new StandardStartupPageHelper(),
                new ServiceInjectionModule(), new PhoneInjectionModule(new PhonePageHelper()));
            LoadApplication(app);

            // add in the special injection module for windows phone navigation
            Resolver.CurrentResolver.AddModule(new PhoneNavigationModule(app.MainPage.Navigation));
        }
    }
}