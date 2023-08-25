using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int coinValue = 1;
    [SerializeField] GameObject coinEffect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            AudioManager.instance.PlaySfx(AudioManager.instance.sfxSource[0]);
            GameManager.instance.AddCoins(coinValue);
            Instantiate(coinEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }   
    }
}
