using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

public class UI_Grups : MonoBehaviour
{
    [SerializeField] Utils_DisableTempsAnimacio[] grups;

    private void OnEnable() => MostrarSubmenu(0);


    public void MostrarSubmenu(int index)
    {
        for (int i = 0; i < grups.Length; i++)
        {
            if (i == index) grups[i].gameObject.SetActive(true);
            else
            {
                if (grups[i].gameObject.activeSelf) grups[i].Disable();
            }
        }
    }

}
