using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserModel : MonoBehaviour
{
    public string username;
    public string email;
    public string id;

    public UserModel(string username, string email, string id)
    {
        this.username = username;
        this.email = email;
        this.id = id;
    }
}
