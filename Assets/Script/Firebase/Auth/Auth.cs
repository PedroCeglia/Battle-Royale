using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

public class Auth : MonoBehaviour
{
    // Create Singleton
    public static Auth Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            gameObject.SetActive(false);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private FirebaseAuth _authRef;

    // Get Auth Reference by Firebase Config
    // Called When Firebase starts
    public void SetAuthReference(FirebaseAuth authRef)
    {
        // Get Auth Ref
        _authRef = authRef;

        // Create Auth User Listener

    }
}
