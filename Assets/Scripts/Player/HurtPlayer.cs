using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.PlaySfx(AudioManager.instance.sfxSource[2]);
            HealthManager.instance.Hurt();
        }
    }
}
