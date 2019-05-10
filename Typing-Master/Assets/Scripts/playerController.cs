using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Diagnostics;
using UnityEngine.SceneManagement;
public class playerController : MonoBehaviour {
    
    public GameObject shot;
    public Transform ship;
    public float angle;
    private GameObject target = null;
    private  Stopwatch timer;
    private  int correctStrokes, totalStrokes;
    private  int avgWPM ;
    private bool startTime;

    void Start ()
    {
        timer = new Stopwatch();
        totalStrokes = correctStrokes = 0;
        avgWPM = 0;
        startTime = false;
    }

    void Update ()
    {

        foreach (GameObject go in EnemiesManager.enemyShips)
        {
            if(go.transform.position.y <4.5 && !startTime)
            {
                startTime = true;
                timer.Start();
            }

            double dist = Math.Sqrt((go.transform.position.x - ship.position.x) * (go.transform.position.x - ship.position.x) + (go.transform.position.y - ship.position.y) * (go.transform.position.y - ship.position.y) );
            if (dist <= 1)
            {
                die();
                break;
            }
        }
       ship.GetComponent<Rigidbody2D>().velocity = new Vector2(0 , 0);
       foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if(Input.GetKeyDown(vKey))
            {
                string key = vKey.ToString();
                key = key.ToLower();
              
                if (target == null)
                {
                    foreach(GameObject go in EnemiesManager.enemyShips)
                     {
                       
                        if(key[0].Equals(go.GetComponentInChildren<TextMesh>().text[0]) && go.transform.position.y < 4.5)
                        {
                            
                            Vector3 shotPos = ship.position;
                            shotPos.y += 0.5f;
                            target = go;

                            TextMesh textmesh = target.GetComponentInChildren<TextMesh>();
                            textmesh.color = Color.yellow;

                            rotate();


                            GameObject shotObj = Instantiate(shot, shotPos, ship.rotation);

                            Destroy(shotObj, Vector3.Distance(shotObj.transform.position, go.transform.position)/5);
                            correctStrokes++;
                            if (EnemiesManager.hit(go))
                                target = null;

                            break;
                        }
                    }
                }

                else if(key[0].Equals(target.GetComponentInChildren<TextMesh>().text[0]))
                {
                    correctStrokes++;
                    Vector3 shotPos = ship.position;
                    shotPos.y += 0.5f;
                    rotate();

                    TextMesh textmesh = target.GetComponentInChildren<TextMesh>();
                    textmesh.color = Color.yellow;

                    GameObject shotObj = Instantiate(shot, shotPos, ship.rotation);

                    Destroy(shotObj, Vector3.Distance(shotObj.transform.position, target.transform.position) / 5);

                    if (EnemiesManager.hit(target))
                        target = null;
                }
                totalStrokes++;
                     avgWPM += wpm();
            }
        }
    }

    void rotate()
    {
        Vector3 offset = target.transform.position - ship.position;
        offset.y -= 1;

        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, offset);
        ship.rotation = rotation;
    }
    private int wpm()
    {
        return (int)Math.Round(((correctStrokes / 5.0) / (timer.ElapsedMilliseconds / 60000.0)) * ((double)correctStrokes / totalStrokes));
    }

     void die()
    {
        FileStream fs = new FileStream("Files/Player.txt", FileMode.Open);
        StreamReader sr = new StreamReader(fs);
        string playerName = "Files/"+ sr.ReadLine();
        playerName += ".txt";
        print(playerName);
        sr.Close();
        fs.Close();
        if (totalStrokes != 0)
            exportScore((int)Math.Ceiling((avgWPM / totalStrokes) * 1.5f) , playerName);
        else exportScore(0, playerName);
        SceneManager.LoadScene("MainWindow");
    }

    void exportScore(int score, string fileName)
    {   
        FileStream fs = new FileStream(fileName, FileMode.Append);
        StreamWriter Sw = new StreamWriter(fs);
        Sw.WriteLine(score);
        Sw.Close();
        fs.Close();
    }
}

