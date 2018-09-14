/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

#if UNITY_STANDALONE_WIN

using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace HomewreckersStudio
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
                SteamTicket = PlatformManager.Instance.Entitlements.SessionTicket,
                CreateAccount = true
            };

            PlayFabClientAPI.LoginWithSteam(request, OnLoginSuccess, OnLoginFailure);
        }

        /**
         * Calls the success method.
         */
        private void OnLoginSuccess(LoginResult result)
        {
            Debug.Log("Login succeeded");

            m_request.OnSuccess();
        }

        /**
         * Calls the failure method.
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
