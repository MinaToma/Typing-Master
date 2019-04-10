using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    public Transform ship;
    public Vector2 pos;
    public string shipName;
    public bool returnToOrigin = false;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      //  Vector3 offset = playerShip.position - enemyShips[i].transform.position;
        //Quaternion rotation = Quaternion.LookRotation(Vector3.forward, offset);

        //enemyShips[i].transform.rotation = rotation;
    }
}
