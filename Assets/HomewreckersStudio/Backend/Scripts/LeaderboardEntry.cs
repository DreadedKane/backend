/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

namespace Homewreckers.Backend
{
    /**
     * Contains data for an entry in a leaderboard.
     */
    public sealed class LeaderboardEntry
    {
        /** The name of the user this entry represents. */
        private string m_name;

        /** The user's rank. */
        private int m_rank;

        /** The user's score. */
        private float m_score;

        /**
         * Initialises the entry.
         */
        public LeaderboardEntry(string name, int rank, float score)
        {
            m_name = name;
            m_rank = rank;
            m_score = score;
        }

        /** Gets the user's name. */
        public string GetName()
        {
            return m_name;
        }

        /** Gets the user's rank. */
        public int GetRank()
        {
            return m_rank;
        }

        /** Gets the user's score. */
        public float GetScore()
        {
            return m_score;
        }
    }
}
