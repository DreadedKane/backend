﻿/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

using UnityEngine;

namespace HomewreckersStudio
{
    public sealed class BackendTests : MonoBehaviour
    {
        /** Used to initialize the platform. */
        [SerializeField]
        private PlatformManager m_platformManager;

        /** Used to login. */
        [SerializeField]
        private BackendManager m_backendManager;

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

            m_platformManager.Initialize(OnPlatformSuccess, Finish);
        }

        /**
         * Logs in.
         */
        private void OnPlatformSuccess()
        {
            m_backendManager.Login(Finish, Finish);
        }
    }
}
