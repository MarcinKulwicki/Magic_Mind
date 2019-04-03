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
    public TextMeshProUGUI message;
    public bool isAuthenticated = false;
    private LoginWithPlayFabRequest loginWithPlayFabRequest;
    // Start is called before the first frame update
    void Start()
    {
        email.gameObject.SetActive(false);
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
            message.text = "Welcome "+ user.text+" Connecting...";
            isAuthenticated = true;
            Debug.Log("You're logged in");
        }, error => {
            //If not found
            isAuthenticated = false;
            email.gameObject.SetActive(true);

            message.text = "Failed to log in your account! ["+error.ErrorMessage+"]";  

        },null);
    }

    public void Register(){
        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest();
        request.Email = email.text;
        request.Username = user.text;
        request.Password = password.text;

        PlayFabClientAPI.RegisterPlayFabUser(request, result => {

            message.text = "Your account has been created!";
        }, error =>{
            email.gameObject.SetActive(true);
            message.text = "Please enter your email";    
        });
    }
}
