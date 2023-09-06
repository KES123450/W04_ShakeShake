using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] float actionDuration;

    PlayerController player;

    float actionStartTime;

    public bool IsActionEnded { get; private set; }
    bool IsActioning { get { return player.CurrentState == PlayerState.Action; } }

    void Awake()
    {
        player = GetComponent<PlayerController>();
    }
    private void Start()
    {
        IsActionEnded = false;
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
}
