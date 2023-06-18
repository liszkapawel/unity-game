using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject heartObject;
    public Sprite heartDisabled;
    public Sprite heartEnabled;
    public GameObject heartParent;
    private GameObject[] hearts;
    private int iterator;
    public GameObject endPanel;
    private float targetTime = 10f;

    private void OnEnable()
    {
        EventManager.BossHitEvent += DisableHeart;
        EventManager.EndGameEvent += EndGame;
    }

    private void OnDisable()
    {
        EventManager.BossHitEvent -= DisableHeart;
        EventManager.EndGameEvent -= EndGame;
    }

    void Start()
    {
        
        for(int i = 1; i < GameManager.Instance.bossHearts; i++)
        {
            Instantiate(heartObject, heartParent.transform);
        }
        hearts = GameObject.FindGameObjectsWithTag("Heart");
        iterator = hearts.Length - 1;   

    }
    
    private void Update()
    {
        targetTime -= Time.deltaTime;
        if (targetTime <= 0)
        {
            AddHeart();
            targetTime = 10f;
        }
    }

    private void DisableHeart()
    {
        hearts[iterator].GetComponent<Image>().sprite = heartDisabled;
        iterator -= 1;
    }

    private void AddHeart()
    {
        if(iterator < 2)
        {
            hearts[iterator + 1].GetComponent<Image>().sprite = heartEnabled;
            iterator += 1;
            EventManager.AddHeart();
        }
    }
    private void EndGame()
    {
        endPanel.SetActive(true);
    }
}
