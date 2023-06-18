using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public float smallMonsterSpeed = 1.2f;
    public float bossSpeed = 0.5f;
    public int minMonsterSpawnRate = 2;
    public int maxMonsterSpawnRate = 6;
    public int minKnifeRate = 2;
    public int maxKnifeRate = 6;
    public int bossHearts = 3;
    public bool isSuperPlayer;

    public static GameManager Instance;

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnEnable()
    {
        EventManager.BossHitEvent += BossCondition;
        EventManager.AddHeartEvent += AddHeart;
    }

    private void OnDisable()
    {
        EventManager.BossHitEvent -= BossCondition;
        EventManager.AddHeartEvent -= AddHeart;
    }

    private void AddHeart()
    {
        if(bossHearts < 3)
        {
            bossHearts += 1;
        }
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        } else
        {
            Instance = this;
        }
    }

    private void BossCondition()
    {
        bossHearts -= 1;
        if (bossHearts == 0)
        {
            EventManager.EndGame();
            Time.timeScale = 0;
        }
    }

    private void Start()
    {
        float randomChanceToBeSuperPlayer = Random.value;
        if(randomChanceToBeSuperPlayer < 0.50f)
        {
            isSuperPlayer = false;
        } else
        {
            isSuperPlayer = true;
        }
        Debug.Log(isSuperPlayer);
    }
}
