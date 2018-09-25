/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

using Homewreckers.Backend;
using HomewreckersStudio;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Homewreckers
{
    /**
     * Performs unit tests on the UserData class.
     */
    public sealed class UserDataTests
    {
        /** Used to invoke callbacks. */
        private Request m_request;

        /**
         * Creates the required objects.
         */
        public UserDataTests()
        {
            m_request = new Request();
        }

        /**
         * Starts the user data tests.
         */
        public void Test(Action success, Action failure)
        {
            m_request.SetListeners(success, failure);

            TestData();
        }

        /**
         * Initialises user data.
         */
        private void TestData()
        {
            Debug.Log("Testing user data");

            UserData userData = BackendManager.Instance.GetUserData();

            userData.Initialise(OnDataSuccess, OnFailure);
        }

        /**
         * Prints user data, statistics, and inventory. Runs the consume test if the inventory is not empty.
         */
        private void OnDataSuccess()
        {
            PrintData();
            PrintStatistics();
            PrintInventory();

            UserData userData = BackendManager.Instance.GetUserData();

            List<ItemInstance> inventory = userData.GetInventory();

            if (inventory.IsNullOrEmpty())
            {
                m_request.OnSuccess();
            }
            else
            {
                TestConsume();
            }
        }

        /**
         * Logs the user's data.
         */
        private void PrintData()
        {
            UserData userData = BackendManager.Instance.GetUserData();

            Dictionary<string, UserDataRecord> playFabData = userData.GetData();

            foreach (string key in playFabData.Keys)
            {
                UserDataRecord record = playFabData[key];

                Debug.Log(key + ": " + record.Value);
            }
        }

        /**
         * Logs the user's statistics.
         */
        private void PrintStatistics()
        {
            UserData userData = BackendManager.Instance.GetUserData();

            foreach (StatisticValue statistic in userData.GetStatistics())
            {
                Debug.Log(statistic.StatisticName + ": " + statistic.Value);
            }
        }

        /**
         * Logs the user's inventory.
         */
        private void PrintInventory()
        {
            UserData userData = BackendManager.Instance.GetUserData();

            foreach (ItemInstance item in userData.GetInventory())
            {
                Debug.Log(item.DisplayName);
            }
        }

        /**
         * Consumes the first item in the user's inventory.
         */
        private void TestConsume()
        {
            Debug.Log("Testing consume");

            UserData userData = BackendManager.Instance.GetUserData();
            List<ItemInstance> inventory = userData.GetInventory();

            userData.Consume(inventory.GetFirst(), m_request.OnSuccess, OnFailure);
        }

        /**
         * Logs an error and calls failure.
         */
        private void OnFailure()
        {
            Debug.LogError("Data test failed");

            m_request.OnFailure();
        }
    }
}
