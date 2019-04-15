using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemiesManager : MonoBehaviour
{
    //TO-DO: READ FROM FILE
    //TO-DO: ROTATE THE TEXT

    String[] arr = { "Hi", "Test", "Welcome", "Game", "Mina", "Mehiesen", "Ahmed", "Sobhy", "Mariam" };
    public GameObject enemyPrefab;
    public Sprite[] enemySprites;
    public static List<GameObject> enemyShips = new List<GameObject>();
    public GameObject playerShip;
    public void MakeRandomShip()
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
        go.GetComponentInChildren<TextMesh>().text = arr[UnityEngine.Random.Range(0, arr.Length)];
        //go.GetComponentInChildren<TextMesh>().transform.rotation

        go.GetComponent<SpriteRenderer>().transform.position = new Vector2(UnityEngine.Random.Range(-4, 4), UnityEngine.Random.Range(4.5f, 10f));

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
       for(int i = 0; i < 15; i++)
        {
            MakeRandomShip();
        }
    }

    void Update()
    {
        Console.WriteLine("Test");
       
        for (int i = 0; i < 15; i++)
        {
            Vector3 offset = playerShip.transform.position - enemyShips[i].transform.position;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, offset);
            enemyShips[i].transform.rotation = rotation;
        }
       
    }

}
