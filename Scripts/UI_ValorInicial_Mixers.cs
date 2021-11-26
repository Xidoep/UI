using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;


public class UI_ValorInicial_Mixers : UI_ValorInicial
{
    [SerializeField] AudioMixerGroup audioMixerGroup;
    [SerializeField] UnityEvent<float> passar;

    [SerializeField] bool boolitzar;

    [ContextMenu("Provar")]
    public override void Agafar()
    {
        audioMixerGroup.audioMixer.GetFloat($"Volum{audioMixerGroup.name}", out float _valor );
        passar.Invoke(!boolitzar ? (_valor / 80f) + 1 : Mathf.Round((_valor / 80f) + 1));
    }
}
