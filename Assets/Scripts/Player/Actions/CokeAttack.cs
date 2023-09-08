using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CokeAttack : ActionInfo
{
    [Header("Coke Gauge")]
    [SerializeField] float gaugeRegen;
    [SerializeField] float gaugeLossOnStop;
    [SerializeField] float regenRollMuliplier;
    [SerializeField] float gaugeCostPerSecond;
    [SerializeField] float maxGauge;
    [SerializeField] float minimumGaugeToShoot;
    [Header("Coke Geyser")]
    [SerializeField] float maxLength;
    [SerializeField] float maxAngularSpeed;
    [SerializeField] LayerMask targetLayer;

    CokeGeyser geyser;

    float _currentGauge;
    float geyserStartGauge;

    PlayerMove playerMove => player.MoveComponent;
    public override bool CanAction => (CurrentGauge >= minimumGaugeToShoot);
    float CurrentGauge
    {
        get { return _currentGauge; }
        set { _currentGauge = Mathf.Clamp(value, 0, maxGauge); }
    }

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, Screen.width * (CurrentGauge / maxGauge), Screen.height / 10), "");
    }
    bool IsRolling => player.CurrentState == PlayerState.Roll;

    protected override void Awake()
    {
        base.Awake();
        geyser = GetComponentInChildren<CokeGeyser>();
    }
    protected override void Start()
    {
        base.Start();
        geyser.SetActive(false);
        CurrentGauge = 0;
    }
    public override void OnStartAction()
    {
        base.OnStartAction();
        geyserStartGauge = CurrentGauge;
        geyser.StartGeyser(targetLayer, transform.position, aimDirection, maxAngularSpeed);
    }
    public override void OnUpdateAction()
    {
        if (CurrentGauge > 0)
        {
            CurrentGauge -= gaugeCostPerSecond * Time.deltaTime;
            var usedGauge = geyserStartGauge - CurrentGauge;
            var length = maxLength * usedGauge / maxGauge;
            geyser.UpdatePoint(transform.position, aimDirection * length);
        }
        if (CurrentGauge == 0)
        {
            IsActionEnded = true;
        }
    }
    public override void OnUpdateNotAction()
    {
        if (playerMove.IsMoving)
        {
            var multiplier = (IsRolling ? regenRollMuliplier : 1) * playerMove.SpeedModifier;
            CurrentGauge += gaugeRegen * multiplier * Time.deltaTime;
        }
        else
        {
            CurrentGauge -= gaugeLossOnStop * Time.deltaTime;
        }
    }
    public override void OnEndAction()
    {
        base.OnEndAction();
        geyser.EndGeyser();
    }

}
