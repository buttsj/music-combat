using UnityEngine;

public class MusicSphere : MonoBehaviour {

    public void MusicSphereEventHandler(BeatDetection.EventInfo eventInfo)
    {
        if (eventInfo.messageInfo == BeatDetection.EventType.Energy)
        {
            GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
    }

	// Use this for initialization
	void Start () {
        GetComponent<BeatDetection>().CallBackFunction = MusicSphereEventHandler;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

}
