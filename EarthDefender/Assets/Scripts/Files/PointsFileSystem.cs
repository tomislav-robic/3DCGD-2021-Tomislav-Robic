using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class PointsFileSystem
{
    public static void SaveScore(PointsFile pointsFile)
    {
        List<PointsFile> pointsEntries = LoadScores();
        if (pointsEntries == null)
        {
            pointsEntries = new List<PointsFile>();
        }
        pointsEntries.Add(pointsFile);
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "scores.bin");
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, pointsEntries);
        stream.Close();
    }

    public static List<PointsFile> LoadScores()
    {
        string path = Path.Combine(Application.persistentDataPath, "scores.bin");
        if (File.Exists(path))
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                List<PointsFile> pointsEntries = formatter.Deserialize(stream) as List<PointsFile>;
                stream.Close();

                return pointsEntries;
            } catch (Exception ex)
            {
                Debug.LogWarning($"{path} is inaccessible. {ex.GetType().Name}");
            }
        }
        else
        {
            Debug.LogWarning("Scores file not found in " + path);
        }
        return null;
    }

    public static void ClearScores()
    {
        List<PointsFile> pointsEntries = new List<PointsFile>();
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "scores.bin");
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, pointsEntries);
        stream.Close();
    }

}
