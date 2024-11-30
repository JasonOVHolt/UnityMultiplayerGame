using Epic.OnlineServices.Auth;
using Epic.OnlineServices;
using PlayEveryWare.EpicOnlineServices.Samples.Network;
using PlayEveryWare.EpicOnlineServices.Samples;
using PlayEveryWare.EpicOnlineServices;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager;
using UnityEngine;


public class MyNetworkController : MonoBehaviour
{
    private EOSTransportManager transportManager = null;
    private bool isHost = false;
    private bool isClient = false;
    private LoginCredentialType loginType;
    private FriendData friendData;
    string usernameAsString, passwordAsString;
    [SerializeField] EOSTransport eosTransport;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (PlayerPrefs.GetString("firstTime") == "true")
            PlayerPrefs.SetString("firstTime", "false");


        if (PlayerPrefs.GetString("firstTime") == "")
            PlayerPrefs.SetString("firstTime", "true");

        if(PlayerPrefs.GetString("firstTime") == "true")
        {
            loginType = LoginCredentialType.AccountPortal;
        }
        else
        {
            loginType = LoginCredentialType.PersistentAuth;
        }


        Login();
        transportManager = EOSManager.Instance.GetOrCreateManager<EOSTransportManager>();
    }



    public void StartHostOnClick()
    {
        if (isHost)
        {
            Debug.LogError("UIP2PTransportMenu (StartHostOnClick): already hosting");
            return;
        }

        if (transportManager.StartHost())
        {
            isHost = true;
            SetJoinInfo(EOSManager.Instance.GetProductUserId());
        }
        else
        {
            Debug.LogError("UIP2PTransportMenu (StartHostOnClick): failed to start host");
        }
    }

    private void SetJoinInfo(ProductUserId serverUserId)
    {
        var joinData = new P2PTransportPresenceData()
        {
            SceneIdentifier = P2PTransportPresenceData.ValidIdentifier,
            ServerUserId = serverUserId.ToString()
        };

        string joinString = JsonUtility.ToJson(joinData);

        EOSSessionsManager.SetJoinInfo(joinString);
    }

    private void JoinGame(ProductUserId hostId)
    {
        if (hostId.IsValid())
        {
            NetworkSamplePlayer.SetNetworkHostId(hostId);
            if (transportManager.StartClient())
            {
                NetworkSamplePlayer.RegisterDisconnectCallback(OnDisconnect);
                isClient = true;
                SetJoinInfo(hostId);
            }
            else
            {
                Debug.LogError("UIP2PTransportMenu (JoinGame): failed to start client");
            }
        }
        else
        {
            Debug.LogError("UIP2PTransportMenu (JoinGame): invalid server user id");
        }
    }

    private void OnDisconnect(ulong _)
    {
        Debug.LogWarning("UIP2PTransportMenu (OnDisconnect): server disconnected");
        isClient = false;
        EOSSessionsManager.SetJoinInfo(null);
        NetworkSamplePlayer.UnregisterDisconnectCallback(OnDisconnect);
    }

    void Login()
    {

        if (loginType == LoginCredentialType.PersistentAuth)
        {
            Debug.Log("Persistent Login");
            EOSManager.Instance.StartPersistentLogin((Epic.OnlineServices.Auth.LoginCallbackInfo callbackInfo) =>
            {
                
                // In this state, it means one needs to login in again with the previous login type, or a new one, as the
                // tokens are invalid
                if (callbackInfo.ResultCode != Epic.OnlineServices.Result.Success)
                {
                    print("Failed to login with Persistent token [" + callbackInfo.ResultCode + "]");
                }
                else
                {
                    StartLoginWithLoginTypeAndTokenCallback(callbackInfo);
                }
            });
        }
        else
        {
            Debug.Log("Other Login");
            EOSManager.Instance.StartLoginWithLoginTypeAndToken(loginType,
                                                                        usernameAsString,
                                                                        passwordAsString,
                                                                        StartLoginWithLoginTypeAndTokenCallback);
            
        }
    }


    public void JoinPlayer()
    {
        var joinInfo = JsonUtility.FromJson<P2PTransportPresenceData>(friendData.Presence.JoinInfo);
        if (joinInfo.IsValid())
        {
            var hostId = ProductUserId.FromString(joinInfo.ServerUserId);
            if (hostId.IsValid())
            {
                JoinGame(hostId);
            }
        }
    }


    public void myJoinScript(string userID)
    {
        var hostID = ProductUserId.FromString(userID);
        if (hostID.IsValid())
        {
            JoinGame(hostID);
        }
    }


    public void StartLoginWithLoginTypeAndTokenCallback(LoginCallbackInfo loginCallbackInfo)
    {
        if (loginCallbackInfo.ResultCode == Epic.OnlineServices.Result.AuthMFARequired)
        {
            // collect MFA
            // do something to give the MFA to the SDK
            print("MFA Authentication not supported in sample. [" + loginCallbackInfo.ResultCode + "]");
        }
        else if (loginCallbackInfo.ResultCode == Result.AuthPinGrantCode)
        {
            ///TODO(mendsley): Handle pin-grant in a more reasonable way
            Debug.LogError("------------PIN GRANT------------");
            Debug.LogError("External account is not connected to an Epic Account. Use link below");
            Debug.LogError($"URL: {loginCallbackInfo.PinGrantInfo?.VerificationURI}");
            Debug.LogError($"CODE: {loginCallbackInfo.PinGrantInfo?.UserCode}");
            Debug.LogError("---------------------------------");
        }
        else if (loginCallbackInfo.ResultCode == Epic.OnlineServices.Result.Success)
        {
            StartConnectLoginWithEpicAccount(loginCallbackInfo.LocalUserId);
        }
        else if (loginCallbackInfo.ResultCode == Epic.OnlineServices.Result.InvalidUser)
        {
            print("Trying Auth link with external account: " + loginCallbackInfo.ContinuanceToken);
            EOSManager.Instance.AuthLinkExternalAccountWithContinuanceToken(loginCallbackInfo.ContinuanceToken,
#if UNITY_SWITCH
                                                                                LinkAccountFlags.NintendoNsaId,
#else
                                                                            LinkAccountFlags.NoFlags,
#endif
                                                                            (Epic.OnlineServices.Auth.LinkAccountCallbackInfo linkAccountCallbackInfo) =>
                                                                            {
                                                                                if (linkAccountCallbackInfo.ResultCode == Result.Success)
                                                                                {
                                                                                    StartConnectLoginWithEpicAccount(linkAccountCallbackInfo.LocalUserId);
                                                                                }
                                                                                else
                                                                                {
                                                                                    print("Error Doing AuthLink with continuance token in. [" + linkAccountCallbackInfo.ResultCode + "]");
                                                                                }
                                                                            });
        }

        else
        {
            print("Error logging in. [" + loginCallbackInfo.ResultCode + "]");
        }


    }



    private void StartConnectLoginWithEpicAccount(EpicAccountId LocalUserId)
    {
        EOSManager.Instance.StartConnectLoginWithEpicAccount(LocalUserId, (Epic.OnlineServices.Connect.LoginCallbackInfo connectLoginCallbackInfo) =>
        {
            if (connectLoginCallbackInfo.ResultCode == Result.Success)
            {
                print("Connect Login Successful. [" + connectLoginCallbackInfo.ResultCode + "]");
            }
            else if (connectLoginCallbackInfo.ResultCode == Result.InvalidUser)
            {
                // ask user if they want to connect; sample assumes they do
                EOSManager.Instance.CreateConnectUserWithContinuanceToken(connectLoginCallbackInfo.ContinuanceToken, (Epic.OnlineServices.Connect.CreateUserCallbackInfo createUserCallbackInfo) =>
                {
                    print("Creating new connect user");
                    EOSManager.Instance.StartConnectLoginWithEpicAccount(LocalUserId, (Epic.OnlineServices.Connect.LoginCallbackInfo retryConnectLoginCallbackInfo) =>
                    {

                    });
                });
            }
        });
    }


    public void LogOut()
    {
        PlayerPrefs.SetString("firstTime", "true");
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }



}
