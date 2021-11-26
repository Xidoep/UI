using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Tamany : MonoBehaviour
{
    [SerializeField] Settings settings;
    //[SerializeField] [Range(0.75f, 1.5f)] float size = 1;
    CanvasScaler canvasScaler;

    /*public void SetSize(float size)
    {
        this.size = size;
        UpdateUI();
        XS_Utils.Debugar.Log($"Size: {size}");
    }
    public float GetSize() => size;
    */
    void OnEnable()
    {
        settings.UiSize_AddAndInvokeEvent(UpdateUI);
    }
    void UpdateUI(float size)
    {
        if (canvasScaler == null) canvasScaler = GetComponent<CanvasScaler>();

        canvasScaler.referenceResolution = new Vector2(1920 / size, 1080 / size);
    }
    void OnDisable()
    {
        settings.UiSize_RemoveEvent(UpdateUI);
    }
}
