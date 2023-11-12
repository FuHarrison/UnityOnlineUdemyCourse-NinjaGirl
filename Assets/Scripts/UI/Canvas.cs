using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{
    public Text lifeText, kunaiText, stoneText;

    private void Awake() {
        LifeUpdate();
        KunaiUpdate();
        StoneUpdate();
    }

    public void LifeUpdate(){
        lifeText.text = "X" + PlayerPrefs.GetInt("PlayerLife").ToString();
    }

    public void KunaiUpdate(){
        kunaiText.text = "X" +  PlayerPrefs.GetInt("PlayerKunai").ToString();
    }

    public void StoneUpdate(){
        stoneText.text = "X" +  PlayerPrefs.GetInt("PlayerStone").ToString();        
    }
}
