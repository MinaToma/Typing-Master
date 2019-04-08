using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
    
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
                Instantiate(shot, ship.position, ship.rotation);
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
