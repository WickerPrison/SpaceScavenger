using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamPrepSlot : MonoBehaviour
{
    [SerializeField] int slotIndex;
    [SerializeField] PlayerData playerData;
    TeamPrepUnitUI teamPrepUnit;

    private void Start()
    {
        teamPrepUnit = GetComponentInChildren<TeamPrepUnitUI>();

        if (playerData.squad.Count <= slotIndex)
        {
            teamPrepUnit.gameObject.SetActive(false);
        }
        else
        {
            teamPrepUnit.unitSO = playerData.squad[slotIndex];
            teamPrepUnit.UpdateUI();
        }
    }


}
