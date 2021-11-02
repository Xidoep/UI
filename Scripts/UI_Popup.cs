using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Popup : MonoBehaviour
{
    [SerializeField] internal Button acceptar;
    internal Selectable lastSelected;



    public void SetLastSelected(Selectable selectable)
    {
        lastSelected = selectable;
    }
}
