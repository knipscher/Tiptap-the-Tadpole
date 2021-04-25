using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private bool playerOneHasMovedHorizontally = false;
    private bool playerOneHasMovedVertically = false;

    [SerializeField]
    private GameObject playerTwoCompleteSign = default;

    [SerializeField]
    private Button playerTwoButton = default;

    private bool playerTwoReady = false;

    private void Start()
    {
        playerTwoButton.onClick.AddListener(PlayerTwoButton);
    }

    private void PlayerTwoButton()
    {
        SoundEffectsManager.instance.PlaySoundEffect(SoundEffect.Woodblock);
        playerTwoReady = true;
        SceneManager.LoadScene("Main");
    }


    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            playerOneHasMovedHorizontally = true;
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            playerOneHasMovedVertically = true;
        }

        if (Input.GetMouseButtonDown(0) && playerOneHasMovedHorizontally && playerOneHasMovedVertically)
        {
            playerTwoCompleteSign.SetActive(true);
        }
    }
}