using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class BeatController : MonoBehaviour {

    public GameObject enemyBar;         // holder of Enemy Beats
    public GameObject playerBar;        // holder of Player Beats
    
    public GameObject enemyBeat;        // original Enemy Beat
    public GameObject playerBeat;       // original Player Beat

    public GameObject beatZone;         // the center of the music bar

    private GameObject currentEnemyBeat;
    private GameObject currentPlayerBeat;

    public TextAsset csvFile;

    private string[] beatTimestamps;    // every timestamp of a beat in a song
    private List<string> deliveryBeats; // beats to deploy a "Beat" on screen
    
    private int cursor;
    private int angerBuildUp;
    
    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        currentPlayerBeat = null;
        cursor = 1;
        angerBuildUp = 0;
        beatTimestamps = csvFile.text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        CreateBeats();
	}
	
    /// <summary>
    /// 
    /// </summary>
	void Update()
    {
        if (currentPlayerBeat != null)
        {
            //currentPlayerBeat.GetComponent<Outline>().enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckForBeat();
        }

        // Generates new beat object when time to next beat is less than or equal to 3 seconds
        if (float.Parse(deliveryBeats[cursor]) - Time.timeSinceLevelLoad <= 3.0f)
        {
            if (angerBuildUp >= 10)
            {
                angerBuildUp = 0;
                GameObject newBeat = Instantiate(enemyBeat, enemyBeat.transform.position, enemyBeat.transform.rotation, enemyBar.transform);
                newBeat.GetComponent<BeatObject>().IsEvil = true;
                newBeat.GetComponent<BeatObject>().IsLaunched = true;
                newBeat.GetComponent<BeatObject>().IsOriginal = false;
                newBeat.GetComponent<BeatObject>().CreateNewBeat(BeatObject.BeatType.Enemy);
            }
            else if (UnityEngine.Random.Range(0, 10) == 1)
            {
                GameObject newBeat = Instantiate(enemyBeat, enemyBeat.transform.position, enemyBeat.transform.rotation, enemyBar.transform);
                newBeat.GetComponent<BeatObject>().IsEvil = true;
                newBeat.GetComponent<BeatObject>().IsLaunched = true;
                newBeat.GetComponent<BeatObject>().IsOriginal = false;
                newBeat.GetComponent<BeatObject>().CreateNewBeat(BeatObject.BeatType.Enemy);
            }
            else if (UnityEngine.Random.Range(0, 10) == 2)
            {
                GameObject newBeat = Instantiate(enemyBeat, enemyBeat.transform.position, enemyBeat.transform.rotation, enemyBar.transform);
                newBeat.GetComponent<BeatObject>().IsEvil = true;
                newBeat.GetComponent<BeatObject>().IsLaunched = true;
                newBeat.GetComponent<BeatObject>().IsOriginal = false;
                newBeat.GetComponent<BeatObject>().CreateNewBeat(BeatObject.BeatType.Enemy);

                int beatType = UnityEngine.Random.Range(0, 3);
                newBeat = Instantiate(playerBeat, playerBeat.transform.position, playerBeat.transform.rotation, playerBar.transform);
                newBeat.GetComponent<BeatObject>().IsLaunched = true;
                newBeat.GetComponent<BeatObject>().IsOriginal = false;
                newBeat.GetComponent<BeatObject>().CreateNewBeat((BeatObject.BeatType)beatType);
            }
            else
            {
                int beatType = UnityEngine.Random.Range(0, 3);
                GameObject newBeat = Instantiate(playerBeat, playerBeat.transform.position, playerBeat.transform.rotation, playerBar.transform);
                newBeat.GetComponent<BeatObject>().IsLaunched = true;
                newBeat.GetComponent<BeatObject>().IsOriginal = false;
                newBeat.GetComponent<BeatObject>().CreateNewBeat((BeatObject.BeatType)beatType);
            }
            cursor++; // move cursor to the next beat in song
        }

        //FindExpiredBeats();

        MonitorCurrentBeats();
        
    }

    /// <summary>
    /// 
    /// </summary>
    void CreateBeats()
    {
        deliveryBeats = new List<string>();

        for (int i = 5; i < beatTimestamps.Length; i += 5)
        {
            deliveryBeats.Add(beatTimestamps[i]);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void CheckForBeat()
    {
        if (currentPlayerBeat != null)
        {
            BeatObject beatScript = currentPlayerBeat.GetComponent<BeatObject>();
            
            if (!beatScript.IsOriginal && 
                !beatScript.MissedBeat && 
                beatScript.DistanceToCenter >= -40 && 
                beatScript.DistanceToCenter <= 40)
            {
                if (Input.GetKeyDown(KeyCode.Space) && 
                    Input.GetKeyDown(KeyCode.A) && 
                    beatScript.myBeatType == BeatObject.BeatType.Fire)
                {
                    Debug.Log("Destroying fire beat.");
                    UpdateCurrentPlayerBeat(true);
                }
                else if (Input.GetKeyDown(KeyCode.Space) &&
                         Input.GetKeyDown(KeyCode.S) && 
                         beatScript.myBeatType == BeatObject.BeatType.Ice)
                {
                    Debug.Log("Destroying ice beat.");
                    UpdateCurrentPlayerBeat(true);
                }
                else if (Input.GetKeyDown(KeyCode.Space) &&
                         beatScript.myBeatType == BeatObject.BeatType.Normal)
                {
                    Debug.Log("Destroying normal beat.");
                    UpdateCurrentPlayerBeat(true);
                }
                else
                {
                    // player hit beat button, but wrong button combo was used
                }
            }
            else
            {
                // player hit beat button before beat was in center
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void UpdateCurrentPlayerBeat(bool doDestroy)
    {
        //currentPlayerBeat.GetComponent<Outline>().enabled = false;
        if (doDestroy)
        {
            Destroy(currentPlayerBeat);
            angerBuildUp++;
        }
        currentPlayerBeat = null;
    }

    void MonitorCurrentBeats()
    {
        if (currentPlayerBeat == null)
        {
            for (int i = 1; i < playerBar.transform.childCount; i++)
            {
                BeatObject beatScript = playerBar.transform.GetChild(i).gameObject.GetComponent<BeatObject>();
                if (!beatScript.IsOriginal && !beatScript.MissedBeat)
                {
                    currentPlayerBeat = playerBar.transform.GetChild(i).gameObject;
                    break;
                }
            }
        }
        else
        {
            BeatObject beatScript = currentPlayerBeat.GetComponent<BeatObject>();
            if (!beatScript.IsOriginal && !beatScript.MissedBeat && beatScript.DistanceToCenter > 40)
            {
                // Whoops you missed a beat, time to update current player beat
                beatScript.MissedBeat = true;
                UpdateCurrentPlayerBeat(false);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void FindExpiredBeats()
    {
        foreach (Transform beat in playerBar.transform)
        {
            BeatObject beatScript = beat.gameObject.GetComponent<BeatObject>();
            if (!beatScript.IsOriginal && beatScript.DistanceToCenter > 600)
            {
                Destroy(beat.gameObject);
            }
        }

        foreach (Transform beat in enemyBar.transform)
        {
            BeatObject beatScript = beat.gameObject.GetComponent<BeatObject>();
            if (!beatScript.IsOriginal && beatScript.DistanceToCenter < -600)
            {
                Destroy(beat.gameObject);
            }
        }
    }
}
