using UnityEngine.UI;
using UnityEngine;

public class BeatObject : MonoBehaviour {

    public GameObject beatZone;
    
    public bool MissedBeat
    {
        get { return missed; }
        set { missed = value; }
    }
    private bool missed = false;

    public bool IsOriginal
    {
        get { return original; }
        set { original = value; }
    }
    private bool original = true;

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

    public float DistanceToCenter
    {
        get { return distance; }
    }
    private float distance;

    public BeatType myBeatType;
    public enum BeatType { Left, Right, Up, Down, Normal, Enemy };

    
    // Use this for initialization
    void Start () {
        distance = transform.position.x - beatZone.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {

        if (launched && !original)
        {
            if (!evil)
            {
                transform.position = new Vector3(transform.position.x + 5, transform.position.y);
            }
            else if (evil)
            {
                transform.position = new Vector3(transform.position.x - 5, transform.position.y);
            }
            
            distance = transform.position.x - beatZone.transform.position.x;

            CheckExpired();
        }
    }

    public void CreateNewBeat (BeatType beat)
    {
        myBeatType = beat;

        switch (myBeatType)
        {
            case BeatType.Normal:
                break;

            case BeatType.Up:
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/circleUp");
                break;

            case BeatType.Down:
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/circleDown");
                break;

            case BeatType.Left:
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/circleLeft");
                break;

            case BeatType.Right:
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/circleRight");
                break;

            case BeatType.Enemy:
                break;
        }
    }

    void CheckExpired()
    {
        if (evil && !original && distance < -600)
        {
            Destroy(gameObject);
        }
        else if (!evil && !original && distance > 600)
        {
            Destroy(gameObject);
        }
    }
}
