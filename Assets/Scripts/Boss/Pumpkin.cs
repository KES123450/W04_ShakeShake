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
        collider = GetComponent<Collider2D>();
        collider.enabled = false;
    }
    public void OnDamage(int damage = 1)
    {
        if (collider.enabled)
        {
            gameObject.SetActive(false);
            DOTween.Complete(transform);
            Destroy(gameObject);
        }
    }

    public void EnableCollider()
    {
        collider.enabled = true;
        var result = new List<Collider2D>();
        var filter = new ContactFilter2D();
        filter.SetLayerMask(playerLayer);

        collider.OverlapCollider(filter, result);
        Debug.Log(result.Count);
        result.ForEach(c =>
        {
            if (c.gameObject.TryGetComponent<IDamageable>(out var player))
            {
                player.OnDamage();
            }
        });
    }

}
