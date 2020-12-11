using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    //tg menu manager style
    //credits: pastor

    public Canvas MainMenuCanvas;
    public Canvas LevelSelectCanvas;
    public Canvas SettingsCanvas;
    public Canvas LoadScreenCanvas;
    public Canvas CreditsCanvas;
    //som failsafe if somethin breaks or stuf (WRONG)
    public GameObject Temp;
    private int culoDolor;


    public void OpenLevelSelect()
    {
        MainMenuCanvas.gameObject.SetActive(false);
        LevelSelectCanvas.gameObject.SetActive(true);
    }

    public void OpenSettings()
    {
        MainMenuCanvas.gameObject.SetActive(false);
        SettingsCanvas.gameObject.SetActive(true);
        DataController dolor = FindObjectOfType<DataController>();
        GameObject.Find("sliderFOV").GetComponent<Slider>().value = dolor.LoadedData.FOV;
        GameObject.Find("sliderMouseSens").GetComponent<Slider>().value = dolor.LoadedData.MouseSensitivity;
    }

    //load stage from level select
    public void LoadStageLevelSelect(int stagenumber)
    {
        LevelSelectCanvas.gameObject.SetActive(false);
        LoadScreenCanvas.gameObject.SetActive(true);
        StartCoroutine(LoadStage(stagenumber));
    }

    IEnumerator LoadStage(int stagenumber)
    {
        yield return new WaitForSeconds(5);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(stagenumber);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void OpenCredits()
    {
        MainMenuCanvas.gameObject.SetActive(false);
        CreditsCanvas.gameObject.SetActive(true);
    }

    public void ButonOnINSAIN()
    {
        culoDolor++;
        if(culoDolor > 5)
        {
            culoDolor = 0;
            if (!FindObjectOfType<DataController>().LoadedData.LazyRiver)
            {
                MainMenuCanvas.gameObject.SetActive(false);
                Temp.SetActive(true);
            }
        }
    }

    public void SaveSettings()
    {
        DataController data = FindObjectOfType<DataController>();
        data.LoadedData.FOV = (int)GameObject.Find("sliderFOV").GetComponent<Slider>().value;
        data.LoadedData.MouseSensitivity = GameObject.Find("sliderMouseSens").GetComponent<Slider>().value;
    }


    public void Cancel()
    {
        //main menu -> kill game
        if (MainMenuCanvas.gameObject.activeSelf)
        {
            //exit game?
            Application.Quit();
        }
        //settings -> main menu
        if (SettingsCanvas.gameObject.activeSelf)
        {
            SaveSettings();
            SettingsCanvas.gameObject.SetActive(false);
            MainMenuCanvas.gameObject.SetActive(true);

        }
        //levelselect -> main menu
        if (LevelSelectCanvas.gameObject.activeSelf)
        {
            LevelSelectCanvas.gameObject.SetActive(false);
            MainMenuCanvas.gameObject.SetActive(true);
        }
        //credits -> main menu
        if (CreditsCanvas.gameObject.activeSelf)
        {
            CreditsCanvas.gameObject.SetActive(false);
            MainMenuCanvas.gameObject.SetActive(true);
        }

        //dolor
        if (Temp.activeSelf)
        {
            Temp.SetActive(false);
            MainMenuCanvas.gameObject.SetActive(true);
        }

        FindObjectOfType<DataController>().SaveGameData();
    }


}
