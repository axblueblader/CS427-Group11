using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderScript : MonoBehaviour
{
    public Vector2 newPos;
    public GameObject test;
    // Start is called before the first frame update
    void Start()
    {
        newPos = new Vector2(-8.155842f, -1.48468f);
    }

    // Update is called once per frame
    void Update()
    {
        newPos = new Vector2(-7.155842f, -1.48468f);
        GetComponent<Transform>().position = newPos;
        Debug.Log(test);
    }
}
