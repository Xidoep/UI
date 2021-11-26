using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

public class UI_ValorInicial_MixersBool : UI_ValorInicial
{
    [SerializeField] AudioMixerGroup audioMixerGroup;

    [SerializeField] UnityEvent<bool> passar;

    [ContextMenu("Provar")]
    public override void Agafar()
    {
        audioMixerGroup.audioMixer.GetFloat($"Volum{audioMixerGroup.name}", out float _valor);
        passar.Invoke(_valor <= -75);
    }
}
