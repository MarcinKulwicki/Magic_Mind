using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;


public class PlayFabAuth : MonoBehaviour
{
    public TMP_InputField user;
    public TMP_InputField password;
    public TMP_InputField email;
    public bool isAuthenticated = false;
    private LoginWithPlayFabRequest loginWithPlayFabRequest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LogIn(){
        loginWithPlayFabRequest = new LoginWithPlayFabRequest();
        loginWithPlayFabRequest.Username = user.text;
        loginWithPlayFabRequest.Password = password.text;

        PlayFabClientAPI.LoginWithPlayFab(loginWithPlayFabRequest, result => {
            //If acc found
            isAuthenticated = true;
            Debug.Log("You're logged in");
        }, error => {
            //If not found
            isAuthenticated = false;
            Debug.Log(error.ErrorMessage);

        },null);
    }
}
