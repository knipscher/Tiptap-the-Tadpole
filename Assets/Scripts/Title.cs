using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private bool isAllowedToStart = false;
    [SerializeField]
    private GameObject pressAnyKeyCaption = default;

    private IEnumerator Start()
    {
        pressAnyKeyCaption.SetActive(false);
        yield return new WaitForSeconds(12);
        pressAnyKeyCaption.SetActive(true);
        yield return new WaitForSeconds(1);
        isAllowedToStart = true;
    }

    private void Update()
    {
        if (isAllowedToStart && Input.anyKey)
        {
            StartTutorial();
        }
    }

    private void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}