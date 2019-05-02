using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class leaderboard : MonoBehaviour
{

    public void plotAll()
    {
        using (var sr = new StreamReader("playerNames.txt"))
        {
            while (sr.Peek() != -1)
            {
                graphDrawer.addScoreToList(sr.ReadLine() + ".txt");
                graphDrawer.showGraph(graphDrawer.value, 2, 2, 2);
            }
            sr.Close();
        }
    }
}


