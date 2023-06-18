using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMonster : MonoBehaviour
{
    public float distance = 0.5f;
    public GameObject knife;
    public GameObject monster;

    private Vector2 direction;
    private float speed;
    private float targetTime = 1f;
    private float superMonsterChildtargetTime = 2f;

    void Start()
    {
        direction = transform.right;
        speed = GameManager.Instance.smallMonsterSpeed;
    }

    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
        targetTime -= Time.deltaTime;
        superMonsterChildtargetTime -= Time.deltaTime;
        if (targetTime <= 0 && gameObject.CompareTag("SuperEnemy"))
        {
            SpawnMonster();
            targetTime = Random.Range(GameManager.Instance.minMonsterSpawnRate, GameManager.Instance.maxMonsterSpawnRate);
        }
        if (targetTime <= 0)
        {
            ThrowAKnife();
            if(gameObject.CompareTag("SuperEnemy"))
            {
                targetTime = Random.Range(0.5f, 1.5f);
            }
            else
            {
                targetTime = Random.Range(GameManager.Instance.minKnifeRate, GameManager.Instance.maxKnifeRate);
            }
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            transform.position = transform.position + Vector3.down * distance;
            transform.Rotate(0, 180, 0);
        }
    }

    private void ThrowAKnife()
    {
        Instantiate(knife, transform.position, Quaternion.Euler(-180, 0, 0));
    }

    void SpawnMonster()
    {
        Instantiate(monster, transform.position - new Vector3(Random.Range(-2f,2f), 1, 0), Quaternion.identity);

    }
}
