using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] Image blackScreen;
    [SerializeField] float fadeSpeed = 1.5f;
    public bool fadeToBlack, fadeFromBlack;

    public Text healthText;
    public Image healthImage;

    public Text coinText;

    public GameObject pauseScreen;


    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        //Pantalla negra cuando el jugador pierde
        if (fadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }

        if (fadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f)
            {
                fadeFromBlack = false;
            }
        }
    }

    public void Resume()
    {
      GameManager.instance.PauseUnpause();
    }

    public void Quit()
    {
        if (UnityEditor.EditorApplication.isPlaying)
            UnityEditor.EditorApplication.isPlaying = false;
        else
            Application.Quit();
    }

    public void MainMenu()
    {
        //Redireccionar a la escena del menu principal
        SceneManager.LoadScene("MainMenuScene");
    }

}
