using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const float MapSize = 40;

    public static GameManager instance;
    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public GameObject GetPlayer() => player;

    public GameObject GetBoss() => boss;

    public void SetBoss(GameObject bossObject)
    {
        boss = bossObject;
    }

    public void GameOver()
    {
        UIManager.Instance.EnableGameOverUI();
    }
}
