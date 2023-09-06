using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoss : Boss
{
    protected override void OnEnable()
    {
        base.PatternStart();
    }

}
