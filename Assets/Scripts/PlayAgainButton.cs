using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayAgainButton : MonoBehaviour
{
    private Button button;
    [SerializeField]
    private string nameOfSceneToMoveTo = default;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlayAgain);
    }

    private void PlayAgain()
    {
        SoundEffectsManager.instance.PlaySoundEffect(SoundEffect.Woodblock);
        SceneManager.LoadScene(nameOfSceneToMoveTo);
    }
}