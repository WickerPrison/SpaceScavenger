using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamPrepUnitUI : MonoBehaviour
{
    [System.NonSerialized] public PlayerUnitSO unitSO;
    [SerializeField] List<Image> healthPoints = new List<Image>();
    [SerializeField] TextMeshProUGUI carryCapacity;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject[] hideWhenEmpty;
    [SerializeField] PlayerData playerData;

    public void UpdateUI()
    {
        if (unitSO == null)
        {
            NoUnit();
            return;
        }

        ShowObjects(true);
        UpdateHealthbar();
        UpdateStats();
    }

    void UpdateHealthbar()
    {
        for (int i = 0; i < healthPoints.Count; i++)
        {
            if (i < unitSO.maxHealth)
            {
                healthPoints[i].enabled = true;
            }
            else
            {
                healthPoints[i].enabled = false;
            }
        }
    }

    void UpdateStats()
    {
        nameText.text = unitSO.unitName;
        carryCapacity.text = "Carry Capacity: " + unitSO.carryCapacity.ToString();
    }

    void NoUnit()
    {
        for (int i = 0; i < healthPoints.Count; i++)
        {
            healthPoints[i].enabled = false;  
        }

        nameText.text = "";
        carryCapacity.text = "";

        ShowObjects(false);
    }

    void ShowObjects(bool show)
    {
        foreach (GameObject hide in hideWhenEmpty)
        {
            hide.SetActive(show);
        }
    }

    public void RemoveFromSquad()
    {
        playerData.squad.Remove(unitSO);
        playerData.barracks.Add(unitSO);
        TeamPrepEvents.instance.OnUpdateUI();
    }
}
