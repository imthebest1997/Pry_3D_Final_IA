using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] GameObject cpON, cpOFF;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.PlaySfx(AudioManager.instance.sfxSource[3]);
            GameManager.instance.SetSpawnPoint(transform.position);

           //TODO: Guardar todos los checkpoints en la escena (Temporal)
           Checkpoint[] allCp = FindObjectsOfType<Checkpoint>();

            foreach (Checkpoint cp in allCp)
            {
                cp.cpOFF.SetActive(true);
                cp.cpON.SetActive(false);
            }

            cpOFF.SetActive(false);
            cpON.SetActive(true);

        }
    }
}
