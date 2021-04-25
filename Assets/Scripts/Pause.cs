using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private Button pauseButton;
    private bool isPaused = false;

    private void Start()
    {
        pauseButton = GetComponent<Button>();
        pauseButton.onClick.AddListener(Toggle);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    private void Toggle()
    {
        SoundEffectsManager.instance.PlaySoundEffect(SoundEffect.Woodblock);
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}