using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Xido Studio/Menu/SubMenu", fileName = "SubMenu")]
public class UI_Submenu : ScriptableObject
{
    [SerializeField] Object Scene;
    [SerializeField] new public string name;

    private void OnValidate()
    {
        name = Scene.name;
    }
}
