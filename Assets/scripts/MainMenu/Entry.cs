using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Assets.scripts.MainMenu
{
    /// <summary>
    /// this is a score entry.
    /// Parameter: Name, lastLevel the player reached, this score, the difficulty the player plays the game, time is the timestamp when the player make the score
    /// </summary>
    public class Entry : IComparable<Entry>
    {
        private string entryName;
        private int entryLastLevel;
        private int entryScore;
        private float entryDifficulty;
        private long entryTime;

        /// <summary>
        /// Entry in the score file
        /// </summary>
        /// <param name="name">name of the player</param>
        /// <param name="lastLevel">which is the last level reached</param>
        /// <param name="score">coins collected = score reached</param>
        /// <param name="difficulty">difficulty setting played in</param>
        /// <param name="t">time of completion</param>
        public Entry(string name, int lastLevel, int score, float difficulty, long t)
        {
            entryName = name;
            entryLastLevel = lastLevel;
            entryScore = score;
            entryDifficulty = difficulty;
            entryTime = t;
        }

        /// <summary>
        /// Entry in the score file
        /// </summary>
        /// <param name="name">name of the player</param>
        /// <param name="lastLevel">which is the last level reached</param>
        /// <param name="score">coins collected = score reached</param>
        /// <param name="difficulty">difficulty setting played in</param>
        public Entry(string name, int lastLevel, int score, float difficulty)
        {
            entryName = name;
            entryLastLevel = lastLevel;
            entryScore = score;
            entryDifficulty = difficulty;
            entryTime = System.DateTime.Now.Ticks;
        }

        /// <summary>
        /// name of the entry
        /// </summary>
        public string name
        {
            get { return entryName; }
        }
        /// <summary>
        /// last level reached by the player
        /// </summary>
        public int lastLevel
        {
            get { return entryLastLevel; }
        }
        /// <summary>
        /// score the player reached
        /// </summary>
        public int score
        {
            get { return entryScore; }
        }
        /// <summary>
        /// difficulty setting played in
        /// </summary>
        public float difficulty
        {
            get { return entryDifficulty; }
        }
        /// <summary>
        /// the time the entry was created
        /// </summary>
        public long time
        {
            get { return entryTime; }
        }
        /// <summary>
        /// method to sort the entries
        /// </summary>
        /// <param name="e">the entry being sorted</param>
        /// <returns>number to indicate sorting order</returns>
        public int CompareTo(Entry e)
        {
            if (entryScore < e.score)
            {
                return 1;
            }
            else if (entryScore == e.score)
            {
                if (entryDifficulty < e.difficulty)
                {
                    return 1;
                }
                else if (entryDifficulty > e.difficulty)
                {
                    return -1;
                }

                if (entryTime < e.time)
                {
                    return -1;
                }
                else if (entryTime > e.time)
                {
                    return 1;
                }

                return 0;
            }

            return -1;
        }
    }
}
