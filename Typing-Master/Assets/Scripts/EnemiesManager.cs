using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class EnemiesManager : MonoBehaviour
{

    List<String> [] words= new List<string>[26];


    public GameObject enemyPrefab;
    public Sprite[] enemySprites;
    public static List<GameObject> enemyShips = new List<GameObject>();
    public GameObject playerShip;

    public void MakeRandomShip(int charRow)
    {
        int arraIdx = UnityEngine.Random.Range(0, enemySprites.Length);
        Sprite shipSprite = enemySprites[arraIdx];
        string shipName = shipSprite.name;

        GameObject go = Instantiate(enemyPrefab);
        go.name = shipName;
        go.GetComponent<EnemyClass>().shipName = shipName;


        go.GetComponentInChildren<TextMesh>().offsetZ = 500;
        //go.GetComponentInChildren<TextMesh>().fontSize = 7;
        go.GetComponentInChildren<MeshRenderer>().sortingLayerName = "Player";
        go.GetComponentInChildren<MeshRenderer>().sortingOrder = 50;
        go.GetComponentInChildren<TextMesh>().text = words[charRow][0];
        //go.GetComponentInChildren<TextMesh>().transform.rotation

        go.GetComponent<SpriteRenderer>().transform.position = new Vector2(UnityEngine.Random.Range(-8, 8), UnityEngine.Random.Range(4.5f, 10f));

        go.GetComponent<SpriteRenderer>().sprite = shipSprite;
        float xSpeed = 0;
        if (go.transform.position.x < playerShip.transform.position.x)
        {
            xSpeed = 0.1f;
        }
        if (go.transform.position.x > playerShip.transform.position.x)
        {
            xSpeed = -0.1f;
        }
        go.GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed , -0.3f);
        Physics2D.IgnoreCollision(go.GetComponent<Collider2D>(), go.GetComponent<Collider2D>());
        enemyShips.Add(go);
    }

    void Start()
    {
        for(int i = 0; i < 26; i++)
        {
            words[i] = new List<string>();
        }
       readWords();
        for(int i = 0; i < 15; i++)
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
            float xSpeed = 0;
            if (enemyShips[i].transform.position.x < playerShip.transform.position.x)
            {
                xSpeed = 0.1f;
            }
            if (enemyShips[i].transform.position.x > playerShip.transform.position.x)
            {
                xSpeed = -0.1f;
            }
            enemyShips[i].GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, -0.3f);
        }
       
    }

    static public bool hit(GameObject go)
    {
        if (go.GetComponentInChildren<TextMesh>().text.Length == 1)
        {
            go.GetComponentInChildren<TextMesh>().text = "=_=";
            Destroy(go, 2f);
            enemyShips.Remove(go);
            return true;
        }
        go.GetComponentInChildren<TextMesh>().text = go.GetComponentInChildren<TextMesh>().text.Substring(1);
        return false;
    }


    public void readWords()
    {
        FileStream fs = new FileStream("Assets/words/words_lineByLine.txt", FileMode.Open);
        StreamReader sr = new StreamReader(fs);

        for(int i = 0; sr.Peek() != -1; i++)
        {
            String[] line = sr.ReadLine().Split(',');
            for(int j = 0; j < line.Length; j++) {
                words[i].Add(line[j]);
            }
        }

        sr.Close();
        fs.Close();
    }
}

