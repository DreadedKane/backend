/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

using Homewreckers.Backend;
using HomewreckersStudio;
using UnityEngine;

namespace Homewreckers
{
    /**
     * Performs unit tests on the module.
     */
    public sealed class BackendTests : MonoBehaviour
    {
        [Header("Properties")]

        [SerializeField]
        [Tooltip("Names of leaderboards to test.")]
        private string[] m_leaderboardNames;

        /** Used to test the UserData class. */
        private UserDataTests m_userDataTests;

        /** Used to test the leaderboard class. */
        private LeaderboardTests m_leaderboardTests;

        /** The current index in the leaderboard name array. */
        private int m_leaderboardIndex;

        /**
         * Creates the required objects.
         */
        private void Awake()
        {
            m_userDataTests = new UserDataTests();
            m_leaderboardTests = new LeaderboardTests();
        }

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

            HomewreckersStudio.PlatformManager.Instance.Initialise(OnPlatformSuccess, Finish);
        }

        /**
         * Logs in.
         */
        private void OnPlatformSuccess()
        {
            BackendManager.Instance.Login(TestData, OnLoginFailure);
        }

        /**
         * Logs an error and finishes.
         */
        private void OnLoginFailure()
        {
            Debug.LogError("Login failed");

            Finish();
        }

        /**
         * Runs user data tests.
         */
        private void TestData()
        {
            m_userDataTests.Test(OnDataComplete, OnDataComplete);
        }

        /**
         * Tests leaderboards if leaderboard names are available or finishes.
         */
        private void OnDataComplete()
        {
            if (m_leaderboardNames.IsNullOrEmpty())
            {
                Finish();
            }
            else
            {
                TestLeaderboards();
            }
        }

        /**
         * Runs leaderboard tests for each leaderboard name available.
         */
        private void TestLeaderboards()
        {
            if (m_leaderboardIndex < m_leaderboardNames.Length)
            {
                string name = m_leaderboardNames[m_leaderboardIndex];

                m_leaderboardIndex++;

                m_leaderboardTests.Test(name, TestLeaderboards, TestLeaderboards);
            }
            else
            {
                Finish();
            }
        }
    }
}

