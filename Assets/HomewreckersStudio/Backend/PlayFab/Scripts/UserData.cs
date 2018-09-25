/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Homewreckers.Backend
{
    /**
     * Implements the PlayFab-specific part.
     */
    public partial class UserData
    {
        /** PlayFab user data. */
        private Dictionary<string, UserDataRecord> m_data;

        /** PlayFab user statistics. */
        private List<StatisticValue> m_statistics;

        /** PlayFab user inventory. */
        private List<ItemInstance> m_inventory;

        /**
         * Gets the user's data.
         */
        public Dictionary<string, UserDataRecord> GetData()
        {
            return m_data;
        }

        /**
         * Gets the user's statistics.
         */
        public List<StatisticValue> GetStatistics()
        {
            return m_statistics;
        }

        /**
         * Gets the user's inventory.
         */
        public List<ItemInstance> GetInventory()
        {
            return m_inventory;
        }

        /**
         * Initialises user data.
         */
        partial void InitialisePartial()
        {
            Debug.Log("Initialising data");

            GetUserDataRequest request = new GetUserDataRequest();

            PlayFabClientAPI.GetUserData(request, OnDataSuccess, OnDataFailure);
        }

        /**
         * Stores the data and initialises statistics.
         */
        private void OnDataSuccess(GetUserDataResult result)
        {
            m_data = result.Data;

            InitialiseStatistics();
        }

        /**
         * Logs a warning and initialises statistics.
         */
        private void OnDataFailure(PlayFabError error)
        {
            Debug.LogWarning("Couldn't get data: " + error.ErrorMessage);

            InitialiseStatistics();
        }

        /**
         * Initialises user statistics.
         */
        private void InitialiseStatistics()
        {
            Debug.Log("Initialising statistics");

            GetPlayerStatisticsRequest request = new GetPlayerStatisticsRequest();

            PlayFabClientAPI.GetPlayerStatistics(request, OnStatisticsSuccess, OnStatisticsFailure);
        }

        /**
         * Stores the statistics and initialises inventory.
         */
        private void OnStatisticsSuccess(GetPlayerStatisticsResult result)
        {
            m_statistics = result.Statistics;

            InitialiseInventory();
        }

        /**
         * Logs a warning and initialises inventory.
         */
        private void OnStatisticsFailure(PlayFabError error)
        {
            Debug.LogWarning("Couldn't get statistics: " + error.ErrorMessage);

            InitialiseInventory();
        }

        /**
         * Initialises user inventory.
         */
        private void InitialiseInventory()
        {
            Debug.Log("Initialising inventory");

            GetUserInventoryRequest request = new GetUserInventoryRequest();

            PlayFabClientAPI.GetUserInventory(request, OnInventorySuccess, OnInventoryFailure);
        }

        /**
         * Stores the inventory and calls success.
         */
        private void OnInventorySuccess(GetUserInventoryResult result)
        {
            m_inventory = result.Inventory;

            m_request.OnSuccess();
        }

        /**
         * Logs a warning and calls failure.
         */
        private void OnInventoryFailure(PlayFabError error)
        {
            Debug.LogWarning("Couldn't get inventory: " + error.ErrorMessage);

            m_request.OnFailure();
        }

        /**
         * Consumes an inventory item.
         */
        public void Consume(ItemInstance item, Action success, Action failure)
        {
            Debug.Log("Consuming item: " + item.DisplayName);

            m_request.SetListeners(success, failure);

            ConsumeItemRequest request = new ConsumeItemRequest
            {
                ItemInstanceId = item.ItemInstanceId,
                ConsumeCount = 1
            };

            PlayFabClientAPI.ConsumeItem(request, OnConsumeSuccess, OnConsumeFailure);
        }

        /**
         * Calls success.
         */
        private void OnConsumeSuccess(ConsumeItemResult result)
        {
            m_request.OnSuccess();
        }

        /**
         * Logs a warning and calls failure.
         */
        private void OnConsumeFailure(PlayFabError error)
        {
            Debug.LogWarning("Couldn't consume item: " + error.ErrorMessage);

            m_request.OnFailure();
        }
    }
}
