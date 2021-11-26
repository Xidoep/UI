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

    [SerializeField] 

    public void Seleccionar(GameObject boto)
    {
        position = boto.transform.position;
        if(!moving)
            GoTo();
    }

    async void GoTo()
    {
        moving = true;
        while (!resaltador.position.IsNear(position, 0.1f))
        {
            resaltador.position = Vector3.Lerp(resaltador.position, position, 0.2f);
            await Task.Yield();
        }
        moving = false;
    }

}
