using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatObject : MonoBehaviour {

    public bool IsLaunched
    {
        get { return launched; }
        set { launched = value; }
    }
    private bool launched = false;

    public bool IsEvil
    {
        get { return evil; }
        set { evil = value; }
    }
    private bool evil = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (launched && !evil)
        {
            transform.position = new Vector3(transform.position.x + 5, transform.position.y);
        }
        else if (launched && evil)
        {
            transform.position = new Vector3(transform.position.x - 5, transform.position.y);
        }
	}
}
