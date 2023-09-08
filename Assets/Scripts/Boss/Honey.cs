using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honey : MonoBehaviour
{
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float downSpeed;
    [SerializeField] private float honeyTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(nameof(SpeedDown), collision.GetComponent<PlayerMove>());
        }
    }

    private IEnumerator SpeedDown(PlayerMove player)
    {
        player.SetSpeedModifier(downSpeed);
        yield return new WaitForSeconds(honeyTime);
        player.SetSpeedModifier(defaultSpeed);
    }
}
