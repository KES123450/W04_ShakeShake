using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pumpkin : MonoBehaviour,IDamageable
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;
    private Collider2D collider;

    void Start()
    {
        collider= GetComponent<Collider2D>();
    }
    public void OnDamage(int damage = 1)
    {
        gameObject.SetActive(false);
        DOTween.Complete(transform);
        Destroy(gameObject);
    }

    public void EnableCollider()
    {
        collider.enabled = true;
        Collider2D col = Physics2D.OverlapCircle((Vector2)transform.position, radius,playerLayer);
        if (col != null)
        {
            // 플레이어의 OnDamage() 호출
        }
    }

}
