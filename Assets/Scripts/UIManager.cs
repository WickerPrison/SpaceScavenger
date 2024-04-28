using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [SerializeField] UnitUI unitUI;
    [System.NonSerialized] public bool mouseOverUI = false;

    public static UIManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        mouseOverUI = EventSystem.current.IsPointerOverGameObject();
    }

    public void SetUIUnit(PlayerUnit unit)
    {
        if (unitUI == null) return;
        unitUI.SetUnitUI(unit);
    }

    public void HideUnitUI()
    {
        if (unitUI == null) return;
        unitUI.HideUnitUI();
    }

    public void UpdateAPUI(PlayerUnit unit)
    {
        if (unitUI == null) return;
        unitUI.UpdateAPUI(unit);
    }

    public void UpdateSalvageUI(PlayerUnit unit)
    {
        if(unitUI == null) return;
        unitUI.UpdateSalvageUI(unit);
    }
}
