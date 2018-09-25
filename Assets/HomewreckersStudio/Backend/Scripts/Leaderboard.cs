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
     * Initialises leaderboard entries.
     */
    public sealed partial class Leaderboard
    {
        /** The name of the leaderboard to retrieve data for. */
        private readonly string m_name;

        /** The maximum number of entries to return. */
        private readonly int m_maxEntries;

        /** Used to invoke callbacks. */
        private Request m_request;

        /** The current position in the leaderboard. */
        private int m_position;

        /** The current list of entries. */
        private List<LeaderboardEntry> m_entries;

        /**
         * Initialises the leaderboard and creates required objects.
         */
        public Leaderboard(string name, int maxEntries)
        {
            m_name = name;
            m_maxEntries = maxEntries;

            m_request = new Request();
        }

        /**
         * Gets the current entry list.
         */
        public List<LeaderboardEntry> GetEntries()
        {
            return m_entries;
        }

        /**
         * Initialises entries from the current position.
         */
        public void Initialise(Action success, Action failure)
        {
            Debug.Log("Initialising entries");

            m_request.SetListeners(success, failure);

            InitialisePartial();
        }

        /** Implemented in backend module. */
        partial void InitialisePartial();

        /**
         * Increments the start position and initialises entries.
         */
        public void InitialiseNext(Action success, Action failure)
        {
            m_position += m_maxEntries;

            Initialise(success, failure);
        }

        /**
         * Decrements the start position and initialises entries.
         */
        public void InitialisePrevious(Action success, Action failure)
        {
            m_position = Mathf.Max(m_position - m_maxEntries, 0);

            Initialise(success, failure);
        }

        /**
         * Initialises entries around the user.
         */
        public void InitialiseUser(Action success, Action failure)
        {
            Debug.Log("Initialising user");

            m_request.SetListeners(success, failure);

            InitialiseUserPartial();
        }

        /** Implemented in backend module. */
        partial void InitialiseUserPartial();
    }
}
