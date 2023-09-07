using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] ActionInfo currentAction;

    PlayerController player;

    float actionStartTime;

    public bool IsActionEnded { get; private set; }
    public ActionInfo CurrentAction => currentAction;

    bool IsActioning => player.CurrentState == PlayerState.Action;

    void Awake()
    {
        player = GetComponent<PlayerController>();
    }
    private void Start()
    {
        IsActionEnded = false;
        LoadAction();
    }
    private void Update()
    {
        if (IsActioning)
        {
            currentAction.OnUpdateAction();
            if (Time.time > actionStartTime + currentAction.ActionDuration)
            {
                IsActionEnded = true;
            }
        }
        else
        {
            currentAction.OnUpdateNotAction();
        }
    }
    public void StartAction()
    {
        if (currentAction == null)
        {
            Debug.LogWarning("Player has no action!");
            return;
        }
        actionStartTime = Time.time;
        currentAction.OnStartAction(player);
    }
    public void EndAction()
    {
        if (!IsActioning) { return; }
        IsActionEnded = false;
        currentAction.OnEndAction();
    }

    void LoadAction(ActionInfo currentAction = null)
    {
        if (currentAction == null) 
        { 
            currentAction = this.currentAction; 
        }
        else
        {
            this.currentAction = currentAction;
        }
    }

}
