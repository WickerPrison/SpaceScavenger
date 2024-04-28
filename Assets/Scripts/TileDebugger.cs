using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileDebugger : MonoBehaviour
{
    [SerializeField] SpriteRenderer debuggingSquare;

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] bool showOccupant;
    TileScript tileScript;

    // Start is called before the first frame update
    void Start()
    {
        tileScript = GetComponent<TileScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(showOccupant)
        text.text = tileScript.occupant.ToString();
    }

    public void SetDebuggingColor(Color color)
    {
        debuggingSquare.color = color;
    }
}
