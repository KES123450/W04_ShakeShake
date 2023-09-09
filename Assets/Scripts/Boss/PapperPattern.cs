using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PapperPattern : BossPattern
{
    [SerializeField] private GameObject pepperPrefab;

    private Vector3[] cornerPositions = new Vector3[4];

    private void Start()
    {
        float halfMapSize = (GameManager.MapSize) / 2;
        cornerPositions[0] = new Vector3(-halfMapSize, halfMapSize, 0);
        cornerPositions[1] = new Vector3(halfMapSize, halfMapSize, 0);
        cornerPositions[2] = new Vector3(-halfMapSize, -halfMapSize, 0);
        cornerPositions[3] = new Vector3(halfMapSize, -halfMapSize, 0);
    }

    protected override void ActionContext()
    {
        SpawnPepper();
    }

    private void SpawnPepper()
    {
        Vector3 playerPos = GameManager.instance.GetPlayer().transform.position;
        var sortedByMagnitude = cornerPositions.OrderBy(v => (playerPos - v).magnitude);

        Vector3 pepperSpawnPoint = sortedByMagnitude.First();
        Instantiate(pepperPrefab, pepperSpawnPoint, Quaternion.identity);
    }
    
}
