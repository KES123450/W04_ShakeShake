using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CokeGeyser : MonoBehaviour
{
    LineRenderer lineRenderer;
    BoxCollider2D collider;

    LayerMask targetLayer;
    List<IDamageable> damaged;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }
    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
    public void StartGeyser(LayerMask targetLayer, Vector2 startPoint)
    {
        lineRenderer.SetPositions(new Vector3[]{ startPoint, startPoint });
        this.targetLayer = targetLayer;
        SetActive(true);
    }
    public void EndGeyser()
    {
        SetActive(false);
    }
    public void UpdatePoint(Vector2 startPoint, Vector2 targetVector)
    {
        var direction = targetVector.normalized;
        var length = targetVector.magnitude;

        transform.rotation = Quaternion.FromToRotation(Vector2.right, direction);

        collider.offset = Vector2.right * length / 2;
        collider.size = Vector2.right * length + Vector2.up;

        lineRenderer.SetPositions(new Vector3[] { startPoint, startPoint + targetVector });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetLayer.Contains(collision.gameObject.layer) && collision.transform.TryGetComponent<IDamageable>(out var damageable))
        {
            if (!damaged.Contains(damageable))
            {
                damageable.OnDamage();
                damaged.Add(damageable);
            }
        }
    }

}
