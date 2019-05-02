using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;


public class Leaderboard : MonoBehaviour{

    public static bool allLeaderboard = false;

    public void plotAll()
    {
        if (!allLeaderboard)
        {
            allLeaderboard = true;
            using (var sr = new StreamReader("Files/playerNames.txt"))
            {
                while (sr.Peek() != -1)
                {
                    string playerName = sr.ReadLine();
                    if (playerName.Equals(graphDrawer.PlayerName)) continue;
                    graphDrawer.addScoreToList("Files/" + playerName + ".txt");
                    graphDrawer.showGraph(graphDrawer.value, UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
                }
                sr.Close();
            }
        }
        else
        {
            allLeaderboard = false;
            SceneManager.LoadScene("MainWindow");

        }
    }
}


