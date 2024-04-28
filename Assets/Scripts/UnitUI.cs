using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    [SerializeField] GameObject[] UIObjects;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image[] APCircles;
    [SerializeField] AbilityButton[] abilityButtons;
    [SerializeField] TextMeshProUGUI salvageText;

    private void Start()
    {
        HideUnitUI();
    }

    public void SetUnitUI(PlayerUnit unit)
    {
        foreach (GameObject go in UIObjects) 
        {
            go.SetActive(true);
        }

        nameText.text = unit.unitName;

        UpdateAPUI(unit);
        UpdateAbilityUI(unit);
        UpdateSalvageUI(unit);
    }

    public void UpdateAbilityUI(PlayerUnit unit)
    {
        for(int i = 0; i < abilityButtons.Length; i++)
        {
            if(unit.abilityProfiles.Count > i)
            {
                abilityButtons[i].gameObject.SetActive(true);
                abilityButtons[i].UpdateUI(unit);
            }
            else
            {
                abilityButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void UpdateAPUI(PlayerUnit unit)
    {
        UnitStats stats = unit.GetComponent<UnitStats>();
        for (int i = 0; i < APCircles.Length; i++)
        {
            APCircles[i].enabled = stats.currentAP > i;
        }
    }

    public void UpdateSalvageUI(PlayerUnit unit)
    {
        UnitStats stats = unit.GetComponent<UnitStats>();
        salvageText.text = "Salvage " + stats.salvage.ToString() + "/" + stats.carryCapacity.ToString();
    }

    public void HideUnitUI()
    {
        foreach (GameObject go in UIObjects)
        {
            go.SetActive(false);
        }
    }
}
