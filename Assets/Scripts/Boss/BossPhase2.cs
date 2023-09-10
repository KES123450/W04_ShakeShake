using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase2 : Boss
{
    public Boss nextBossPhase;

    public override void OnDamage(int damage = 1)
    {
        if (isDeal)
        {
            nextBossPhase.transform.position = transform.position;
            nextBossPhase.gameObject.SetActive(true);
            gameObject.SetActive(false);
            nextBossPhase.Initialize();
        }
    }
}
