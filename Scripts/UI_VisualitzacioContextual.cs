using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Serveix per canviar els menus segons si es criden desde el menu inicial o desde el menu de pausa. Que s'enten que hi haurà opcions deferents segons el cas
/// </summary>
public class UI_VisualitzacioContextual : MonoBehaviour
{
    [SerializeField] UI_Menu menu;
    [Space(10)]
    [SerializeField] UI_Submenu previousScene;
    [SerializeField] UnityEvent OnMatch;



    void OnEnable()
    {
        if (menu.Previous == previousScene)
        {
            OnMatch.Invoke();
        }
    }

}
