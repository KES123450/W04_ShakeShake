using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HoneyPattern : BossPattern
{
    [SerializeField] private GameObject honey;
    [SerializeField] private Transform honeyBarrage;
    [SerializeField] private float barrageSpeed;

    protected override void ActionContext()
    {
        
    }

    private void ShootHoney()
    {
        Vector3 playerPos = GameManager.instance.GetPlayer().transform.position;
        transform.DOMove(playerPos, barrageSpeed)
            .OnComplete(() =>
            {

            });
    }
}
