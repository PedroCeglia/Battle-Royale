using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
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

    // Auth Attributes
    public FirebaseUser _user { get; private set; }
    private FirebaseAuth _authRef;
    private bool _isLog;

    // Get Auth Reference by Firebase Config
    // Called When Firebase starts
    public void SetAuthReference(FirebaseAuth authRef)
    {
        // Get Auth Ref
        _authRef = authRef;

        // Create Auth User Listener

    }

    public delegate void GetErroMensage(string erroMensage);

    // User Login Functions
    public void LoginUser(string email, string password, GetErroMensage SetErroMensage)
    {
        object[] parms = new object[3] { email, password, SetErroMensage };
        StartCoroutine("Login", parms);
    }
    IEnumerator Login(object[] parms)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = _authRef.SignInWithEmailAndPasswordAsync((string)parms[0], (string)parms[1]);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            //Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            GetErroMensage GetErroLoginMensage = (GetErroMensage)parms[2];
            GetErroLoginMensage(message);
        }
        else
        {
            //User is now logged in
            //Now get the result
            _user = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", _user.DisplayName, _user.Email);

            yield return new WaitForSeconds(2);
        }
    }

    // Create User Functions
    public void CreateNewUser(string username, string email, string password, GetErroMensage SetErroMensage)
    {
        object[] parms = new object[4] { username, email, password, SetErroMensage };
        StartCoroutine("CreateUser", parms);
    }
    IEnumerator CreateUser(object[] parms)
    {

        string username = (string)parms[0];
        string email = (string)parms[1];
        string password = (string)parms[2];
        GetErroMensage GetErroLoginMensage = (GetErroMensage)parms[3];
        if (username != null && email != null && password != null && GetErroLoginMensage != null)
        {
            if (username == "")
            {
                //If the username field is blank show a warning
                GetErroLoginMensage("Missing Username");
            }
            else if (email.IndexOf("@") < 4 && email.IndexOf(".com") < 6)
            {
                //If the password does not match show a warning
                GetErroLoginMensage("The Password needs more than 5 words");
            }
            else
            {
                //Call the Firebase auth signin function passing the email and password
                var RegisterTask = _authRef.CreateUserWithEmailAndPasswordAsync(email, password);
                //Wait until the task completes
                yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

                if (RegisterTask.Exception != null)
                {
                    //If there are errors handle them
                    //Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                    FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                    AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                    string message = "Register Failed!";
                    switch (errorCode)
                    {
                        case AuthError.MissingEmail:
                            message = "Missing Email";
                            break;
                        case AuthError.MissingPassword:
                            message = "Missing Password";
                            break;
                        case AuthError.WeakPassword:
                            message = "Weak Password";
                            break;
                        case AuthError.EmailAlreadyInUse:
                            message = "Email Already In Use";
                            break;
                    }
                    GetErroLoginMensage(message);
                }
                else
                {
                    //User has now been created
                    //Now get the result
                    _user = RegisterTask.Result;

                    if (_user != null)
                    {

                        // Create User In Database
                        //UserModel userDatabase = new UserModel(username, email, _user.UserId);
                        //_databaseAPI.WriteNewPlayerDatabase(userDatabase);

                        //Create a user profile and set the username
                        UserProfile profile = new UserProfile { DisplayName = username };

                        //Call the Firebase auth update user profile function passing the profile with the username
                        var ProfileTask = _user.UpdateUserProfileAsync(profile);
                        //Wait until the task completes
                        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                        if (ProfileTask.Exception != null)
                        {
                            //If there are errors handle them
                            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        }
                        else
                        {
                            //Username is now set
                        }
                    }
                }
            }
        }

    }
}
