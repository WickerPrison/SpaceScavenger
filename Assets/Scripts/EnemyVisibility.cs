using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisibility : MonoBehaviour
{
    [SerializeField] GameSettings gameSettings;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.maskInteraction = gameSettings.visibilitySetting;
    }

    // Update is called once per frame
    void Update()
    {
        if(spriteRenderer.maskInteraction != gameSettings.visibilitySetting)
        {
            spriteRenderer.maskInteraction = gameSettings.visibilitySetting;
        }
    }
}
