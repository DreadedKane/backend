/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

using Homewreckers.Backend;
using HomewreckersStudio;
using System;
using UnityEngine;

namespace Homewreckers
{
    /**
     * Performs unit tests on the Leaderboard class.
     */
    public sealed class LeaderboardTests
    {
        /** Used to invoke callbacks. */
        private Request m_request;

        /** The name of the leaderboard being tested. */
        private string m_name;

        /**
         * Creates the required objects.
         */
        public LeaderboardTests()
        {
            m_request = new Request();
        }

        /**
         * Starts the leaderboard tests.
         */
        public void Test(string name, Action success, Action failure)
        {
            m_request.SetListeners(success, failure);

            m_name = name;

            TestCurrent();
        }

        /**
         * Initialises the leaderboard.
         */
        private void TestCurrent()
        {
            Debug.Log("[" + m_name + "] Testing current");

            Leaderboard leaderboard = BackendManager.Instance.GetLeaderboard(m_name);

            leaderboard.Initialise(OnSuccess, OnFailure);
        }

        /**
         * Prints the leaderboard entries and initialises the next lot of entries.
         */
        private void OnSuccess()
        {
            PrintEntries();

            Debug.Log("[" + m_name + "] Testing next");

            Leaderboard leaderboard = BackendManager.Instance.GetLeaderboard(m_name);

            leaderboard.InitialiseNext(OnNextSuccess, OnFailure);
        }

        /**
         * Prints the leaderboard entries and initialises the previous lot of entries.
         */
        private void OnNextSuccess()
        {
            PrintEntries();

            Debug.Log("[" + m_name + "] Testing previous");

            Leaderboard leaderboard = BackendManager.Instance.GetLeaderboard(m_name);

            leaderboard.InitialisePrevious(OnPreviousSuccess, OnFailure);
        }

        /**
         * Prints the leaderboard entries and intialises the user entries.
         */
        private void OnPreviousSuccess()
        {
            PrintEntries();

            Debug.Log("[" + m_name + "] Testing user");

            Leaderboard leaderboard = BackendManager.Instance.GetLeaderboard(m_name);

            leaderboard.InitialiseUser(OnUserSuccess, OnFailure);
        }

        /**
         * Prints the leaderboard entries and calls success.
         */
        private void OnUserSuccess()
        {
            PrintEntries();

            m_request.OnSuccess();
        }

        /**
         * Logs the leaderboard entries.
         */
        private void PrintEntries()
        {
            Leaderboard leaderboard = BackendManager.Instance.GetLeaderboard(m_name);

            foreach (LeaderboardEntry entry in leaderboard.GetEntries())
            {
                string message = string.Format("{0} {1}: {2}", entry.GetRank(), entry.GetName(), entry.GetScore());

                Debug.Log(message);
            }
        }

        /**
         * Logs an error and calls failure.
         */
        private void OnFailure()
        {
            Debug.LogError("[" + m_name + "] leaderboard test failed");

            m_request.OnFailure();
        }
    }
}
