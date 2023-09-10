using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase1 : Boss
{
    public Boss nextBossPhase;

    public override void OnDamage(int damage = 1)
    {

        Debug.Log("isDeal : " + isDeal);
        Debug.Log("next");

        if (isDeal)
        {
            Debug.Log("isDeal : " + isDeal);
            Debug.Log("next");
            nextBossPhase.transform.position = transform.position;
            nextBossPhase.gameObject.SetActive(true);
            gameObject.SetActive(false);
            nextBossPhase.Initialize();
        }
    }
}
