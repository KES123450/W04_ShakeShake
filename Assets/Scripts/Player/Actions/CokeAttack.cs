using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CokeAttack : ActionInfo
{
    [Header("Coke Gauge")]
    [SerializeField] float gaugeRegen;
    [SerializeField] float regenRollMuliplier;
    [SerializeField] float gaugeCostPerSecond;
    [SerializeField] float maxGauge;
    [SerializeField] float minimumGaugeToShoot;


    float _currentGauge;
    float CurrentGauge
    {
        get { return _currentGauge; }
        set { _currentGauge = Mathf.Clamp(value, 0, maxGauge); }
    }

    public override bool CanAction => (CurrentGauge >= minimumGaugeToShoot);

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, Screen.width * (CurrentGauge / maxGauge), Screen.height), "");
    }
    bool IsRolling => player.CurrentState == PlayerState.Roll;
    protected override void Start()
    {
        base.Start();
        CurrentGauge = 0;
    }
    public override void OnStartAction()
    {
        base.OnStartAction();
    }
    public override void OnUpdateAction()
    {
        if (CurrentGauge > 0)
        {
            CurrentGauge -= gaugeCostPerSecond * Time.deltaTime;
        }
        if (CurrentGauge == 0)
        {
            IsActionEnded = true;
        }
    }
    public override void OnUpdateNotAction()
    {
        var multiplier = IsRolling ? regenRollMuliplier : 1;
        CurrentGauge += gaugeRegen * multiplier * Time.deltaTime;
    }
    public override void OnEndAction()
    {
        base.OnEndAction();
    }

}
