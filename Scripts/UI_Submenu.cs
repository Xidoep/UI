using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

public class UI_Submenu : MonoBehaviour
{
    [SerializeField] RectTransform resaltador;
    Vector3 position;
    bool moving;

    [SerializeField] Submenu[] submenus;
    int actual;

    private void OnEnable()
    {
        MostrarSubmenu(0);
    }

    public void Seleccionar(GameObject boto)
    {
        position = boto.transform.position;
        if(!moving)
            GoTo();
    }

    async void GoTo()
    {
        moving = true;
        while (!resaltador.position.IsNear(position, 0.2f))
        {
            resaltador.position = Vector3.Lerp(resaltador.position, position, 0.4f);
            await Task.Yield();
        }
        moving = false;
    }

    public void MostrarSubmenu(int index)
    {
        actual = index;
        for (int i = 0; i < submenus.Length; i++)
        {
            if (i == index) submenus[i].menu.SetActive(true);
            else
            {
                if (submenus[i].menu.activeSelf) submenus[i].disableTempsAnimacio.Disable();
            }
        }
    }

    public void Augmentar()
    {
        if (actual >= submenus.Length - 1)
            return;

        actual++;
        Seleccionar(submenus[actual].boto);
        MostrarSubmenu(actual);
    }
    public void Disminuir()
    {
        if (actual <= 0)
            return;

        actual--;
        Seleccionar(submenus[actual].boto);
        MostrarSubmenu(actual);
    }

    [System.Serializable]
    public class Submenu
    {
        public GameObject boto;
        public GameObject menu;
        public Utils_DisableTempsAnimacio disableTempsAnimacio;
    }
}
