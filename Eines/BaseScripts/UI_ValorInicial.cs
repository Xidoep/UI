using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_ValorInicial : MonoBehaviour
{
    private void OnEnable() => Agafar();
    public abstract void Agafar();
}
