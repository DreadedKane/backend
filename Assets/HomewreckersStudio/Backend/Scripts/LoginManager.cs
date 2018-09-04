/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

using System;

namespace HomewreckersStudio
{
    public sealed partial class LoginManager : Request
    {
        /**
         * Starts the login process.
         */
        public void Login(Action success, Action failure)
        {
            SetEvents(success, failure);

            LoginPartial();
        }

        /** Implemented in login module. */
        partial void LoginPartial();
    }
}
