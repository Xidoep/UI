using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Bindable : MonoBehaviour
{
    [SerializeField] protected AnimacioPerCodi_GameObject animacio;

    protected bool utilitzada = false;
    protected Coroutine coroutine;
}
