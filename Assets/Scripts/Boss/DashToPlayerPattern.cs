using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashToPlayerPattern : BossPattern
{
    [SerializeField] private float dashForce;
    [SerializeField] private Rigidbody2D rigid;

    protected override void PreProcessing()
    {
        base.PreProcessing();
        rigid.velocity = Vector2.zero;
        //�ִϸ��̼� �����Ű��
    }
    protected override void ActionContext()
    {
        Dash();
    }

    private void Dash()
    {
        Vector3 playerDirection = GameManager.instance.GetPlayer().transform.position - transform.position;
        rigid.AddForce(playerDirection * dashForce, ForceMode2D.Impulse);
    }
}
