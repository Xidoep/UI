using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XS_Utils;

public class UI_GrupsResaltador : MonoBehaviour
{
    bool moving = false;
    Coroutine coroutine;

    public void Resaltar(Transform transform)
    {
        if (moving)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(GoTo(transform.position));
    }

    IEnumerator GoTo(Vector3 position)
    {
        moving = true;
        while(XS_Transform.Distance(position,transform.position,true) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, position, 10f * Time.unscaledDeltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        moving = false;
    }
}
