using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

namespace Assets.scripts.MainMenu
{
    /// <summary>
    /// this class save the score on the disc or read from disc
    /// </summary>
    public class Highscore : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }
        // path to save the score
        private static string scorePath = Application.dataPath + "/Score.txt";
        private static char seperator = '$';

        /// <summary>
        /// read the score file, this isn't sorted
        /// </summary>
        /// <returns>sorted list of score entries in file</returns>
        public static List<Entry> readScore()
        {
            List<Entry> list = new List<Entry>();
            if (!File.Exists(scorePath))
            {
                return list;
            }

            TextReader tr = new StreamReader(scorePath);
            string line;
            while ((line = tr.ReadLine()) != null)
            {
                Entry temp = lineToEntry(line);
                if (temp != null)
                {
                    list.Add(temp);
                }
            }

            tr.Close();
            return list;
        }

        /// <summary>
        /// add a entry into score file
        /// </summary>
        /// <param name="e"></param>
        public static void writeNewScore(Entry e)
        {
            TextWriter tw = File.AppendText(scorePath);
            if (tw == null)
            {
                Debug.Log("can't find file");
                return;
            }

            tw.WriteLine(buildString(e));
            tw.Close();
        }

        private static Entry lineToEntry(string s)
        {
            char[] temp = { seperator };
            string[] x = s.Split(temp);
            if (x.Length != 5)
            {
                return null;
            }

            return new Entry(x[0], int.Parse(x[1]), int.Parse(x[2]), float.Parse(x[3]), long.Parse(x[4]));
        }

        private static string buildString(Entry e)
        {
            return e.name + seperator + e.lastLevel + seperator + e.score + seperator + e.difficulty + seperator + e.time;
        }
    }
}