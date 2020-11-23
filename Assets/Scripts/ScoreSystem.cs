using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class ScoreSystem
{
    // Constants
    private static readonly string SCORE_FOLDER = Application.dataPath + "/Scores/";

    // Directory initialization
    public static void Init()
    {
        if (!Directory.Exists(SCORE_FOLDER))
        {
            Directory.CreateDirectory(SCORE_FOLDER);
        }
    }

    // Saves scores to file
    public static void Save(string scoreString)
    {
        File.WriteAllText(SCORE_FOLDER + "/scores.txt", scoreString);
    }

    // Loads scores from file
    public static string Load()
    {
        if (File.Exists(SCORE_FOLDER + "/scores.txt"))
        {
            return File.ReadAllText(SCORE_FOLDER + "/scores.txt");
        }
        else
        {
            return null;
        }
    }
}
