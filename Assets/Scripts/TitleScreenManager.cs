using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    bool showingHost = false, showingJoin = false, showingSettings = false;
    [SerializeField] GameObject hostSettings, joinSettings, settings;
    [SerializeField] Image mapImage;
    [SerializeField] List<Sprite> mapImages;
    [SerializeField] int currentMap;

    // Start is called before the first frame update
    void Awake()
    {
        hostSettings.SetActive(false);
        joinSettings.SetActive(false);
        settings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

}
