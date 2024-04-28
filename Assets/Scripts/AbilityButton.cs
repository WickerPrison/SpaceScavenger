using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    [SerializeField] int buttonNum;
    [SerializeField] Image iconImage;
    PlayerUnit currentUnit;

    public void UpdateUI(PlayerUnit unit)
    {
        currentUnit = unit;
        iconImage.sprite = unit.abilityProfiles[buttonNum - 1].abilityIcon;
    }

    public void UseAblity()
    {
        if(currentUnit != null)
        {
            currentUnit.UseAbility(buttonNum - 1);
        }
    }
}
