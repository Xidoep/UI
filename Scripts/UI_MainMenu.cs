using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] UI_Menu menu;
    private void Start()
    {
        menu.MainMenu();
    }
}
