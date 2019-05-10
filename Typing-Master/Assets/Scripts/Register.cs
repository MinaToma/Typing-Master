using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using System.IO;
using UnityEditor;

public class Register : MonoBehaviour {

    public GameObject username;
    public GameObject email;
    public GameObject password;
    public GameObject confPassword;
    private string Username;
    private string Password;
    private string Email;
    private string ConfPassword;
    private string form;

    // Use this for initialization
    void Start () {
       
    }
	
    public void RegisterButton()
    {
        bool EM = false;
        bool UN = false;
        bool PS = false;
        bool CPS = false;
        if(Username!="")
        {
            if(!File.Exists("Files/"+Username+".txt"))
            {
                UN = true;
            }
            else
            {
                Debug.LogWarning("Username Taken");
            }
        }
        else
        {
            Debug.LogWarning("Username field Empty");
        }
        if(Email!="")
        {
            
                if(Email.Contains("@"))
                {
                    if(Email.Contains("."))
                    {
                        EM = true;
                    }
                    else
                    {
                        Debug.LogWarning("Email is Incorrect");
                    }
                }
                else
                {
                    Debug.LogWarning("Email is Incorrect");
                }
            
        }
        else
        {
            Debug.LogWarning("Email field Empty");
        }
        if(Password!="")
        {
            if(Password.Length>5)
            {
                PS = true;
            }
            else
            {
                Debug.LogWarning("Password must be atleast 6 charchters");
            }
        }
        else
        {
            Debug.LogWarning("Password field Empty");
        }
        if(ConfPassword != "")
        {
            if(Password == ConfPassword )
            {
                CPS = true;
            }
            else
            {
                Debug.LogWarning("Passwords Don't Match");
            }
        }
        else
        {
            Debug.LogWarning("Confirm Password field Empty");
        }
        if(PS == true && UN == true && EM == true && CPS == true)
        {
            bool Clear = true;
            int i = 1;
            foreach(char c in Password)
            {
                if(Clear)
                {
                    Password = "";
                    Clear = false;

                }
                i++;
                char Encrypted = (char)(c * i);
                Password += Encrypted.ToString();
            }
            form = (Username + "$" + Email + "$" + Password);
            
            FileStream fs = new FileStream("Files/" + Username + ".txt", FileMode.Create);
            StreamWriter Sw = new StreamWriter(fs);
            Sw.WriteLine(form);
            Sw.Close();
            fs.Close();
            fs = new FileStream("Files/playerNames.txt", FileMode.Append);
            Sw = new StreamWriter(fs);
            Sw.WriteLine(Username);
            Sw.Close();
            fs.Close();

            username.GetComponent<InputField>().text = "";
            email.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
            confPassword.GetComponent<InputField>().text = "";

          //  EditorUtility.DisplayDialog("Successful", "Registered", "OK");
            print("Registration complete");
        }
        else
        {
         //   EditorUtility.DisplayDialog("Failed", "Check your data", "Cancel");
        }
    }
    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                email.GetComponent<InputField>().Select();
            }
            else if (email.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
            else if (password.GetComponent<InputField>().isFocused)
            {
                confPassword.GetComponent<InputField>().Select();
            }

        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            RegisterButton();   
        }

        Username = username.GetComponent<InputField>().text;
        Email = email.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
        ConfPassword = confPassword.GetComponent<InputField>().text;
    }
}
