using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Tamany : MonoBehaviour
{
    [SerializeField] Settings settings;
    CanvasScaler canvasScaler;

    void OnEnable()
    {
        //settings.UiSize_AddAndInvokeEvent(UpdateUI);
        settings.interfaceSize.Event_InvokeAndAdd(UpdateUI);
    }
    void UpdateUI(float size)
    {
        if (canvasScaler == null) canvasScaler = GetComponent<CanvasScaler>();

        canvasScaler.referenceResolution = new Vector2(1920 / size, 1080 / size);
    }
    void OnDisable()
    {
        //settings.UiSize_RemoveEvent(UpdateUI);
        settings.interfaceSize.Event_Remove(UpdateUI);
    }
}
