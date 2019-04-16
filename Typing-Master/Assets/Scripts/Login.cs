using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using System.IO;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {

    public GameObject username;
    public GameObject password;
    private string Username;
    private string Password;
    private string[] Lines;
    private string[] Line;
    private string DecryptedPass;

    // Use this for initialization
    void Start () {
		
	}
	
    public void LoginButton()
    {
        
        bool UN = false;
        bool PS = false;
        
        if (Username != "")
        {
            if (File.Exists("Files/" + Username + ".txt"))
            {
                UN = true;
                Line = File.ReadAllLines("Files/" + Username + ".txt");
                Lines = Line[0].Split('$');
            }
            else
            {
                Debug.LogWarning("Username Is Invaild");
            }
        }
        else
        {
            Debug.LogWarning("Username field Empty");
        }
        if(Password != "" && Username!="")
        {
            if (File.Exists("Files/" + Username + ".txt"))
            {
                int i = 1;
                foreach (char c in Lines[2])
                {

                    i++;
                    char Decrypted = (char)(c / i);
                    DecryptedPass += Decrypted.ToString();
                }
                if(Password.Equals(DecryptedPass))
                {
                    PS = true;
                }
                else
                {
                    Debug.LogWarning("Password Is Invaild");
                }
            }
            else
            {
                Debug.LogWarning("Password Is Invaild");
            }
        }
        else
        {
            Debug.LogWarning("Password field Empty");
        }

        if(PS == true && UN == true)
        {
             username.GetComponent<InputField>().text = "";
             password.GetComponent<InputField>().text = "";
            print("Login Successfull");
            SceneManager.LoadScene("MainWindow");
        }
        

    }
    

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            
            LoginButton();
            
        }
        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;



    }
}
