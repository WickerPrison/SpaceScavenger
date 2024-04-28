using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DragUnit : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] PlayerData playerData;
    [System.NonSerialized] public PlayerUnitSO unitSO;
    [System.NonSerialized] public int previousIndex;
    [System.NonSerialized] public BarracksUI barracksUI;
    [System.NonSerialized] public PointerEventData downEventData;
    RectTransform rectTransform;
    Vector2 posFloat;
    float xpos;
    float ypos;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        OnPointerDown(downEventData);
    }

    private void Update()
    {
        posFloat = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        xpos = Mathf.Lerp(0, 1920, posFloat.x);
        ypos = Mathf.Lerp(0, 1080, posFloat.y);
        rectTransform.position = new Vector2(xpos, ypos);
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("test");
        playerData.barracks.Insert(previousIndex, unitSO);
        barracksUI.UpdateBarracks();
        Destroy(gameObject);
    }
}
