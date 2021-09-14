using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform Cam;
    public float moveRate;
    public Vector2 startPoint;
    //private float startPointx,startPoint;
    public bool lockY;
    // Start is called before the first frame update
    void Start()
    {
        startPoint =new Vector2 ( transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (!lockY)
        {
            transform.position = new Vector3(startPoint.x + Cam.position.x * moveRate, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(startPoint.x, transform.position.y  +Cam.position.y * moveRate, transform.position.z);
        }
    
    }
       
}
