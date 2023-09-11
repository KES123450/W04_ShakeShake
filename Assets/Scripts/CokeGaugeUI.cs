using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CokeGaugeUI : MonoBehaviour
{
    [SerializeField] Image gaugeBar;

    public void SetGaugeValue(float value)
    {
        gaugeBar.fillAmount = value;
    }
}
