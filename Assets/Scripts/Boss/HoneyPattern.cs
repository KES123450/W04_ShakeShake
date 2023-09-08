using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HoneyPattern : BossPattern
{
    [SerializeField] private GameObject honeyPrefab;
    [SerializeField] private Transform honeyBarrage;
    [SerializeField] private float barrageSpeed;

    protected override void ActionContext()
    {
        ShootHoney();
    }

    private void ShootHoney()
    {
        Vector3 playerPos = GameManager.instance.GetPlayer().transform.position;
        honeyBarrage.gameObject.SetActive(true);
        honeyBarrage.position = transform.position;
        honeyBarrage.DOMove(playerPos, barrageSpeed)
            .OnComplete(() =>
            {
                Instantiate(honeyPrefab, playerPos, Quaternion.identity);
                honeyBarrage.gameObject.SetActive(false);
                
            });
    }
}
