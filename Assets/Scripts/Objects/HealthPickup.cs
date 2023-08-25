using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healthAmount = 1;
    [SerializeField] bool isFullHealth = false;
    [SerializeField] GameObject healthEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            AudioManager.instance.PlaySfx(AudioManager.instance.sfxSource[1]);
            Instantiate(healthEffect, transform.position, transform.rotation);

            if (isFullHealth)
                HealthManager.instance.ResetHealth();
            else
                HealthManager.instance.AddHealth(healthAmount);
        }
    }
}





