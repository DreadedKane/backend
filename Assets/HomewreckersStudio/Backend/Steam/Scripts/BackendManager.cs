/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

#if UNITY_STANDALONE_WIN

using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace Homewreckers.Backend
{
    /**
     * Implements Windows-specific part.
     */
    public sealed partial class BackendManager
    {
        /**
         * Logs in with Steam.
         */
        partial void LoginPartial()
        {
            Debug.Log("Logging in");

            LoginWithSteamRequest request = new LoginWithSteamRequest
            {
                SteamTicket = HomewreckersStudio.PlatformManager.Instance.Entitlements.SessionTicket,
                CreateAccount = true
            };

            PlayFabClientAPI.LoginWithSteam(request, OnLoginSuccess, OnLoginFailure);
        }

        /**
         * Calls success.
         */
        private void OnLoginSuccess(LoginResult result)
        {
            Debug.Log("Login succeeded");

            m_request.OnSuccess();
        }

        /**
         * Logs an error and calls failure.
         */
        private void OnLoginFailure(PlayFabError error)
        {
            string message = error.GenerateErrorReport();

            Debug.LogError("Login failed: " + message);

            m_request.OnFailure();
        }
    }
}

#endif // UNITY_STANDALONE_WIN
