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

    // Create user In Database
    public void CreateUserInDatabase(UserModel userModel)
    {
        if(_databaseRef != null)
        {
            // Trasform the User Object in Json
            string userJson = JsonUtility.ToJson(userModel);

            // Create User In Database
            _userRef.Child(userModel.id).SetRawJsonValueAsync(userJson);
        }
    }

    // Set User Name In Database
    public void SetUserNameInDatabase(string id, string username)
    {
        if(_databaseRef != null && id != null && username != null)
        {
            _userRef.Child(id).Child("username").SetValueAsync(username);
        }
    }
    // Set Email Name In Database
    public void SetUserEmailInDatabase(string id, string email)
    {
        if (_databaseRef != null && id != null && email != null)
        {
            _userRef.Child(id).Child("email").SetValueAsync(email);
        }
    }

    // Get User By Id
    public delegate void DelegateGetUserById(UserModel userModel);
    public void GetUserById(string id, DelegateGetUserById setUser)
    {
        _userRef.Child(id)
            .GetValueAsync()
                .ContinueWith((task) => 
                {
                    if (task.IsCompleted)
                    {
                        // Get User Snapshot
                        DataSnapshot snapshot = task.Result;

                        // Transform Snapshot in Json
                        string userJson = snapshot.GetRawJsonValue();

                        // Transform Json in Object
                        UserModel userModel = JsonUtility.FromJson<UserModel>(userJson);
                        
                        // Set User Function
                        setUser(userModel);
                    }
                });
    }
}
