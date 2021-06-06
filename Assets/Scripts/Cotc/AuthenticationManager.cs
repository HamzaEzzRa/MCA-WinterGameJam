using System;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class AuthenticationManager : MonoBehaviour
{
    public static AuthenticationManager Instance { get; private set; }

    [SerializeField] private GameObject logView = default;
    [SerializeField] private TMPro.TMP_Text viewText = default;
    [SerializeField] private TMPro.TMP_Text buttonText = default;

    [SerializeField] private GameObject connectionWaitPrefab = default;
    
    private GameObject connectionWait;

    private bool isLogShown;
    private int logLineCount;
    private string infoText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        viewText = logView.gameObject.GetComponentInChildren<TMPro.TMP_Text>();
    }

    private void Start()
    {
        // Resume Cotc Session
        string dataPath = Application.persistentDataPath + "/Data/PlayerData.gd";
        GameData data = (GameData)SerializationManager.Load(dataPath);
        if (data != null)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                    FB.ActivateApp();
                else
                    AddToLog("Failed To Initialize The Facebook SDK");
            }, OnHideUnity);

            GameData.Instance = data;
            string gamerId = GameData.Instance.CotcId;
            string gamerSecret = GameData.Instance.CotcSecret;

            CotcManager.Instance.ResumeSession(gamerId, gamerSecret);
        }
        else
        {
            if (Utility.Platform == RuntimePlatform.Android || Utility.Platform == RuntimePlatform.IPhonePlayer)
            {
                // Sign in silently with Facebook
                if (!FB.IsInitialized)
                    FB.Init(InitCallback, OnHideUnity);
                else
                {
                    FB.ActivateApp();
                    try
                    {
                        FacebookExpressLogin();
                    }
                    catch (Exception facebookException)
                    {
                        AddToLog("Failed To Sign In Silently With Facebook. " + facebookException);

                        // Sign in anonymously witch Cotc
                        CotcManager.Instance.LoginAnonymously();

                        if (connectionWait != null)
                        {
                            Destroy(connectionWait);
                            connectionWait = null;
                        }
                    }
                }
            }
            else
            {
                // Sign in anonymously witch Cotc
                CotcManager.Instance.LoginAnonymously();

                if (connectionWait != null)
                {
                    Destroy(connectionWait);
                    connectionWait = null;
                }
            }
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            try
            {
                FacebookExpressLogin();
            }
            catch (Exception facebookException)
            {
                AddToLog("Failed To Sign In Silently With Facebook. " + facebookException);

                // Sign in anonymously witch Cotc
                CotcManager.Instance.LoginAnonymously();

                if (connectionWait != null)
                {
                    Destroy(connectionWait);
                    connectionWait = null;
                }
            }
        }

        else
            AddToLog("Failed To Initialize The Facebook SDK");
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void SignInWithFacebook() { if (Utility.Platform == RuntimePlatform.Android || Utility.Platform == RuntimePlatform.IPhonePlayer) OnFacebookSignIn(); }

    public void SignOutFromFacebook() { OnFacebookSignOut(); }

    private void OnFacebookSignIn()
    {
        List<string> permissions = new List<string>() { "public_profile", "email", "user_friends" };
        try
        {
            FB.LogInWithReadPermissions(permissions, OnFacebookSignInCallback);
        }
        catch (Exception exception)
        {
            AddToLog("Failed To Sign In With Facebook. " + exception);
        }
    }

    private void OnFacebookSignOut()
    {
        try
        {
            FB.LogOut();
        }
        catch (Exception exception)
        {
            AddToLog("Failed To Sign Out With Facebook. " + exception);
        }
    }

    private void OnFacebookSignInCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            AccessToken accessToken = AccessToken.CurrentAccessToken;
            CotcManager.Instance.NetworkLogin(CotcSdk.LoginNetwork.Facebook, accessToken.UserId, accessToken.TokenString);
        }

        if (connectionWait != null)
        {
            Destroy(connectionWait);
            connectionWait = null;
        }
    }

    private void FacebookExpressLogin()
    {
        if (connectionWait == null)
            connectionWait = Instantiate(connectionWaitPrefab, null);
        FB.Android.RetrieveLoginStatus(ExpressLoginCallback);
    }

    private void ExpressLoginCallback(ILoginStatusResult result)
    {
        if (!string.IsNullOrEmpty(result.Error))
            AddToLog("Error: " + result.Error);
        else if (result.Failed)
            AddToLog("Failed To Sign In Silently With Facebook.");
        else
        {
            AccessToken accessToken = result.AccessToken;
            CotcManager.Instance.NetworkLogin(CotcSdk.LoginNetwork.Facebook, accessToken.UserId, accessToken.TokenString);
        }

        if (connectionWait != null)
        {
            Destroy(connectionWait);
            connectionWait = null;
        }
    }

    public void AddToLog(string str)
    {
        logLineCount++;
        infoText += "\n" + logLineCount + ". " + (str.Length > 500 ? (str.Substring(0, 500) + "...") : str);
        viewText.SetText("");
        viewText.SetText(infoText);
    }

    public void ToggleLog()
    {
        if (!isLogShown)
        {
            logView.gameObject.SetActive(true);
            viewText.SetText(infoText);
            buttonText.SetText("Hide Log");
            isLogShown = true;
        }
        else
        {
            logView.gameObject.SetActive(false);
            buttonText.SetText("Show Log");
            isLogShown = false;
        }
    }

    public void ClearLog()
    {
        logLineCount = 0;
        infoText = "";
        viewText.SetText(infoText);
    }
}
