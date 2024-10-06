using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    bool showingHost = false, showingJoin = false, showingSettings = false;
    [SerializeField] GameObject hostSettings, joinSettings, settings;


    // Start is called before the first frame update
    void Begin()
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

    public void Quit() {
    Application.Quit();    
    }



}
