using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

public class Fbase : MonoBehaviour
{
    public GameObject DirectionPrefab;
    public GameObject Arcam;

    // Start is called before the first frame update
    void Start()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document("alovelace");
        Dictionary<string, object> user = new Dictionary<string, object>
{
        { "First", "Ada" },
        { "Last", "Lovelace" },
        { "Born", 1815 },
};
        docRef.SetAsync(user).ContinueWithOnMainThread(task => {
            Debug.Log("Added data to the alovelace document in the users collection.");
        });
    }

    public void AddData()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document("aturing");
        Dictionary<string, object> user = new Dictionary<string, object>
{
        { "First", "Alan" },
        { "Middle", "Mathison" },
        { "Last", "Turing" },
        { "Born", 1912 }
};
        docRef.SetAsync(user).ContinueWithOnMainThread(task => {
            Debug.Log("Added data to the aturing document in the users collection.");
        });

    }


    public void ReadData()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        CollectionReference usersRef2 = db.Collection("Coordinates");

        usersRef2.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
        QuerySnapshot snapshot = task.Result;
        foreach (DocumentSnapshot document in snapshot.Documents)
        {
            Debug.Log(string.Format("nodestart: {0}", document.Id));
            Dictionary<string, object> documentDictionary = document.ToDictionary();
            Debug.Log(string.Format("mac: {0}", documentDictionary["mac"]));
            Debug.Log(string.Format("rssi: {0}", documentDictionary["rssi"]));
            // Debug.Log(string.Format("Pos: {0}", documentDictionary["Pos"]));
            Debug.Log(string.Format("Pos.x: {0}", documentDictionary["Pos.x"]));
            Debug.Log(string.Format("Pos.y: {0}", documentDictionary["Pos.y"]));
            Debug.Log("--------------------- ");
            //Assign positions value
            // float xCoord = float.Parse(string.Format("Pos.x: {0}", documentDictionary["Pos.x"]));
            // float yCoord = float.Parse(string.Format("Pos.x: {0}", documentDictionary["Pos.x"]));

            Vector3 putPlace = new Vector3(float.Parse( documentDictionary["Pos.x"].ToString()),
                                            Arcam.transform.position.y,
                                            float.Parse(documentDictionary["Pos.y"].ToString()));
               // if(currentRSSIvalue == CalledRSSivalue || CurrentMacvalue == calledMacvalue)
                Instantiate(DirectionPrefab,putPlace, Quaternion.identity);
            }
            Debug.Log("Read all data from the users collection.");
        });

    }

}
/*
        CollectionReference usersRef = db.Collection("users");
        usersRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot snapshot = task.Result;
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Debug.Log(string.Format("User: {0}", document.Id));
                Dictionary<string, object> documentDictionary = document.ToDictionary();
                Debug.Log(string.Format("First: {0}", documentDictionary["First"]));
                if (documentDictionary.ContainsKey("Middle"))
                {
                    Debug.Log(string.Format("Middle: {0}", documentDictionary["Middle"]));
                }

                Debug.Log(string.Format("Last: {0}", documentDictionary["Last"]));
                Debug.Log(string.Format("Born: {0}", documentDictionary["Born"]));
            }
            Debug.Log("Read all data from the users collection.");
        });
        */

// string.Format("Pos: {0}", documentDictionary["Pos"])
//foreach (DocumentSnapshot doc in snapshot.Documents)
// {
//  Debug.Log(string.Format("Pos : {0}", document.Id));
//  Dictionary<string, object> docPos = document.ToDictionary();
//  Debug.Log(string.Format("Pos.x : {0}", docPos["x"]));
//  Debug.Log(string.Format("Pos.y : {0}", docPos["y"]));
// }


/*Dictionary<string, object> docPosition = document.ToDictionary();
Debug.Log(string.Format("Pos.x : {0}", docPosition["x"]));
Debug.Log(string.Format("Pos.y : {0}", docPosition["y"]));
*/