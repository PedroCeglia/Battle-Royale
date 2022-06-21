using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public void SetMenuActive(bool cond)
    {
        Debug.Log("Change");
        gameObject.SetActive(cond);
    }

    public void returnToMenuLobby()
    {   
        Destroy(GameController.Instance.gameObject);
        Destroy(FirebaseConfig.Instance.gameObject);
        GestorDeRede.Instance.BackToIntroScene();
    }
}
