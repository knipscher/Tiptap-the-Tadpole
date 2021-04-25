using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    private Button button;
    private bool isSoundOn = true;

    [SerializeField]
    private GameObject soundOnImage = default;
    [SerializeField]
    private GameObject soundOffImage = default;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Toggle);
        soundOnImage.SetActive(true);
        soundOffImage.SetActive(false);
    }

    private void Toggle()
    {
        isSoundOn = !isSoundOn;
        if (isSoundOn)
        {
            AudioListener.volume = 1;
            soundOnImage.SetActive(true);
            soundOffImage.SetActive(false);
        }
        else
        {
            AudioListener.volume = 0;
            soundOnImage.SetActive(false);
            soundOffImage.SetActive(true);
        }
    }
}