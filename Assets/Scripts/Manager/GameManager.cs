using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Vector3 respawnPosition;
    [SerializeField] GameObject deathEffect;
    [SerializeField] int currentCoins;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        respawnPosition = PlayerController.instance.transform.position;
        AddCoins(0);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void Respawn()
    {
        StartCoroutine(RespawnWaiter());
        HealthManager.instance.PlayerKilled();
    }

    public IEnumerator RespawnWaiter()
    {
        //El personaje desaparece y la camara lo deja de seguir
        PlayerController.instance.gameObject.SetActive(false);
        CameraController.instance.cmBrain.enabled = false;
        UIManager.instance.fadeToBlack = true;

        //Permite mostrar objetos en la escena (mundo)
        Instantiate(deathEffect, PlayerController.instance.transform.position + new Vector3(0f, 1f, 0f), PlayerController.instance.transform.rotation);
        
        //Tiempo de espera        
        yield return new WaitForSeconds(2f);
        UIManager.instance.fadeFromBlack = true;

        //Reubicar el personaje, mostrarlo y aplicar la camara
        PlayerController.instance.transform.position = respawnPosition;
        CameraController.instance.cmBrain.enabled = true;
        PlayerController.instance.gameObject.SetActive(true);

        //Restablecer vida
        HealthManager.instance.ResetHealth();

    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
    }


   public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
        UIManager.instance.coinText.text = currentCoins.ToString();
    }

    public void PauseUnpause()
    {
        if(UIManager.instance.pauseScreen.activeInHierarchy)
        {
            UIManager.instance.pauseScreen.SetActive(false);
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UIManager.instance.pauseScreen.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public IEnumerator LevelEndWaiter()
    {
        AudioManager.instance.PlaySfx(AudioManager.instance.musicSource[1]);
        PlayerController.instance.stopMove = true;
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Level-2");
    }
}


