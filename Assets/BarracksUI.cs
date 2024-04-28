using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarracksUI : MonoBehaviour
{
    [SerializeField] BarrackUnitUI[] units;
    [SerializeField] PlayerData playerData;

    private void Start()
    {
        UpdateBarracks();
    }

    public void UpdateBarracks()
    {
        for (int i = 0; i < units.Length; i++)
        {
            if (i < playerData.barracks.Count)
            {
                units[i].gameObject.SetActive(true);
                units[i].unitSO = playerData.barracks[i];
                units[i].UpdateUI();
            }
            else
            {
                units[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        TeamPrepEvents.instance.onUpdateUI += onUpdateUI;
    }

    private void OnDisable()
    {
        TeamPrepEvents.instance.onUpdateUI -= onUpdateUI;
    }

    private void onUpdateUI(object sender, System.EventArgs e)
    {
        UpdateBarracks();
    }
}
