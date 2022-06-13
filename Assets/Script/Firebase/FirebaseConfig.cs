using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;

public class FirebaseConfig : MonoBehaviour
{
    // Atributes
    private bool _verifyItIsOk;
    private FirebaseAuth _firebaseAuth;
    private FirebaseDatabase _firebaseDatabase;
    public bool _isConnect;

    // Create Singleton
    public static FirebaseConfig Instance { get; private set; }
    private void Awake()
    {
        // Singleton Config
        if(Instance != null && Instance != this)
        {
            gameObject.SetActive(false);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);


        // Initialize Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                // Caso não haja erro  Inicialize o Firebase
                Debug.Log("Firebase Available");
                _verifyItIsOk = true;

            }
            else
            {
                _verifyItIsOk = false;
                Debug.LogError("Não foi possivel resolver todas as dependencias: " + task.Result);
                _isConnect = false;
            }
        });
    }

    // Verify if Firebase was Initialized
    void Update()
    {
        if (_verifyItIsOk)
        {
            _verifyItIsOk = false;
            InitializeFirebase();
        }   
    }

    private void InitializeFirebase()
    {
        // Get API´s
        _firebaseAuth = FirebaseAuth.DefaultInstance;
        _firebaseDatabase = FirebaseDatabase.DefaultInstance;

        // Pass to API´s Class
        Auth.Instance.SetAuthReference(_firebaseAuth);
        RealtimeDatabase.Instance.GetDatabaseInstance(_firebaseDatabase);

        // Is Connect
        _isConnect = true;
    }
}
