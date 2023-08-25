using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            AudioManager.instance.PlaySfx(AudioManager.instance.sfxSource[5]);
            GameManager.instance.Respawn();
        }
    }
}
