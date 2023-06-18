using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float speed = 2f;
    private Vector2 direction = Vector2.up;
    private bool penetrated = false;

    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster") || collision.CompareTag("SuperEnemy"))
        {
            float randomValue = Random.value;
            if (randomValue <= 0.25f && !penetrated)
            {
                penetrated = true;
                return;
            }
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Boss") && !penetrated)
        {
            EventManager.BossHit();
        }

        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
