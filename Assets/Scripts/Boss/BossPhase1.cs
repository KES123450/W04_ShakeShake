using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossPhase1 : Boss
{
    public Boss nextBossPhase;
    [SerializeField] private float moveToCenterSpeed;
    [SerializeField] private string animationName;
    [SerializeField] private float delay;
    public override void OnDamage(int damage = 1)
    {
        StartCoroutine(nameof(StartNextPhase));
    }

    private IEnumerator StartNextPhase()
    {
        if (isDeal)
        {
            ShutdownAction();
            anim.Play(animationName);
            yield return new WaitForSeconds(delay);
            Vector3 center = Vector3.zero;
            transform.DOMove(center, moveToCenterSpeed)
                .OnComplete(() =>
                {
                    
                    nextBossPhase.transform.position = transform.position;
                    nextBossPhase.gameObject.SetActive(true);
                    gameObject.SetActive(false);
                    nextBossPhase.Initialize();
                });
        }
    }
}
