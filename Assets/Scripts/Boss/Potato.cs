using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potato : MonoBehaviour, IDamageable
{
    [SerializeField] private LayerMask potatoLayer;
    [SerializeField] private float raycastDistance;
    [SerializeField] private float moveSpeed;
    private bool isMove=false;
    private Vector2 direction=Vector2.right;


    public void InitPotato(Vector2 dir, float speed)
    {
        direction = dir;
        moveSpeed = speed;
    }

    public void OnDamage(int damage = 1)
    {
        Destroy(gameObject);
    }


    public void CanMove() => isMove = true;
    private void MoveForward()
    {
        if (isMove)
        {
            transform.Translate(direction * moveSpeed);

            if (RaycastCollision())
            {
                // 뛰는 애니메이션 실행
                return;
            }
        }
        
    }
    
    private bool RaycastCollision()
    {
        Vector2 playerPos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(playerPos, Vector2.right, raycastDistance, potatoLayer);

        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    private void Update()
    {
        MoveForward();
    }
}
