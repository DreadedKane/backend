/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

using HomewreckersStudio;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Homewreckers.Backend
{
    /**
     * Manages logging in, encapsulates user data and leaderboards.
     */
    public sealed partial class BackendManager : Singleton<BackendManager>
    {
        [Header("Properties")]

        [SerializeField]
        [Tooltip("Maximum number of entries to return with leaderboard queries.")]
        private int m_maxLeaderboardEntries = 5;

        /** Used to invoke callbacks. */
        private Request m_request;

        /** Used to get user-specific data. */
        private UserData m_userData;

        /** Used to get leaderboard entries. */
        private Dictionary<string, Leaderboard> m_leaderboards;

        /**
         * Gets the user data.
         */
        public UserData GetUserData()
        {
            return m_userData;
        }

        /**
         * Gets a leaderboard by name.
         */
        public Leaderboard GetLeaderboard(string name)
        {
            if (!m_leaderboards.ContainsKey(name))
            {
                Leaderboard leaderboard = new Leaderboard(name, m_maxLeaderboardEntries);

                m_leaderboards.Add(name, leaderboard);
            }

            return m_leaderboards[name];
        }

        /**
         * Creates the required objects.
         */
        protected override void Awake()
        {
            base.Awake();

            m_request = new Request();
            m_userData = new UserData();
            m_leaderboards = new Dictionary<string, Leaderboard>();
        }

        /**
         * Starts the login process.
         */
        public void Login(Action success, Action failure)
        {
            m_request.SetListeners(success, failure);

            LoginPartial();
        }

        /** Implemented in backend module. */
        partial void LoginPartial();
    }
}
