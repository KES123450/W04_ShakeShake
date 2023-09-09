using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WalnutPattern : BossPattern
{
    [SerializeField] int walnutNum;
    [SerializeField] float walnutRadius;
    [SerializeField] float walnutAngularSpeed;
    [SerializeField] GameObject shadowPrefab;
    [SerializeField] GameObject walnutPrefab;

    float currentAngle;
    GameObject shadow;
    List<Walnut> spawnedWalnuts = new();

    private void Update()
    {
        if (spawnedWalnuts.Count > 0)
        {
            currentAngle += walnutAngularSpeed * Time.deltaTime;
            ForeachWalnut((index, radian) =>
            {
                spawnedWalnuts[index].UpdateRotate(transform.position, radian + currentAngle * Mathf.Deg2Rad);
            });
        }
    }

    protected override void PreProcessing()
    {
        spawnedWalnuts.Clear();
        base.PreProcessing();
        CastShadow();
    }
    protected override void ActionContext()
    {
        SpawnWalnuts();
    }
    protected override void PostProcessing()
    {
        base.PostProcessing();
        foreach (var w in spawnedWalnuts.ToList())
        {
            spawnedWalnuts.Remove(w);
            w.EndRotate();
        }
    }
    void CastShadow()
    {
        shadow = Instantiate(shadowPrefab, transform.position, Quaternion.identity);
        shadow.transform.localScale = Vector3.one * walnutRadius * 2;
        Destroy(shadow, preDelaySeconds);
    }
    void SpawnWalnuts()
    {
        ForeachWalnut((index, radian) =>
        {
            var walnut = Instantiate(walnutPrefab, transform.position, Quaternion.identity).GetComponent<Walnut>();
            walnut.Initialize(transform.position, walnutRadius, radian);
            spawnedWalnuts.Add(walnut);
        });
    }

    void ForeachWalnut(System.Action<int, float> action)
    {
        for (int i = 0; i < walnutNum; i++)
        {
            var radian = (2 * Mathf.PI * i) / walnutNum;
            action?.Invoke(i, radian);
        }
    }
}
