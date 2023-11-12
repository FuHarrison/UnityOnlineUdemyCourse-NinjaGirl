using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaPanel : MonoBehaviour
{
    RectTransform myPanel;

    private void Awake(){
        myPanel = GetComponent<RectTransform>();

        Vector2 safeAreaMinPosition = Screen.safeArea.position;
        Vector2 safeAreaMaxPosition = Screen.safeArea.position + Screen.safeArea.size;

        safeAreaMinPosition.x /= Screen.width;
        safeAreaMinPosition.y /= Screen.height;

        safeAreaMaxPosition.x /= Screen.width;
        safeAreaMaxPosition.y /= Screen.height;

        myPanel.anchorMin = safeAreaMinPosition;
        myPanel.anchorMax = safeAreaMaxPosition;
    }
}
