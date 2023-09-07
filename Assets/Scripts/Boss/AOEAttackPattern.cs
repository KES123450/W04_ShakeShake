using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEAttackPattern : BossPattern
{
    [SerializeField] private AOEAttack attack;
    protected override void ActionContext()
    {
        attack.StartAttack();
    }
}
