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
    

    public void OpenLevelSelect()
    {
        MainMenuCanvas.gameObject.SetActive(false);
        LevelSelectCanvas.gameObject.SetActive(true);
    }

    public void OpenSettings()
    {
        MainMenuCanvas.gameObject.SetActive(false);
        SettingsCanvas.gameObject.SetActive(true);
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


    public void SaveSettings()
    {
        DataController data = FindObjectOfType<DataController>();
        //gameovbject.find() all the slider/button values
        //puts all values on the data controller and save is called in Cancel()

        Cancel();
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

            SettingsCanvas.gameObject.SetActive(false);
            MainMenuCanvas.gameObject.SetActive(true);

        }
        //levelselect -> main menu
        if (LevelSelectCanvas.gameObject.activeSelf)
        {
            LevelSelectCanvas.gameObject.SetActive(false);
            MainMenuCanvas.gameObject.SetActive(true);
        }

        FindObjectOfType<DataController>().SaveGameData();
    }


}
