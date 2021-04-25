using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    public bool gameOver = false;

    [SerializeField]
    private float gameLengthInSeconds = 10;

    [SerializeField]
    private GameObject player1WinPanel = default;
    [SerializeField]
    private GameObject player1LosePanel = default;

    [SerializeField]
    private Button playAgainButton = default;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Start()
    {
        playAgainButton.gameObject.SetActive(false);

        player1WinPanel.SetActive(false);
        player1LosePanel.SetActive(false);

        yield return new WaitForSeconds(gameLengthInSeconds);

        if (!gameOver)
        {
            Player1WinGame();
        }
    }

    public void Player1WinGame()
    {
        gameOver = true;
        SceneManager.LoadScene("Player 1 Win");
    }

    public void Player1LoseGame()
    {
        player1LosePanel.SetActive(true);
        gameOver = true;
        playAgainButton.gameObject.SetActive(true);
    }
}