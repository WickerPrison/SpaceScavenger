using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class BarrackUnitUI : MonoBehaviour
{
    [SerializeField] int barracksIndex;
    [SerializeField] TextMeshProUGUI unitName;
    [SerializeField] TextMeshProUGUI maxHealth;
    [SerializeField] TextMeshProUGUI carryCapacity;
    [SerializeField] PlayerData playerData;
    [System.NonSerialized] public PlayerUnitSO unitSO;
    BarracksUI barracksUI;

    private void Start()
    {
        barracksUI = GetComponentInParent<BarracksUI>();
    }

    public void UpdateUI()
    {
        unitName.text = unitSO.unitName;
        maxHealth.text = "Max Health: " + unitSO.maxHealth.ToString();
        carryCapacity.text = "Carry Capacity: " + unitSO.carryCapacity.ToString();
    }

    public void AddToSquad()
    {
        if (playerData.squad.Count > 3) return;

        playerData.barracks.Remove(unitSO);
        playerData.squad.Add(unitSO);
        TeamPrepEvents.instance.OnUpdateUI();
    }
}
