using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public const float MapSize = 40;

    public static GameManager instance;
    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;

    [SerializeField] private string endingSceneName;
    private Animator playerAni;
    private SpriteRenderer bossSprite;

    private void Start()
    {
        playerAni = player.GetComponent<Animator>();
        bossSprite = boss.GetComponent<SpriteRenderer>();
    }
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

    public void StartEnding()
    {
        StartCoroutine(nameof(Ending));
    }
    private IEnumerator Ending()
    {
        yield return new WaitForSeconds(2f);
        playerAni.Play("Dance");
        bossSprite.DOFade(0f, 3f);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(endingSceneName);

    }
}
