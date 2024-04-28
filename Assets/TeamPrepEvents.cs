using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamPrepEvents : MonoBehaviour
{
    public static TeamPrepEvents instance { get; private set; }

    public event EventHandler onUpdateUI;

    private void Awake()
    {
        instance = this;
    }

    public void OnUpdateUI()
    {
        onUpdateUI?.Invoke(this, EventArgs.Empty);
    }
}
