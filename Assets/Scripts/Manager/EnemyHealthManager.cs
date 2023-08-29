using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public static EnemyHealthManager instance;
    [SerializeField] private int numLives;
    private new Renderer renderer; // El componente Renderer de la cápsula
    private Color originalColor;
    [SerializeField] GameObject deathEffect;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
    }


    public void HurtEnemy()
    {
        NumLives--;
    }

    public int NumLives
    {
        get => numLives;
        set {
            numLives = value;

            if (numLives > 0)
            {
                StartCoroutine(ChangeColorForSeconds(3f));
            }
            else
            {
                //Mostar efecto de muerte 
                Instantiate(deathEffect, transform.position + new Vector3(0f, 1f, 0f), transform.rotation);

                Destroy(gameObject);
            }
        }
    }
    IEnumerator ChangeColorForSeconds(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            renderer.material.color = Color.Lerp(originalColor, Color.red, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        renderer.material.color = originalColor; // Restaurar el color original
    }

}
