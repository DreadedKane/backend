/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

using UnityEngine;

namespace HomewreckersStudio
{
    /**
     * Performs unit tests on the module.
     */
    public sealed class BackendTests : MonoBehaviour
    {
        /**
         * Runs the unit tests.
         */
        private void Start()
        {
            Debug.Log("Running unit tests");

            TestLogin();
        }

        /**
         * Finishes the unit tests.
         */
        private void Finish()
        {
            Debug.Log("Unit tests complete");
        }

        /**
         * Initialises the platform.
         */
        private void TestLogin()
        {
            Debug.Log("Testing login");

            PlatformManager.Instance.Initialise(OnPlatformSuccess, Finish);
        }

        /**
         * Logs in.
         */
        private void OnPlatformSuccess()
        {
            BackendManager.Instance.Login(Finish, Finish);
        }
    }
}

