using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionInfo : MonoBehaviour
{
    [Header("General")]
    [SerializeField] protected float speedMuptiplier;
    [SerializeField] protected bool canRollCancelAction;
    [SerializeField] protected bool canActionCancelRoll;

    protected PlayerController player;

    public float SpeedMultiplier => speedMuptiplier;
    public bool CanRollCancelAction => canRollCancelAction;
    public bool CanActionCancelRoll => canActionCancelRoll;
    public bool IsActionEnded { get; protected set; }

    public abstract bool CanAction { get; }

    protected virtual void Awake()
    {
        player = transform.parent.GetComponent<PlayerController>();
    }
    protected virtual void Start()
    {

    }

    public virtual void Initialize()
    {
    }
    public virtual void OnStartAction()
    {

    }
    public abstract void OnUpdateAction();
    public abstract void OnUpdateNotAction();
    public virtual void OnEndAction()
    {
        IsActionEnded = false;
    }
}
