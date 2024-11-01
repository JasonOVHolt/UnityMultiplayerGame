using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Epic.OnlineServices;
using PlayEveryWare.EpicOnlineServices.Samples.Network;
using PlayEveryWare.EpicOnlineServices.Samples;
using PlayEveryWare.EpicOnlineServices;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager;
using Unity.Netcode;


public class TitleScreenManager : MonoBehaviour
{
    bool showingHost = false, showingJoin = false, showingSettings = false, showingCustomize = false;
    [SerializeField] GameObject hostSettings, joinSettings, settings, customizeSettings;
    [SerializeField] Image mapImage, hatImage, maskImage, colorImage;
    [SerializeField] List<Sprite> mapImages, hatImages, maskImages, colorImages;
    [SerializeField] List<Color> colors;
    int currentMap;
    [SerializeField] GameObject player, hatObject, maskObject;
    [SerializeField] List<GameObject> hats, masks;
    [SerializeField] Material playerMaterial;

    int currentHat, currentMask, currentColor;
    [SerializeField] float rotSpeed;



    // Start is called before the first frame update
    void Awake()
    {
        CheckStats();
        UnlockCustomizedItems();
        hostSettings.SetActive(false);
        joinSettings.SetActive(false);
        settings.SetActive(false);
        customizeSettings.SetActive(false);
        player.SetActive(false);

        hatImage.sprite = hatImages[currentHat];
        maskImage.sprite = maskImages[currentMask];
        colorImage.sprite = colorImages[currentColor];

        currentHat = PlayerPrefs.GetInt("currentHat");
        currentMask = PlayerPrefs.GetInt("currentMask");
        currentColor = PlayerPrefs.GetInt("currentColor");
        transportManager = EOSManager.Instance.GetOrCreateManager<EOSTransportManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (showingCustomize)
        {
            player.transform.Rotate(new Vector3(0, (rotSpeed*Time.deltaTime), 0));
        }
    }

    public void toggleHostSettings()
    {
        if (!showingHost)
        {
            showingHost = true;
            hostSettings.SetActive(true);
        }
        else
        {
            showingHost = false;
            hostSettings.SetActive(false);
        }
    }

    public void toggleJoinSettings() {
        if (!showingJoin)
        {
            showingJoin = true;
            joinSettings.SetActive(true);
        }
        else
        {
            showingJoin = false;
            joinSettings.SetActive(false);
        }


    }

    public void toggleSettings() {
        if (!showingSettings)
        {
            showingSettings = true;
            settings.SetActive(true);
        }
        else
        {
            showingSettings = false;
            settings.SetActive(false);
        }

    }

    public void toggleCustomizeSettings()
    {
        if (!showingCustomize)
        {
            showingCustomize = true;
            customizeSettings.SetActive(true);
            player.SetActive(true);
            hats[currentHat].SetActive(true);
            masks[currentMask].SetActive(true);
        }
        else
        {
            showingCustomize = false;
            customizeSettings.SetActive(false);
            player.SetActive(false);
            PlayerPrefs.SetInt("currentHat", currentHat);
            PlayerPrefs.SetInt("currentMask", currentMask);
            PlayerPrefs.SetInt("currentColor", currentColor);
            //////////////////////////////////////////////////////////////////////////////////////SAVE DATA??????
        }


    }

    public void LeftMap()
    {
        currentMap -= 1;
        if (currentMap < 0)
            currentMap = mapImages.Count-1;
        mapImage.sprite = mapImages[currentMap];
    }

    public void RightMap()
    {
        currentMap += 1;
        if (currentMap >= mapImages.Count)
            currentMap = 0;
        mapImage.sprite = mapImages[currentMap];
    }

    public void Quit() {
    Application.Quit();    
    }

    public void HostGame()
    {
        string sceneName = (currentMap + 1).ToString();
        SceneManager.LoadScene(sceneName);
        StartHostOnClick();

        FindObjectOfType<EOSTransport>().Initialize(FindObjectOfType<NetworkManager>());
    }

    void CheckStats()
    {
        if (PlayerPrefs.GetInt("gamesPlayed") != PlayerPrefs.GetInt("gamesWon") + PlayerPrefs.GetInt("gamesLossed"))
        {
            PlayerPrefs.SetInt("gamesLossed", (PlayerPrefs.GetInt("gamesPlayed") - PlayerPrefs.GetInt("gamesWon")));
        }
    }

    void UnlockCustomizedItems()
    {
        int gP, gW, gL;
        int cH, cM, cC;

        gP = PlayerPrefs.GetInt("gamesPlayed");
        gW = PlayerPrefs.GetInt("gamesWon");
        gL = PlayerPrefs.GetInt("gamesLossed");

        cH = PlayerPrefs.GetInt("currentHat");
        cM = PlayerPrefs.GetInt("currentMask");
        cC = PlayerPrefs.GetInt("currentColor");

        if (gP <= 5)    //Unlock Items based on games played
        {
            //Unlock item
        } else if (gP <= 10)
        {
            //Unlock item 2
        }

        if(gW <= 5)     //Unlock Items based on games won
        {

        } else if (gW <= 10)
        {

        }

        if (gL <= 5)    //Unlock Items based on games lossed
        {

        }
        else if (gL <= 10)
        {

        }


        currentHat = cH;
        currentMask = cM;
        currentColor = cC;

    }

    public void LeftHat()
    {
        hats[currentHat].SetActive(false);
        currentHat -= 1;
        if (currentHat < 0)
            currentHat = hatImages.Count - 1;
        hatImage.sprite = hatImages[currentHat];
        hats[currentHat].SetActive(true);
    }

    public void RightHat()
    {
        hats[currentHat].SetActive(false);
        currentHat += 1;
        if (currentHat >= hatImages.Count)
            currentHat = 0;
        hatImage.sprite = hatImages[currentHat];
        hats[currentHat].SetActive(true);
    }


    public void LeftMask()
    {
        masks[currentMask].SetActive(false);
        currentMask -= 1;
        if (currentMask < 0)
            currentMask = maskImages.Count - 1;
        maskImage.sprite = maskImages[currentMask];
        masks[currentMask].SetActive(true);
    }

    public void RightMask()
    {
        masks[currentMask].SetActive(false);
        currentMask += 1;
        if (currentMask >= maskImages.Count)
            currentMask = 0;
        maskImage.sprite = maskImages[currentMask];
        masks[currentMask].SetActive(true);
    }


    public void LeftColor()
    {
        currentColor -= 1;
        if (currentColor < 0)
            currentColor = colorImages.Count - 1;
        colorImage.sprite = colorImages[currentColor];
        playerMaterial.color = colors[currentColor];
    }

    public void RightColor()
    {
        currentColor += 1;
        if (currentColor >= colorImages.Count)
            currentColor = 0;
        colorImage.sprite = colorImages[currentColor];
        playerMaterial.color = colors[currentColor];
    }


    //////////////////////////////////////////////////////////

    private EOSTransportManager transportManager = null;

    private bool isHost = false;

    private bool isClient = false;



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
}
