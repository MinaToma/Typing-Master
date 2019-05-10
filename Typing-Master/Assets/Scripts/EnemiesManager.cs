using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class EnemiesManager : MonoBehaviour
{

    List<String> [] wordsGrid = new List<string>[26];
    static List<String> words = new List<String>();

    static int dropedEnemies = 4;

    public GameObject enemyPrefab;
    public static GameObject workArroundEnemyPrefab;

    static float speed = 0.4f;

    static int stage = 1;
    public Sprite[] enemySprites;
    public static Sprite[] workArroundSprites; 
    public static List<GameObject> enemyShips = new List<GameObject>();


    public GameObject playerShip;
    public static GameObject workArroundPlayerShip;

  

    public static void MakeRandomShip(int charRow)
    {
        int arraIdx = UnityEngine.Random.Range(0, workArroundSprites.Length);
        Sprite shipSprite = workArroundSprites[arraIdx];
        string shipName = shipSprite.name;

        GameObject go = Instantiate(workArroundEnemyPrefab);
        go.name = shipName;
        go.GetComponent<EnemyClass>().shipName = shipName;


        //go.GetComponentInChildren<TextMesh>().offsetZ = 500;
        //go.GetComponentInChildren<TextMesh>().fontSize = 7;
        go.GetComponentInChildren<MeshRenderer>().sortingLayerName = "Player";
        go.GetComponentInChildren<MeshRenderer>().sortingOrder = 50;
        go.GetComponentInChildren<TextMesh>().text = words[charRow];
        //go.GetComponentInChildren<TextMesh>().transform.rotation

        go.GetComponent<SpriteRenderer>().transform.position = new Vector2(UnityEngine.Random.Range(-6, 6), UnityEngine.Random.Range(4.5f, 8f));

        go.GetComponent<SpriteRenderer>().sprite = shipSprite;
        Physics2D.IgnoreCollision(go.GetComponent<Collider2D>(), go.GetComponent<Collider2D>());
        enemyShips.Add(go);
    }

    static void setSpeed(GameObject go , Transform ship)
    {
        double angle;
        if (go.transform.position.x == ship.position.x)
            angle = 1 - 1e-9;
        else angle = (go.transform.position.y - ship.position.y) / (go.transform.position.x - ship.position.x);

        double velX = Math.Sin(angle) * speed;

        int dir = (go.transform.position.x < ship.transform.position.x) ? 1 : -1;
        go.GetComponent<Rigidbody2D>().velocity = new Vector2((float)Math.Abs(velX) * dir, (float)-0.3f);
    }

    void Start()
    {

        wordsGrid = new List<string>[26];
        words = new List<String>();

         dropedEnemies = 6;
        
         enemyShips = new List<GameObject>();
        for (int i = 0; i < 26; i++)
        {
            wordsGrid[i] = new List<string>();
        }

        readWords();

        workArroundSprites = enemySprites;
        workArroundPlayerShip = playerShip;
        workArroundEnemyPrefab = enemyPrefab;

        for (int i = 0; i < dropedEnemies; i++)
        {
            MakeRandomShip(i);
        }
    }
    
    void Update()
    {
       
        for (int i = 0; i < enemyShips.Count; i++)
        {
            
            Vector3 offset = playerShip.transform.position - enemyShips[i].transform.position;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, offset);
            enemyShips[i].transform.rotation = rotation;
            setSpeed(enemyShips[i], playerShip.transform);
        }
       
    }

    static public bool hit(GameObject go)
    {
        
        if (go.GetComponentInChildren<TextMesh>().text.Length == 1)
        {
            go.GetComponentInChildren<TextMesh>().text = "=_=";
            Destroy(go, 2f);
            enemyShips.Remove(go);

            if(enemyShips.Count <= 12)
            {
                for(int i = 0; i < stage; i++)
                    EnemiesManager.MakeRandomShip(dropedEnemies++);
                stage++;
            }
            return true;
        }
        go.GetComponentInChildren<TextMesh>().text = go.GetComponentInChildren<TextMesh>().text.Substring(1);
        return false;
    }


    public void readWords()
    {
        FileStream fs = new FileStream("Assets/words/words_lineByLine.txt", FileMode.Open);
        StreamReader sr = new StreamReader(fs);

        for (int i = 0; sr.Peek() != -1; i++)
        {
            String[] line = sr.ReadLine().Split(',');
            for (int j = 0; j < line.Length; j++)
            {
                wordsGrid[i].Add(line[j]);
            }
        }

        for (int j = 0; true; j++)
        {
            bool toBreak = true;
            for (int i = 0; i < wordsGrid.Length; i++)
            {
                if (j >= wordsGrid[i].Count)
                    continue;
                words.Add(wordsGrid[i][j]);
                toBreak = false;
            }
            if (toBreak == true)
                break;
        }

        sr.Close();
        fs.Close();


    }
}

