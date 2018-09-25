/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

#if UNITY_ANDROID

using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace Homewreckers.Backend
{
    /**
     * Implements Android-specific part.
     */
    public sealed partial class BackendManager
    {
        /**
         * Logs in with custom ID.
         */
        partial void LoginPartial()
        {
            Debug.Log("Logging in");

            LoginWithCustomIDRequest request = new LoginWithCustomIDRequest
            {
                CustomId = HomewreckersStudio.PlatformManager.Instance.User.Name,
                CreateAccount = true
            };

            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
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

#endif // UNITY_ANDROID
