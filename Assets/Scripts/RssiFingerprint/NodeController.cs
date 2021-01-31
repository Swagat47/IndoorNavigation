﻿using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using Firebase.Firestore;
using Firebase.Extensions;


[Serializable]
public class GridData
{
    public string mac;
    public int strength;
    public Vector2 pos;
    public string label;

    public GridData(string mac, int strength, Vector2 pos)
    {
        this.mac = mac;
        this.strength = strength;
        this.pos = pos;
        this.label = string.Empty;
    }
}
[Serializable]
public class GridDataCollection
{
    public List<GridData> nodes = new List<GridData>();
}

public class NodeController : MonoBehaviour
{

    const string WRITE_DATABASE = "http://specnitharnav.epizy.com/WriteMap.php";
    const string READ_DATABASE = "http://specnitharnav.epizy.com/ReadMap.php";
    const string MAP_NAME = "Room1";

    public Text debugText;

    private GridDataCollection gridDataCollection = new GridDataCollection();

    //
    #region Node Data

    public static string mac = "";
    public static int rssint = 0;
    
    //public static Dictionary<string, object> pos = new Dictionary<string, object>
       // {
         //   {"x",0f },
       //     {"y",0f },
     //     //  {"z",0f }
   //     };
    #endregion

    public void Start()
    {
    }

    int i = 0;

    public void AddNode(string macAddress, int rssi, Vector2 position)
    {
        gridDataCollection.nodes.Add(new GridData(macAddress, rssi, position));
        string node = "node";
        if (i < 10)
        {
            node += i.ToString("00");
        }
        else
        {
            node += i.ToString(); 
        }
        FirebaseFirestore db2 = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef2 = db2.Collection("Coordinates").Document(node);
        //Dictionary<string, object> posStart = new Dictionary<string, object>
       // {
      //      {"x",position.x },
      //      {"y",position.y }
      //  };

        Dictionary<string, object> nodestart = new Dictionary<string, object>
        {
             { "mac", macAddress },
             { "rssi", rssi },
            {"Pos.x",position.x },
            {"Pos.y",position.y }
            // {"Pos", new Dictionary<string,object>{ {"x",position.x},{"y",position.y } } }
        };
        docRef2.SetAsync(nodestart).ContinueWithOnMainThread(task => {
            Debug.Log("Added data to the alovelace document in the users collection.");
        });

        i++;
    }

    public List<GridData> GetNodes()
    {
        return gridDataCollection.nodes;
    }

    public void SaveMap()
    {
        //serialize data
        string jsonDataString = JsonUtility.ToJson(gridDataCollection, true);

        //write to database
        StartCoroutine(WriteToDatabase(jsonDataString));
    }

    IEnumerator WriteToDatabase(string json)
    {
        debugText.text = "";
        WWWForm form = new WWWForm();
        form.AddField("mapInfo", json);
        form.AddField("mapName", MAP_NAME);
        UnityWebRequest www = UnityWebRequest.Post(WRITE_DATABASE, form);
        yield return www;
        Debug.Log(www.downloadHandler.text);
        debugText.text = gridDataCollection.nodes.Count + " nodes uploaded.\n Result: " + www.downloadHandler.text + ":)";
    }

    public void LoadMap()
    {
        //load all json from database
        StartCoroutine(ReadFromDatabase());
    }

    IEnumerator ReadFromDatabase()
    {
        WWWForm form = new WWWForm();
        form.AddField("mapName", MAP_NAME);
        UnityWebRequest www = UnityWebRequest.Post(READ_DATABASE, form);
        yield return www;
        string loadedJsonDataString = www.downloadHandler.text;
        print(www.downloadHandler.text);
        //deserialize json
        gridDataCollection = JsonUtility.FromJson<GridDataCollection>(loadedJsonDataString);
        //display map
        GetComponent<ReadMap>().DisplayMap(gridDataCollection.nodes);
    }
}