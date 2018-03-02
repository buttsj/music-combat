using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class BeatController : MonoBehaviour {

    public GameObject enemyBar;
    public GameObject playerBar;

    public GameObject enemyBeat;
    private GameObject[] enemyBeatList;

    public GameObject playerBeat;
    private GameObject[] playerBeatList;

    public GameObject beatZone;
    private Color hitColor = Color.yellow;
    private Color defaultColor;

    public TextAsset csvFile;

    private string[] beatTimestamps; // every timestamp of a beat in a song
    private List<string> deliveryBeats; // beats to deploy a "Beat" on screen

    public AudioSource slash;

    private int cursor = 0;
    private int angerBuildUp = 0;
    
    void Start()
    {

        beatTimestamps = csvFile.text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        CreateBeats();

	}
	
	void Update()
    {
        if (cursor > deliveryBeats.Count)
        {
            cursor = 0;
            CreateBeats();
        }

		if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckForBeat();
        }

        if (float.Parse(deliveryBeats[cursor]) - Time.timeSinceLevelLoad <= 3.0f)
        {
            if (angerBuildUp >= 10)
            {
                angerBuildUp = 0;
                enemyBeatList[cursor].GetComponent<BeatObject>().IsLaunched = true;
                enemyBeatList[cursor].GetComponent<BeatObject>().IsEvil = true;
            }
            else if (UnityEngine.Random.Range(0, 5) == 1)
            {
                enemyBeatList[cursor].GetComponent<BeatObject>().IsLaunched = true;
                enemyBeatList[cursor].GetComponent<BeatObject>().IsEvil = true;
            }
            else if (UnityEngine.Random.Range(0, 5) == 3)
            {
                enemyBeatList[cursor].GetComponent<BeatObject>().IsLaunched = true;
                enemyBeatList[cursor].GetComponent<BeatObject>().IsEvil = true;
                enemyBeatList[cursor].GetComponent<BeatObject>().IsLaunched = true;
                enemyBeatList[cursor].GetComponent<BeatObject>().IsEvil = true;
            }
            else
            {
                playerBeatList[cursor].GetComponent<BeatObject>().IsLaunched = true;
            }
            cursor++;
            angerBuildUp++;
        }
	}

    void CreateBeats()
    {
        deliveryBeats = new List<string>();

        for (int i = 5; i < beatTimestamps.Length; i = i + 5)
        {
            deliveryBeats.Add(beatTimestamps[i]);
        }

        enemyBeatList = new GameObject[deliveryBeats.Count];
        playerBeatList = new GameObject[deliveryBeats.Count];

        for (int k = 0; k < deliveryBeats.Count; k++)
        {
            playerBeatList[k] = Instantiate(playerBeat, playerBeat.transform.position, playerBeat.transform.rotation, playerBar.transform);
            enemyBeatList[k] = Instantiate(enemyBeat, enemyBeat.transform.position, enemyBeat.transform.rotation, enemyBar.transform);
        }
    }

    void CheckForBeat()
    {
        slash.Play();
        beatZone.GetComponent<Image>().color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
