using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogMenu : MonoBehaviour
{
    // Get Widgets
    [Header("Login Attributes")]
    [SerializeField] private GameObject _menuLogin;
    [SerializeField] private InputField _loginEmail;
    [SerializeField] private InputField _loginPassword;
    [SerializeField] private GameObject _loginErroArea;
    [SerializeField] private Text _loginErroMensage;
    [Header("Singin Attributes")]
    [SerializeField] private GameObject _menuSingin;
    [SerializeField] private InputField _singinName;
    [SerializeField] private InputField _singinEmail;
    [SerializeField] private InputField _singinPassword;
    [SerializeField] private GameObject _singinErroArea;
    [SerializeField] private Text _singinErroMensage;


    // Set Menu Log
    public void SetMenulog(string menu)
    {
        if(menu == "login")
        {
            _menuSingin.SetActive(false);
            _menuLogin.SetActive(true);
        }
        else if(menu == "singin")
        {
            _menuLogin.SetActive(false);
            _menuSingin.SetActive(true);
        }
    }

    // Clear Singin Fields 
    private void ClearSinginFields()
    {
        _singinName.text = "";
        _singinEmail.text = "";
        _singinPassword.text = "";
    }
    // Clear Login Fields 
    private void ClearLoginFields()
    {
        _loginEmail.text = "";
        _loginPassword.text = "";
    }

    // Close Singin Erro Mensage
    public void CloseSinginErroArea()
    {
        _singinErroMensage.text = "";
        _singinErroArea.SetActive(false);
    }
    // Close Login Erro Mensage
    public void CloseLoginErroArea()
    {
        _loginErroMensage.text = "";
        _loginErroArea.SetActive(false);
    }
}
