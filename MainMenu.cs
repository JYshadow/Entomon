using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Static")]
    public GameObject fadeImage;
    public GameObject loadingText;
    public Camera mainCamera;

    FirstExplosion firstExplosion;
    bool loadDisclaimer = false;
    float menuFadeTime = 0f;
    float menuFadeDurtion = 3f;
    bool loadGame = false;
    float disclaimerFadeTime = 0f;
    float disclaimerFadeDuration = 7f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        firstExplosion = FindObjectOfType<FirstExplosion>();

        if (fadeImage != null)
        {
            fadeImage.SetActive(false);
        }

        if (loadingText != null)
        {
            loadingText.SetActive(false);
        }
    }

    public void Update()
    {
        if (loadDisclaimer == true)
        {
            menuFadeTime += Time.deltaTime;
            if (menuFadeTime > menuFadeDurtion)
            {
                menuFadeTime = menuFadeDurtion;
            }

            mainCamera.fieldOfView = Mathf.Lerp(60f, 120f, menuFadeTime / menuFadeDurtion);

            fadeImage.GetComponent<Image>().color = Color.Lerp(new Color32(0, 0, 0, 0), new Color32(0, 0, 0, 255), menuFadeTime / menuFadeDurtion);           
            if (fadeImage.GetComponent<Image>().color == Color.black)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        if (loadGame == true)
        {
            disclaimerFadeTime += Time.deltaTime;
            if (disclaimerFadeTime > disclaimerFadeDuration)
            {
                disclaimerFadeTime = disclaimerFadeDuration;
            }

            float t = disclaimerFadeTime / disclaimerFadeDuration;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);
            fadeImage.GetComponent<Image>().color = Color.Lerp(new Color32(0, 0, 0, 0), new Color32(0, 0, 0, 255), t);
            if (fadeImage.GetComponent<Image>().color == Color.black)
            {
                StartCoroutine(LoadNextScene());
            }

            if (loadingText != null)
            {
                loadingText.SetActive(true);
            }
        }
    }

    public void LoadDisclaimer()
    {
        fadeImage.SetActive(true);
        menuFadeTime = 0;
        loadDisclaimer = true;
    }

    public void QuitGame()
    {
        print("quit");
        Application.Quit();
    }

    public void LoadGame()
    {
        StartCoroutine(firstExplosion.PlayFirstExplosion());
        fadeImage.SetActive(true);
        disclaimerFadeTime = 0;
        loadGame = true;
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(8f);
        print("loading");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenFeedbackform()
    {
        Application.OpenURL("https://form.jotform.com/213615001400333");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
