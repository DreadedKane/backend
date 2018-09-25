/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

using HomewreckersStudio;
using System;

namespace Homewreckers.Backend
{
    /**
     * Initialises user data.
     */
    public sealed partial class UserData
    {
        /** Used to invoke callbacks. */
        private Request m_request;

        /**
         * Creates the required objects.
         */
        public UserData()
        {
            m_request = new Request();
        }

        /**
         * Initialises user data.
         */
        public void Initialise(Action success, Action failure)
        {
            m_request.SetListeners(success, failure);

            InitialisePartial();
        }

        /** Implemented in backend module. */
        partial void InitialisePartial();
    }
}
