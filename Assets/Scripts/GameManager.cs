using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int target = 60;
    public int avgFrameRate;
    public TextMeshProUGUI display_Text;
    void Awake()
    {
        Application.targetFrameRate = target;
    }

    
    void Update()
    {
        if (Application.targetFrameRate != target)
            Application.targetFrameRate = target;
        
        float current = 0;
        current = Time.frameCount / Time.time;
        avgFrameRate = (int)current;
        display_Text.text = avgFrameRate.ToString() + " FPS";
    }
}
