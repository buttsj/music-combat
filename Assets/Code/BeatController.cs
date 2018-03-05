using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class BeatController : MonoBehaviour {

    public GameObject enemyBar;         // holder of Enemy Beats
    public GameObject playerBar;        // holder of Player Beats
    
    public GameObject enemyBeat;        // original Enemy Beat
    public GameObject playerBeat;       // original Player Beat

    public GameObject beatZone;         // the center of the music bar

    private GameObject currentPlayerBeat;
    private GameObject currentEnemyBeat;

    public TextAsset csvFile;

    private string[] beatTimestamps;    // every timestamp of a beat in a song
    private List<string> deliveryBeats; // beats to deploy a "Beat" on screen
    
    private int cursor;
    private int angerBuildUp;

    private const int middleRegion = 40;
    private float elapsedTime;
    
    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        currentPlayerBeat = null;
        currentEnemyBeat = null;
        cursor = 0;
        angerBuildUp = 0;
        elapsedTime = 0f;
        beatTimestamps = csvFile.text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        CreateBeats();
	}
	
    /// <summary>
    /// 
    /// </summary>
	void Update()
    {
        elapsedTime += Time.deltaTime;

        if (cursor >= deliveryBeats.Count)
        {
            cursor = 0;
            elapsedTime = 0f;
        }

        // Generates new beat object when time to next beat is less than or equal to 3 seconds
        if (float.Parse(deliveryBeats[cursor]) - elapsedTime <= 3.0f)
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

                int beatType = UnityEngine.Random.Range(0, 5);
                newBeat = Instantiate(playerBeat, playerBeat.transform.position, playerBeat.transform.rotation, playerBar.transform);
                newBeat.GetComponent<BeatObject>().IsLaunched = true;
                newBeat.GetComponent<BeatObject>().IsOriginal = false;
                newBeat.GetComponent<BeatObject>().CreateNewBeat((BeatObject.BeatType)beatType);
            }
            else
            {
                int beatType = UnityEngine.Random.Range(0, 5);
                GameObject newBeat = Instantiate(playerBeat, playerBeat.transform.position, playerBeat.transform.rotation, playerBar.transform);
                newBeat.GetComponent<BeatObject>().IsLaunched = true;
                newBeat.GetComponent<BeatObject>().IsOriginal = false;
                newBeat.GetComponent<BeatObject>().CreateNewBeat((BeatObject.BeatType)beatType);
            }
            cursor++; // move cursor to the next beat in song
        }

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
    public void CheckForBeat(KeyCode key)
    {
        if (currentPlayerBeat != null)
        {
            BeatObject beatScript = currentPlayerBeat.GetComponent<BeatObject>();
            
            if (!beatScript.IsOriginal && 
                !beatScript.MissedBeat && 
                beatScript.DistanceToCenter >= -middleRegion && 
                beatScript.DistanceToCenter <= middleRegion)
            {
                switch (beatScript.myBeatType)
                {
                    case BeatObject.BeatType.Up:
                        if (key == KeyCode.UpArrow)
                        {
                            UpdateCurrentPlayerBeat(true);
                        }
                        break;

                    case BeatObject.BeatType.Down:
                        if (key == KeyCode.DownArrow)
                        {
                            UpdateCurrentPlayerBeat(true);
                        }
                        break;

                    case BeatObject.BeatType.Left:
                        if (key == KeyCode.LeftArrow)
                        {
                            UpdateCurrentPlayerBeat(true);
                        }
                        break;

                    case BeatObject.BeatType.Right:
                        if (key == KeyCode.RightArrow)
                        {
                            UpdateCurrentPlayerBeat(true);
                        }
                        break;

                    case BeatObject.BeatType.Normal:
                        if (key == KeyCode.Space)
                        {
                            UpdateCurrentPlayerBeat(true);
                        }
                        break;
                }
            }
            else
            {
                // they hit the key with no beat in range
            }
        }

        if (currentEnemyBeat != null)
        {
            BeatObject beatScript = currentEnemyBeat.GetComponent<BeatObject>();

            if (!beatScript.IsOriginal &&
                !beatScript.MissedBeat &&
                beatScript.DistanceToCenter >= -middleRegion &&
                beatScript.DistanceToCenter <= middleRegion)
            {
                if (key == KeyCode.Space)
                {
                    UpdateCurrentEnemyBeat(true);
                }
            }
            else
            {
                // they hit the key with no beat in range
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void UpdateCurrentPlayerBeat(bool doDestroy)
    {
        if (doDestroy)
        {
            Destroy(currentPlayerBeat);
            angerBuildUp++;
        }
        currentPlayerBeat = null;
    }

    void UpdateCurrentEnemyBeat(bool doDestroy)
    {
        if (doDestroy)
        {
            Destroy(currentEnemyBeat);
        }
        currentEnemyBeat = null;
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
            if (!beatScript.IsOriginal && !beatScript.MissedBeat && beatScript.DistanceToCenter > middleRegion)
            {
                // Whoops you missed a beat, time to update current player beat
                beatScript.MissedBeat = true;
                UpdateCurrentPlayerBeat(false);
            }
        }

        if (currentEnemyBeat == null)
        {
            for (int i = 1; i < enemyBar.transform.childCount; i++)
            {
                BeatObject beatScript = enemyBar.transform.GetChild(i).gameObject.GetComponent<BeatObject>();
                if (!beatScript.IsOriginal && !beatScript.MissedBeat)
                {
                    currentEnemyBeat = enemyBar.transform.GetChild(i).gameObject;
                    break;
                }
            }
        }
        else
        {
            BeatObject beatScript = currentEnemyBeat.GetComponent<BeatObject>();
            if (!beatScript.IsOriginal && !beatScript.MissedBeat && beatScript.DistanceToCenter < -middleRegion)
            {
                // Whoops you missed a beat, time to update current player beat
                beatScript.MissedBeat = true;
                UpdateCurrentEnemyBeat(false);
            }
        }
    }
}
