using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAction", menuName = "ScriptableObjects/PlayerActionPreset", order = 1)]
public class PlayerActionPreset : ScriptableObject
{
    public bool CanRollCancelAction;
    public bool CanActionCancelRoll;
}
