/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;

namespace Homewreckers.Backend
{
    /**
     * Implements the PlayFab-specific part.
     */
    public partial class Leaderboard
    {
        /**
         * Gets leaderboard entries from the current position.
         */
        partial void InitialisePartial()
        {
            GetLeaderboardRequest request = new GetLeaderboardRequest()
            {
                StatisticName = m_name,
                StartPosition = m_position,
                MaxResultsCount = m_maxEntries
            };

            PlayFabClientAPI.GetLeaderboard(request, OnEntriesSuccess, OnFailure);
        }

        /**
         * Initialises the entry list and calls success.
         */
        private void OnEntriesSuccess(GetLeaderboardResult result)
        {
            InitialiseEntries(result.Leaderboard);

            m_request.OnSuccess();
        }

        /**
         * Gets leaderboard entries around the user.
         */
        partial void InitialiseUserPartial()
        {
            GetLeaderboardAroundPlayerRequest request = new GetLeaderboardAroundPlayerRequest()
            {
                StatisticName = m_name,
                MaxResultsCount = m_maxEntries
            };

            PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnUserSuccess, OnFailure);
        }

        /**
         * Initialises the entry list and calls success.
         */
        private void OnUserSuccess(GetLeaderboardAroundPlayerResult result)
        {
            InitialiseEntries(result.Leaderboard);

            m_request.OnSuccess();
        }

        /**
         * Initialises the entry list using PlayFab data.
         */
        private void InitialiseEntries(List<PlayerLeaderboardEntry> playFabEntries)
        {
            m_entries = new List<LeaderboardEntry>();

            foreach (var playFabEntry in playFabEntries)
            {
                LeaderboardEntry entry = new LeaderboardEntry(
                    playFabEntry.Profile.DisplayName,
                    playFabEntry.Position + 1,
                    playFabEntry.StatValue
                );

                m_entries.Add(entry);
            }
        }

        /**
         * Logs a warning and calls failure.
         */
        private void OnFailure(PlayFabError error)
        {
            Debug.LogWarning("Couldn't get entries: " + error.ErrorMessage);

            m_request.OnFailure();
        }
    }
}
