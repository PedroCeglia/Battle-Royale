using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

public class RealtimeDatabase : MonoBehaviour
{
    // Create Singleton
    public static RealtimeDatabase Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            gameObject.SetActive(false);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }

    // Atributes
    private FirebaseDatabase _database;
    private DatabaseReference _databaseRef;
    private DatabaseReference _userRef;

    // Get Firebase Reference by Firebase Config
    // Called When Firebase starts
    public void GetDatabaseInstance(FirebaseDatabase firebase)
    {
        // Get Firebase Instance
        _database = firebase;

        // Create Firebase Refs
        _databaseRef = firebase.RootReference;
        _userRef = _databaseRef.Child("user");
    }
}
