using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UI_BoolToString : MonoBehaviour
{
    [SerializeField] Slider slider;
    TMP_Text text;
    [SerializeField] LocalizedString True;
    [SerializeField] LocalizedString False;

    private void OnEnable()
    {
        text = GetComponent<TMP_Text>();
        slider.onValueChanged.AddListener(Visualize);
    }
    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(Visualize);
    }

    void Visualize(float valor)
    {
        if (Mathf.Round(slider.value) == 0)
            SetText(False);
        else SetText(True);
    }

    void SetText(LocalizedString String)
    {
        AsyncOperationHandle<string> op = String.GetLocalizedStringAsync();
        if (op.IsDone)
        {
            text.text = op.Result;
        }
        else
        {
            op.Completed += (o) => text.text = op.Result;
        }
    }
}
