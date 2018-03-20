using System.Collections;
using UnityEngine;

public class PlayerInterface : MonoBehaviour {

    public BeatController beatController;

    public GameObject dance;
    public GameObject retreat;
    public GameObject temp1;
    public GameObject temp2;

    public bool IsDanceSelected
    {
        get { return danceSelected; }
        set { danceSelected = value; }
    }
    private bool danceSelected;

    public bool IsRetreatSelected
    {
        get { return retreatSelected; }
        set { retreatSelected = value; }
    }
    private bool retreatSelected;

    public bool IsTempOneSelected
    {
        get { return tempOneSelected; }
        set { tempOneSelected = value; }
    }
    private bool tempOneSelected;

    public bool IsTempTwoSelected
    {
        get { return tempTwoSelected; }
        set { tempTwoSelected = value; }
    }
    private bool tempTwoSelected;

    private enum CurrentSelection { dance, retreat, temp1, temp2 }
    private CurrentSelection uiSelection;

    public Vector2 upPosition;
    public Vector2 leftPosition;
    public Vector2 rightPosition;
    public Vector2 downPosition;

    private Vector2 selectedSize;
    private Vector2 unselectedSize;

    private bool dancing;
    private float speed;
    private float danceTime;
    private float endDanceTime;

    // Use this for initialization
    void Start () {

        dancing = false;
        speed = 0.3f;
        danceTime = 10.0f;
        endDanceTime = 0.0f;

        uiSelection = CurrentSelection.dance;
        danceSelected = true;
        retreatSelected = false;
        tempOneSelected = false;
        tempTwoSelected = false;

        selectedSize = dance.GetComponent<RectTransform>().sizeDelta;
        unselectedSize = retreat.GetComponent<RectTransform>().sizeDelta;

        upPosition = temp2.transform.localPosition;
        leftPosition = temp1.transform.localPosition;
        rightPosition = dance.transform.localPosition;
        downPosition = retreat.transform.localPosition;
	}
	
    IEnumerator RotateObject(Transform thisTransform, Vector2 startingPos, Vector2 endPos, float time)
    {
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0)
        {
            i += Time.deltaTime * rate;
            thisTransform.localPosition = Vector2.Lerp(startingPos, endPos, i);
            yield return new WaitForEndOfFrame();
        }
    }

	// Update is called once per frame
	void Update () {

        if (dancing)
        {
            if (beatController.GetElapsedTime >= endDanceTime)
            {
                dancing = false;
                beatController.AreWeDancing = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                switch (uiSelection)
                {
                    case CurrentSelection.dance:
                        dancing = true;
                        beatController.AreWeDancing = true;
                        endDanceTime = beatController.GetElapsedTime + danceTime;
                        break;
                    case CurrentSelection.retreat:
                        break;
                    case CurrentSelection.temp1:
                        break;
                    case CurrentSelection.temp2:
                        break;
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                switch (uiSelection)
                {
                    case CurrentSelection.dance:
                        uiSelection = CurrentSelection.retreat;
                        danceSelected = false;
                        retreatSelected = true;
                        dance.GetComponent<RectTransform>().sizeDelta = unselectedSize;
                        retreat.GetComponent<RectTransform>().sizeDelta = selectedSize;
                        StartCoroutine(RotateObject(dance.transform, dance.transform.localPosition, upPosition, speed));
                        StartCoroutine(RotateObject(retreat.transform, retreat.transform.localPosition, rightPosition, speed));
                        StartCoroutine(RotateObject(temp1.transform, temp1.transform.localPosition, downPosition, speed));
                        StartCoroutine(RotateObject(temp2.transform, temp2.transform.localPosition, leftPosition, speed));
                        break;
                    case CurrentSelection.retreat:
                        uiSelection = CurrentSelection.temp1;
                        retreatSelected = false;
                        tempOneSelected = true;
                        retreat.GetComponent<RectTransform>().sizeDelta = unselectedSize;
                        temp1.GetComponent<RectTransform>().sizeDelta = selectedSize;
                        StartCoroutine(RotateObject(dance.transform, dance.transform.localPosition, leftPosition, speed));
                        StartCoroutine(RotateObject(retreat.transform, retreat.transform.localPosition, upPosition, speed));
                        StartCoroutine(RotateObject(temp1.transform, temp1.transform.localPosition, rightPosition, speed));
                        StartCoroutine(RotateObject(temp2.transform, temp2.transform.localPosition, downPosition, speed));
                        break;
                    case CurrentSelection.temp1:
                        uiSelection = CurrentSelection.temp2;
                        tempOneSelected = false;
                        tempTwoSelected = true;
                        temp1.GetComponent<RectTransform>().sizeDelta = unselectedSize;
                        temp2.GetComponent<RectTransform>().sizeDelta = selectedSize;
                        StartCoroutine(RotateObject(dance.transform, dance.transform.localPosition, downPosition, speed));
                        StartCoroutine(RotateObject(retreat.transform, retreat.transform.localPosition, leftPosition, speed));
                        StartCoroutine(RotateObject(temp1.transform, temp1.transform.localPosition, upPosition, speed));
                        StartCoroutine(RotateObject(temp2.transform, temp2.transform.localPosition, rightPosition, speed));
                        break;
                    case CurrentSelection.temp2:
                        uiSelection = CurrentSelection.dance;
                        tempTwoSelected = false;
                        danceSelected = true;
                        temp2.GetComponent<RectTransform>().sizeDelta = unselectedSize;
                        dance.GetComponent<RectTransform>().sizeDelta = selectedSize;
                        StartCoroutine(RotateObject(dance.transform, dance.transform.localPosition, rightPosition, speed));
                        StartCoroutine(RotateObject(retreat.transform, retreat.transform.localPosition, downPosition, speed));
                        StartCoroutine(RotateObject(temp1.transform, temp1.transform.localPosition, leftPosition, speed));
                        StartCoroutine(RotateObject(temp2.transform, temp2.transform.localPosition, upPosition, speed));
                        break;
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                switch (uiSelection)
                {
                    case CurrentSelection.dance:
                        uiSelection = CurrentSelection.temp2;
                        danceSelected = false;
                        tempTwoSelected = true;
                        dance.GetComponent<RectTransform>().sizeDelta = unselectedSize;
                        temp2.GetComponent<RectTransform>().sizeDelta = selectedSize;
                        StartCoroutine(RotateObject(dance.transform, dance.transform.localPosition, downPosition, speed));
                        StartCoroutine(RotateObject(retreat.transform, retreat.transform.localPosition, leftPosition, speed));
                        StartCoroutine(RotateObject(temp1.transform, temp1.transform.localPosition, upPosition, speed));
                        StartCoroutine(RotateObject(temp2.transform, temp2.transform.localPosition, rightPosition, speed));
                        break;
                    case CurrentSelection.retreat:
                        uiSelection = CurrentSelection.dance;
                        retreatSelected = false;
                        danceSelected = true;
                        retreat.GetComponent<RectTransform>().sizeDelta = unselectedSize;
                        dance.GetComponent<RectTransform>().sizeDelta = selectedSize;
                        StartCoroutine(RotateObject(dance.transform, dance.transform.localPosition, rightPosition, speed));
                        StartCoroutine(RotateObject(retreat.transform, retreat.transform.localPosition, downPosition, speed));
                        StartCoroutine(RotateObject(temp1.transform, temp1.transform.localPosition, leftPosition, speed));
                        StartCoroutine(RotateObject(temp2.transform, temp2.transform.localPosition, upPosition, speed));
                        break;
                    case CurrentSelection.temp1:
                        uiSelection = CurrentSelection.retreat;
                        tempOneSelected = false;
                        retreatSelected = true;
                        temp1.GetComponent<RectTransform>().sizeDelta = unselectedSize;
                        retreat.GetComponent<RectTransform>().sizeDelta = selectedSize;
                        StartCoroutine(RotateObject(dance.transform, dance.transform.localPosition, upPosition, speed));
                        StartCoroutine(RotateObject(retreat.transform, retreat.transform.localPosition, rightPosition, speed));
                        StartCoroutine(RotateObject(temp1.transform, temp1.transform.localPosition, downPosition, speed));
                        StartCoroutine(RotateObject(temp2.transform, temp2.transform.localPosition, leftPosition, speed));
                        break;
                    case CurrentSelection.temp2:
                        uiSelection = CurrentSelection.temp1;
                        tempTwoSelected = false;
                        tempOneSelected = true;
                        temp2.GetComponent<RectTransform>().sizeDelta = unselectedSize;
                        temp1.GetComponent<RectTransform>().sizeDelta = selectedSize;
                        StartCoroutine(RotateObject(dance.transform, dance.transform.localPosition, leftPosition, speed));
                        StartCoroutine(RotateObject(retreat.transform, retreat.transform.localPosition, upPosition, speed));
                        StartCoroutine(RotateObject(temp1.transform, temp1.transform.localPosition, rightPosition, speed));
                        StartCoroutine(RotateObject(temp2.transform, temp2.transform.localPosition, downPosition, speed));
                        break;
                }
            }
        }
	}

}
