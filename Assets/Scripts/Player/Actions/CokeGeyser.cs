using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CokeGeyser : MonoBehaviour
{
    [Header("Coke Can Object")]
    [SerializeField] GameObject cokeObject;

    new BoxCollider2D collider;
    LayerMask targetLayer;
    List<IDamageable> damaged;
    Vector3 currentDirection;
    Vector2 defaultColliderSize;
    float maxAngularSpeed;
    float width;

    public Quaternion CurrentRotation => Quaternion.FromToRotation(Vector2.right, currentDirection);
    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        defaultColliderSize = collider.size;
        damaged = new();
    }
    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
        cokeObject.SetActive(value);
    }
    public void StartGeyser(LayerMask targetLayer, Vector2 startPoint, Vector2 aimDirection, float angularSpeed, float width)
    {
        this.width = width;
        this.targetLayer = targetLayer;
        maxAngularSpeed = angularSpeed;
        currentDirection = aimDirection;
        transform.localScale = Vector3.zero;
        damaged.Clear();
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

        currentDirection = Vector3.RotateTowards(currentDirection, direction, maxAngularSpeed * Mathf.Deg2Rad * Time.deltaTime, 0) ;

        transform.position = startPoint + (Vector2)currentDirection * length / 2;
        transform.rotation = CurrentRotation;
        transform.localScale = Vector2.right * length / defaultColliderSize.x + Vector2.up * width / defaultColliderSize.y;
        cokeObject.transform.rotation = CurrentRotation;

        var filter = new ContactFilter2D();
        filter.useTriggers = true;
        filter.SetLayerMask(targetLayer);
        var overlapResult = new List<Collider2D>();
        collider.OverlapCollider(filter, overlapResult); 

        overlapResult.
            Select(c => c.GetComponent<IDamageable>()).
            Where(d => d != null && !damaged.Contains(d)).
            ForEach((d) => 
        {
            d.OnDamage();
            damaged.Add(d);
        });
    }
}
