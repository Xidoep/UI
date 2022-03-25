using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI_GuardatToUI<T> : MonoBehaviour
{
    [SerializeField] protected SavableVariable<T> variable;
    [SerializeField] UnityEvent<T> onEnable;

    private void OnEnable()
    {
        onEnable.Invoke(variable.Valor);
    }
}
