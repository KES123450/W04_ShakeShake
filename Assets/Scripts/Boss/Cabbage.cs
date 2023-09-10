using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabbage : MonoBehaviour, IDamageable
{
    [SerializeField] float speed;
    Rigidbody2D rigidbody;
    float colliderRadius;
    bool canAttackBoss;

    public void OnDamage(int damage = 1)
    {
        Debug.Log("Cabbage.OnDamage");
        var currentPosition = transform.position;

        var inDirection = rigidbody.velocity.normalized;
        var inNormal = (currentPosition - GameManager.instance.GetPlayer().transform.position).normalized;

        var newDirection = Vector2.Dot(inDirection, inNormal) <= 0 ? Vector2.Reflect(inDirection, inNormal).normalized : (inDirection + (Vector2)inNormal).normalized;
        rigidbody.velocity = rigidbody.velocity.magnitude * newDirection;
        canAttackBoss = true;
    }
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        colliderRadius = GetComponent<CircleCollider2D>().radius;
        canAttackBoss = false;
    }
    void Update()
    {
        var angularSpeedRadian = (rigidbody.velocity.x < 0 ? 1 : -1) * rigidbody.velocity.magnitude / colliderRadius;
        transform.Rotate(Vector3.forward * angularSpeedRadian * Mathf.Rad2Deg * Time.deltaTime);
    }

    public void Shoot(Vector2 direction)
    {
        rigidbody.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out var player))
        {
            player.OnDamage();
        }
        if (canAttackBoss && collision.gameObject.TryGetComponent<Boss>(out var boss))
        {
            boss.OnWeak(gameObject);
        }

    }
}
