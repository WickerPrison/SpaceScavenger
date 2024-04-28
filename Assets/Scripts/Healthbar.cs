using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> renderers = new List<SpriteRenderer>();
    UnitStats unitStats;

    private void Start()
    {
        unitStats = GetComponentInParent<UnitStats>();
        unitStats.OnTakeDamage += OnTakeDamage;
        for(int i = 0; i < renderers.Count; i++)
        {
            if(i >= unitStats.maxHealth)
            {
                renderers[i].enabled = false;
            }
        }

        UpdateHealthbar();
    }

    private void OnTakeDamage(object sender, System.EventArgs e)
    {
        UpdateHealthbar();
    }

    void UpdateHealthbar()
    {
        for (int i = 0; i < renderers.Count; i++)
        {
            if (i < unitStats.health)
            {
                renderers[i].color = Color.red;
            }
            else
            {
                renderers[i].color = Color.gray;
            }
        }
    }
}
