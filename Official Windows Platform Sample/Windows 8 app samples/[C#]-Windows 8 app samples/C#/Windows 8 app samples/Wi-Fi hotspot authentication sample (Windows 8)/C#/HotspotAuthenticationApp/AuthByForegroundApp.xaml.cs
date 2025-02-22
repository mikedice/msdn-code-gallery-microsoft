﻿//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using HotspotAuthenticationTask;
using Windows.Networking.NetworkOperators;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using SDKTemplate;
using System;

namespace HotspotAuthenticationApp
{
    public sealed partial class AuthByForegroundApp : SDKTemplate.Common.LayoutAwarePage
    {
        // A pointer back to the main page.  This is needed to call methods in MainPage such as NotifyUser()
        MainPage rootPage = MainPage.Current;

        HotspotAuthenticationContext authenticationContext;

        public AuthByForegroundApp()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Configure background task handler to trigger foregound app for authentication
            ConfigStore.AuthenticateThroughBackgroundTask = false;

            // Setup completion handler
            ScenarioCommon.Instance.RegisteredCompletionHandlerForBackgroundTask();

            // Register event to update UI state on authentication event
            ScenarioCommon.Instance.ForegroundAuthenticationCallback = HandleForegroundAuthenticationCallback;

            // Check current authenticatino state
            InitializeForegroundAppAuthentication();
        }

        /// <summary>
        /// Handle auhentication event triggered by background task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void HandleForegroundAuthenticationCallback(object sender, EventArgs e)
        {
            InitializeForegroundAppAuthentication();
        }

        /// <summary>
        /// This is the click handler for the 'Authenticate' button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AuthenticateButton_Click(object sender, RoutedEventArgs args)
        {
            try
            {
                authenticationContext.IssueCredentials(ConfigStore.UserName, ConfigStore.Password, ConfigStore.ExtraParameters, ConfigStore.MarkAsManualConnect);
                rootPage.NotifyUser("Issuing credentials succeeded", NotifyType.StatusMessage);
            }
            catch (Exception ex)
            {
                rootPage.NotifyUser(ex.ToString(), NotifyType.StatusMessage);
            }
            ClearAuthenticationToken();
        }

        /// <summary>
        /// This is the click handler for the 'Skip' button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkipButton_Click(object sender, RoutedEventArgs args)
        {
            try
            {
                authenticationContext.SkipAuthentication();
                rootPage.NotifyUser("Authentication skipped", NotifyType.StatusMessage);
            }
            catch (Exception ex)
            {
                rootPage.NotifyUser(ex.ToString(), NotifyType.ErrorMessage);
            }
            ClearAuthenticationToken();
        }

        /// <summary>
        /// This is the click handler for the 'Abort' button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AbortButton_Click(object sender, RoutedEventArgs args)
        {
            try
            {
                authenticationContext.AbortAuthentication(ConfigStore.MarkAsManualConnect);
                rootPage.NotifyUser("Authentication aborted", NotifyType.StatusMessage);
            }
            catch (Exception ex)
            {
                rootPage.NotifyUser(ex.ToString(), NotifyType.ErrorMessage);
            }
            ClearAuthenticationToken();
        }

        /// <summary>
        /// Query authentication token from application storage and upate the UI.
        /// The token gets passed from the background task handler.
        /// </summary>
        private void InitializeForegroundAppAuthentication()
        {
            string token = ConfigStore.AuthenticationToken;
            if (token == "")
            {
                return; // no token found
            }
            if (!HotspotAuthenticationContext.TryGetAuthenticationContext(token, out authenticationContext))
            {
                rootPage.NotifyUser("TryGetAuthenticationContext failed", NotifyType.ErrorMessage);
                return;
            }

            AuthenticateButton.IsEnabled = true;
            SkipButton.IsEnabled = true;
            AbortButton.IsEnabled = true;
        }

        /// <summary>
        /// Clear the authentication token in the application storage and update the UI.
        /// </summary>
        private void ClearAuthenticationToken()
        {
            ConfigStore.AuthenticationToken = "";
            AuthenticateButton.IsEnabled = false;
            SkipButton.IsEnabled = false;
            AbortButton.IsEnabled = false;
        }
    }
}
