using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
//

    
    public GameObject shot;
    public Transform ship;
    public float targetX, targetY, angle;
    public Transform target;
    void Start ()
    {
    }
	
	void Update ()
    {
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(vKey))
            {
                Vector3 shotPos = ship.position;
                shotPos.y += 0.5f;
                Instantiate(shot, shotPos, ship.rotation);
            }
        }

        if (Input.GetKeyDown("left"))
            rotate();
    }

    void rotate()
    {
        Vector3 offset = target.position - ship.position;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, offset);
        ship.rotation = rotation;
    }
}
