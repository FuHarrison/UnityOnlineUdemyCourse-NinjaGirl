using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PanelButtonScript : MonoBehaviour
{
    public GameObject selectPanel,stopButton, levelSelectButton,mainMenuButton,replayButton;

    public void LevelSelectButton(){
        // SceneManager.LoadScene("LevelSelect");
        BGMController myBGM = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBGM.myAudioSource.PlayOneShot(myBGM.myButtonClip[0]);

        FadeInOut.instance.SceneFadeInOut("LevelSelect");
        Time.timeScale = 1.0f; 
    }

    public void MainMenuButton(){
        // SceneManager.LoadScene("MainMenu");
        BGMController myBGM = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBGM.myAudioSource.PlayOneShot(myBGM.myButtonClip[0]);

        FadeInOut.instance.SceneFadeInOut("MainMenu");
        Time.timeScale = 1.0f; 
    }

    public void ReplayButton(){
        string sceneName = SceneManager.GetActiveScene().name;
        // SceneManager.LoadScene(sceneName);
        BGMController myBGM = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBGM.myAudioSource.PlayOneShot(myBGM.myButtonClip[0]);

        FadeInOut.instance.SceneFadeInOut(sceneName);
        Time.timeScale = 1.0f; 
    }

    public void NoButton(){
        RectTransform DataDeleteImage = GameObject.Find("Canvas/SafeAreaPanel/DataDeleteImage").GetComponent<RectTransform>();
        DataDeleteImage.anchoredPosition = new Vector2(0f,1500f);        

        BGMController myBGM = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBGM.myAudioSource.PlayOneShot(myBGM.myButtonClip[0]);
    }

    public void YesButton(){
        PlayerPrefs.DeleteAll();
        IsFirstTimePlayCheck checkSctipt = GameObject.Find("IsFirstTimePlayCheck").GetComponent<IsFirstTimePlayCheck>();
        checkSctipt.FirstTimePlayState();
        RectTransform DataDeleteImage = GameObject.Find("Canvas/SafeAreaPanel/DataDeleteImage").GetComponent<RectTransform>();
        DataDeleteImage.anchoredPosition = new Vector2(0f,1500f);        

        BGMController myBGM = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBGM.myAudioSource.PlayOneShot(myBGM.myButtonClip[0]);
    }

    public void DataDeleteButton(){
        RectTransform DataDeleteImage = GameObject.Find("Canvas/SafeAreaPanel/DataDeleteImage").GetComponent<RectTransform>();
        DataDeleteImage.anchoredPosition = new Vector2(0f,-100f);

        BGMController myBGM = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBGM.myAudioSource.PlayOneShot(myBGM.myButtonClip[0]);
    }

    public void setSelectPanelOn(){
        selectPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void setSelectPanelOff(){
        selectPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void setStopButtonOn(){
        BGMController myBGM = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBGM.myAudioSource.PlayOneShot(myBGM.myButtonClip[0]);

        stopButton.SetActive(true);
    }

    public void setStopButtonOff(){
        BGMController myBGM = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBGM.myAudioSource.PlayOneShot(myBGM.myButtonClip[0]);
        
        stopButton.SetActive(false);
    }    

    public void MainMenuPlayButton(){
        GameObject mainMenuPlayer  = GameObject.Find("MainMenuPlayer");
        Animator myAnim = mainMenuPlayer.GetComponent<Animator>();
        myAnim.SetBool("Run",true);

        GameObject playButton = GameObject.Find("Canvas/SafeAreaPanel/PlayButton");
        playButton.SetActive(false);

        BGMController myBGM = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBGM.myAudioSource.PlayOneShot(myBGM.myButtonClip[0]);

        StartCoroutine(LoadLevelSelect());
    }

    IEnumerator LoadLevelSelect(){
        yield return new WaitForSeconds(1.5f);
        FadeInOut.instance.SceneFadeInOut("LevelSelect");
    }
}
