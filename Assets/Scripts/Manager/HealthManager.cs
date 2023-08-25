using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    [SerializeField] int currentHealth, maxHealth;

    //Invencibilidad
    [SerializeField] float invencibilityLenght = 2f;
    private float invencibilityCounter;

    [SerializeField] Sprite[] healthBarImages;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        ResetHealth();
    }

    void Update()
    {
        if(invencibilityCounter > 0)
        {
            invencibilityCounter -= Time.deltaTime;
        }

        //Activar y desactivar el personaje
        foreach (GameObject piece in PlayerController.instance.playerPieces)
        {
            if(Mathf.Floor(invencibilityCounter * 5f) % 2 == 0)
                piece.SetActive(true);
            else
                piece.SetActive(false);

            if (invencibilityCounter <= 0)
                piece.SetActive(true);
        }
    }



    public void Hurt()
    {
        if(invencibilityCounter <= 0)
        {
            CurrentHealth--;
            UpdateUI();
        }
    }


    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GameManager.instance.Respawn();
            }
            else if(currentHealth >= 0) 
            {
                PlayerController.instance.KnockBack();
                invencibilityCounter = invencibilityLenght;
            }
        }
    }
    
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UIManager.instance.healthImage.enabled = true;
        UpdateUI();
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        UpdateUI();
    }

    //Actualizar el numero de vidas del jugador en la pantalla
    public void UpdateUI()
    {
        UIManager.instance.healthText.text = currentHealth.ToString();
        switch (currentHealth)
        {
            case 5:
                UIManager.instance.healthImage.sprite = healthBarImages[4];
                break;
            case 4:
                UIManager.instance.healthImage.sprite = healthBarImages[3];
                break;
            case 3:
                UIManager.instance.healthImage.sprite = healthBarImages[2];
                break;
            case 2:
                UIManager.instance.healthImage.sprite = healthBarImages[1];
                break;
            case 1:
                UIManager.instance.healthImage.sprite = healthBarImages[0];
                break;
            case 0:
                UIManager.instance.healthImage.enabled = false;
                break;
        }
    }

    public void PlayerKilled()
    {
        currentHealth = 0;
        UpdateUI();
    }
}
