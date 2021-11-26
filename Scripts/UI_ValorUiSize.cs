using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI_ValorUiSize : MonoBehaviour
{
    [SerializeField] Settings settings;

    [SerializeField] UnityEvent<float> passar;

    private void OnEnable()
    {
        settings.UiSize_AddAndInvokeEvent(passar.Invoke);
    }

    private void OnDisable()
    {
        settings.UiSize_RemoveEvent(passar.Invoke);
    }
}
