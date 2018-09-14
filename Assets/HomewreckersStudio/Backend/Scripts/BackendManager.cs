/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

using System;

namespace HomewreckersStudio
{
    /**
     * Manages logging in.
     */
    public sealed partial class BackendManager : Singleton<BackendManager>
    {
        /** Used to invoke callbacks. */
        private Request m_request;

        /**
         * Creates the request object.
         */
        protected override void Awake()
        {
            base.Awake();

            m_request = new Request();
        }

        /**
         * Starts the login process.
         */
        public void Login(Action success, Action failure)
        {
            m_request.SetListeners(success, failure);

            LoginPartial();
        }

        /** Implemented in login module. */
        partial void LoginPartial();
    }
}
