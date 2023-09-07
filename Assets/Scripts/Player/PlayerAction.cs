using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] float actionDuration;
    [SerializeField] PlayerActionPreset preset;

    PlayerController player;

    float actionStartTime;

    public bool IsActionEnded { get; private set; }
    public bool CanRollCancelAction { get; private set; }
    public bool CanActionCancelRoll { get; private set; }
    bool IsActioning { get { return player.CurrentState == PlayerState.Action; } }

    void Awake()
    {
        player = GetComponent<PlayerController>();
    }
    private void Start()
    {
        IsActionEnded = false;
        LoadPreset();
    }
    private void Update()
    {
        if (IsActioning)
        {
            if (Time.time > actionStartTime + actionDuration)
            {
                IsActionEnded = true;
            }
        }
    }
    public void StartAction()
    {
        actionStartTime = Time.time;
    }
    public void EndAction()
    {
        if (!IsActioning) { return; }
        IsActionEnded = false;
    }

    void LoadPreset(PlayerActionPreset preset = null)
    {
        if (preset == null) { preset = this.preset; }

        CanRollCancelAction = preset.CanRollCancelAction;
        CanActionCancelRoll = preset.CanActionCancelRoll;
    }

}
