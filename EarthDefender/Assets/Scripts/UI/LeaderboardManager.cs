using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    Transform entries;
    Transform template;
    List<Transform> entryTransforms;
    List<PointsFile> pointsEntries;
    public int maxPointsEntries = 10;

    private void Awake()
    {
        entries = GameObject.Find("Entries").transform;
        template = GameObject.Find("Template").transform;
        pointsEntries = PointsFileSystem.LoadScores();

        template.gameObject.SetActive(false);

        if (pointsEntries != null)
        {
            for (int i = 0; i < pointsEntries.Count; i++)
            {
                for (int j = i + 1; j < pointsEntries.Count; j++)
                {
                    if (pointsEntries[j].points > pointsEntries[i].points)
                    {
                        PointsFile tmp = pointsEntries[i];
                        pointsEntries[i] = pointsEntries[j];
                        pointsEntries[j] = tmp;
                    }
                }
            }

            if (pointsEntries.Count > maxPointsEntries)
            {
                pointsEntries.RemoveRange(maxPointsEntries, pointsEntries.Count - maxPointsEntries);
            }

            entryTransforms = new List<Transform>();
            foreach (PointsFile entry in pointsEntries)
            {
                CreateHighscoreEntry(entry, entries, entryTransforms);
            }
        }
    }

    void CreateHighscoreEntry(PointsFile entry, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(template, container);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;

        entryTransform.Find("PosText").GetComponent<Text>().text = rank.ToString();

        float score = entry.points;

        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();

        string name = entry.name;

        entryTransform.Find("NameText").GetComponent<Text>().text = name;

        transformList.Add(entryTransform);
    }

    class HighscoreEntry
    {
        public int score;
        public string name;
    }
}
