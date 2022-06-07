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
    [SerializeField] private Text _loginErroMensageField;
    [Header("Singin Attributes")]
    [SerializeField] private GameObject _menuSingin;
    [SerializeField] private InputField _singinName;
    [SerializeField] private InputField _singinEmail;
    [SerializeField] private InputField _singinPassword;
    [SerializeField] private GameObject _singinErroArea;
    [SerializeField] private Text _singinErroMensageField;

    // Atributes
    private bool _hasSinginErroMensage;
    private string _singinErroMensage;
    private bool _hasLoginErroMensage;
    private string _loginErroMensage;

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
        _singinErroMensageField.text = "";
        _singinErroArea.SetActive(false);
    }
    // Close Login Erro Mensage
    public void CloseLoginErroArea()
    {
        _loginErroMensageField.text = "";
        _loginErroArea.SetActive(false);
    }

    // Singin Functions
    public void SinginUser()
    {
        if(_singinName.text.Length > 4)
        {
            if(_singinEmail.text.IndexOf("@") > 4 && _singinEmail.text.IndexOf(".com") > 7)
            {
                if(_singinPassword.text.Length > 6)
                {
                    // Create User

                    // Set Singin Field
                    ClearSinginFields();

                }
                else
                {
                    GetSinginMensageErro("The user password needs more than 6 letters");
                }
            }
            else
            {
                GetSinginMensageErro("Write a Valid Email ex: xxxx@gmail.com");
            }
        }
        else
        {
            GetSinginMensageErro("The user name needs more than 4 letters");
        }
    }
    // Get Singin ErroMensage
    private void GetSinginMensageErro(string erroMensage)
    {
        _hasSinginErroMensage = true;
        _singinErroMensage = erroMensage;
    }

    // Login Function
    public void LoginUser()
    {
        if (_loginEmail.text.IndexOf("@") > 4 && _loginEmail.text.IndexOf(".com") > 7)
        {
            if (_loginPassword.text.Length > 6)
            {
                // Log User

                // Set Login Field
                ClearLoginFields();
            }
            else
            {
                GetLoginMensageErro("The user password needs more than 6 letters");
            }
        }
        else
        {
            GetLoginMensageErro("Write a Valid Email ex: xxxx@gmail.com");
        }
    }
    // Get Login ErroMensage
    private void GetLoginMensageErro(string erroMensage)
    {
        _hasLoginErroMensage = true;
        _loginErroMensage = erroMensage;
    }
}
