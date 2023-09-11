using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pepper : MonoBehaviour
{
	[SerializeField] private GameObject firePrefab;
    [SerializeField] private float activeTime;
	[SerializeField] private float rotateSpeed;
	[SerializeField] private float guideSpeed;
	[SerializeField] private float radius;
	[SerializeField] private LayerMask bossLayer;
	[SerializeField] private LayerMask playerLayer;
	private Rigidbody2D rigid;
	private float timer;

    private void Start()
    {
		rigid = GetComponent<Rigidbody2D>();
    }
    private void FollowToPlayer()
    {
		if (timer <= activeTime)
		{
			if(Physics2D.OverlapCircle(transform.position, radius, playerLayer) != null)
            {
				ExplosionPepper();
            }
			Vector2 dir = transform.right;
			Vector2 targetDir = GameManager.instance.GetPlayer().transform.position - transform.position;
			Vector3 crossVec = Vector3.Cross(dir, targetDir);
			float inner = Vector3.Dot(Vector3.forward, crossVec);
			float saveAngle = inner + transform.rotation.eulerAngles.z;
			transform.rotation = Quaternion.Euler(0, 0, saveAngle * rotateSpeed);

			float moveDirAngle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
			Vector2 moveDir = Vector2.zero;
			moveDir = new Vector2(Mathf.Cos(moveDirAngle), Mathf.Sin(moveDirAngle));
			rigid.velocity = moveDir * guideSpeed;
			timer += Time.deltaTime;
		}
		else
		{
			ExplosionPepper();
		}
	}

	private void ExplosionPepper()
    {
		Collider2D bossCollider = Physics2D.OverlapCircle(transform.position, radius, bossLayer);
		Debug.Log(bossCollider);
        if (bossCollider != null)
        {
			bossCollider.GetComponent<Boss>().StartOnWeak();
        }


		Instantiate(firePrefab, transform.position, Quaternion.identity);
		Destroy(gameObject);
    }

    private void Update()
    {
		FollowToPlayer();
    }


}
