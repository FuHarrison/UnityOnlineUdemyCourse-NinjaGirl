using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsFirstTimePlayCheck : MonoBehaviour
{
    private void Awake() {
        FirstTimePlayState();
    }

    public void FirstTimePlayState(){
        if(!PlayerPrefs.HasKey("IsFirstTimePlay")){
            PlayerPrefs.SetInt("IsFirstTimePlay",1);
            PlayerPrefs.SetInt("PlayerLife",5);
            PlayerPrefs.SetInt("PlayerKunai",3);
            PlayerPrefs.SetInt("PlayerStone",0);
            PlayerPrefs.SetInt("clearedLevel",0);
        }  
    }
}
