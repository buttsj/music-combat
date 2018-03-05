using UnityEngine;

public class InputController : MonoBehaviour {

    public BeatController beatController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            beatController.CheckForBeat(KeyCode.DownArrow);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            beatController.CheckForBeat(KeyCode.LeftArrow);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            beatController.CheckForBeat(KeyCode.RightArrow);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            beatController.CheckForBeat(KeyCode.UpArrow);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            beatController.CheckForBeat(KeyCode.Space);
        }
    }
}
