using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMController : MonoBehaviour
{
    public AudioClip[] myBGMClip;
    public AudioClip[] myButtonClip;
    [HideInInspector]public AudioSource myAudioSource;

    private void Awake() {
        myAudioSource = GetComponent<AudioSource>();

        string levelName = SceneManager.GetActiveScene().name;

        if(levelName == "MainMenu"){
            myAudioSource.clip = myBGMClip[0];
            myAudioSource.loop = true;
            myAudioSource.volume = 0.7f;
            myAudioSource.Play();
        }else if(levelName == "LevelSelect"){
            myAudioSource.clip = myBGMClip[1];
            myAudioSource.loop = true;
            myAudioSource.volume = 0.7f;
            myAudioSource.Play();
        }else if(levelName == "Level1" || levelName == "Level2"){
            myAudioSource.clip = myBGMClip[2];
            myAudioSource.loop = true;
            myAudioSource.volume = 0.7f;
            myAudioSource.Play();
        }else if(levelName == "Level3"){
            myAudioSource.clip = myBGMClip[3];
            myAudioSource.loop = true;
            myAudioSource.volume = 0.7f;
            myAudioSource.Play();
        }
    }
}
