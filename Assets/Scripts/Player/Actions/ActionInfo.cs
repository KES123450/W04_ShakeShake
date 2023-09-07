using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionInfo : MonoBehaviour
{
    [SerializeField] protected float speedMuptiplier;
    [SerializeField] protected float actionDuration;
    [SerializeField] protected bool canRollCancelAction;
    [SerializeField] protected bool canActionCancelRoll;

    PlayerController player;

    public float SpeedMultiplier => speedMuptiplier;
    public float ActionDuration => actionDuration;
    public bool CanRollCancelAction => canRollCancelAction;
    public bool CanActionCancelRoll => canActionCancelRoll;

    public virtual void OnStartAction(PlayerController playerController)
    {
        player = playerController;
    }
    public virtual void OnUpdateAction()
    {

    }
    public virtual void OnUpdateNotAction()
    {

    }
    public virtual void OnEndAction()
    {

    }
}
