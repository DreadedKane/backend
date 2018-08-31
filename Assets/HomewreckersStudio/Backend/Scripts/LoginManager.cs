/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

using System;
using UnityEngine;

namespace HomewreckersStudio
{
    public sealed partial class LoginManager : MonoBehaviour
    {
        /** Invoked when login succeeds. */
        private event Action m_success;

        /** Invoked when login fails. */
        private event Action m_failure;

        /**
         * Starts the login process.
         */
        public void Login(string id, Action success, Action failure)
        {
            m_success = success;
            m_failure = failure;

            LoginPartial(id);
        }

        /** Implemented in login module. */
        partial void LoginPartial(string id);

        /**
         * Invokes the sucess event.
         */
        private void OnSuccess()
        {
            Event.Invoke(m_success);

            m_success = null;
            m_failure = null;
        }

        /**
         * Invokes the failure event.
         */
        private void OnFailure()
        {
            Event.Invoke(m_failure);

            m_success = null;
            m_failure = null;
        }
    }
}
