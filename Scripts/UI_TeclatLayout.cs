using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class UI_TeclatLayout : MonoBehaviour
{
    enum Layout
    {
        QWERTY,
        QWERTZ,
        AZERTY,
        QZERTY,
        JCUKEN //Rus
    }
    [SerializeField] Layout layout;

    /*[SerializeField] Transform A;
    [SerializeField] Transform M;
    [SerializeField] Transform Q;
    [SerializeField] Transform W;
    [SerializeField] Transform Y;
    [SerializeField] Transform Z;
    */

    [System.Serializable]
    public class Tecla
    {
        [SerializeField] RectTransform transform;
        Vector3 posicio;
        Transform parent;

        public void Inicial()
        {
            posicio = transform.anchoredPosition;
            parent = transform.parent;
        }
        public void Canviar(Tecla objectiu)
        {
            transform.SetParent(objectiu.parent);
            transform.anchoredPosition = objectiu.posicio;
        }
    }
    [SerializeField] Tecla A;
    [SerializeField] Tecla M;
    [SerializeField] Tecla Q;
    [SerializeField] Tecla W;
    [SerializeField] Tecla Y;
    [SerializeField] Tecla Z;
    [SerializeField] Tecla Ñ;

    UI_Tecla[] tecles;
    UI_Tecla[] Tecles
    {
        get
        {
            if (tecles.Length == 0) tecles = GetComponentsInChildren<UI_Tecla>();
            return tecles;
        }
    }
    [SerializeField] UI_Tecla accentE, suma, enya, accentD, cTrencada, coma, punt;
    [SerializeField] TMP_FontAsset normal, rus;
    private void OnEnable()
    {
        tecles = GetComponentsInChildren<UI_Tecla>();

        A.Inicial();
        M.Inicial();
        Q.Inicial();
        W.Inicial();
        Y.Inicial();
        Z.Inicial();
        Ñ.Inicial();

       
    }

    [ContextMenu("Prova")]
    void Provar()
    {
        Actualitzar();
    }

    public void Actualitzar()
    {
        switch (layout)
        {
            case Layout.QWERTY:
                A.Canviar(A);
                M.Canviar(M);
                Q.Canviar(Q);
                W.Canviar(W);
                Y.Canviar(Y);
                Z.Canviar(Z);
                Ñ.Canviar(Ñ);
                foreach (var item in Tecles)
                {
                    if (item.text == null)
                        continue;

                    if (item.Text.font == rus) 
                    {
                        item.Text.font = normal;
                        Debug.Log(item.Text.text);
                        item.Actualitzar();
                    } 
                }
                break;
            case Layout.QWERTZ:
                A.Canviar(A);
                M.Canviar(M);
                Q.Canviar(Q);
                W.Canviar(W);
                Y.Canviar(Z);
                Z.Canviar(Y);
                Ñ.Canviar(Ñ);
                foreach (var item in Tecles)
                {
                    if (item.text == null)
                        continue;

                    if (item.Text.font == rus)
                    {
                        item.Text.font = normal;
                        item.Actualitzar();
                    }
                }
                break;
            case Layout.AZERTY:
                A.Canviar(Q);
                M.Canviar(Ñ);
                Q.Canviar(A);
                W.Canviar(Z);
                Y.Canviar(Y);
                Z.Canviar(W);
                Ñ.Canviar(M);
                foreach (var item in Tecles)
                {
                    if (item.text == null)
                        continue;

                    if (item.Text.font == rus)
                    {
                        item.Text.font = normal;
                        item.Actualitzar();
                    }
                }
                break;
            case Layout.QZERTY:
                A.Canviar(A);
                M.Canviar(Ñ);
                Q.Canviar(Q);
                W.Canviar(Z);
                Y.Canviar(Y);
                Z.Canviar(W);
                Ñ.Canviar(M);

                foreach (var item in Tecles)
                {
                    if (item.text == null)
                        continue;

                    if (item.Text.font == rus)
                    {
                        item.Text.font = normal;
                        item.Actualitzar();
                    }
                }
                break;
            case Layout.JCUKEN:
                A.Canviar(A);
                M.Canviar(M);
                Q.Canviar(Q);
                W.Canviar(W);
                Y.Canviar(Y);
                Z.Canviar(Z);
                Ñ.Canviar(Ñ);
                foreach (var item in Tecles)
                {
                    if (item.text == null)
                        continue;

                    switch (item.Tecla)
                    {
                        case Key.Q:
                            item.Text.font = rus;
                            item.Text.text = "Й";
                            break;
                        case Key.W:
                            item.Text.font = rus;
                            item.Text.text = "Ц";
                            break;
                        case Key.E:
                            item.Text.font = rus;
                            item.Text.text = "У";
                            break;
                        case Key.R:
                            item.Text.font = rus;
                            item.Text.text = "К";
                            break;
                        case Key.T:
                            item.Text.font = rus;
                            item.Text.text = "Е";
                            break;
                        case Key.Y:
                            item.Text.font = rus;
                            item.Text.text = "Н";
                            break;
                        case Key.U:
                            item.Text.font = rus;
                            item.Text.text = "Г";
                            break;
                        case Key.I:
                            item.Text.font = rus;
                            item.Text.text = "Ш";
                            break;
                        case Key.O:
                            item.Text.font = rus;
                            item.Text.text = "Щ";
                            break;
                        case Key.P:
                            item.Text.font = rus;
                            item.Text.text = "З";
                            break;

                        case Key.A:
                            item.Text.font = rus;
                            item.Text.text = "Ф";
                            break;
                        case Key.S:
                            item.Text.font = rus;
                            item.Text.text = "Ы";
                            break;
                        case Key.D:
                            item.Text.font = rus;
                            item.Text.text = "В";
                            break;
                        case Key.F:
                            item.Text.font = rus;
                            item.Text.text = "А";
                            break;
                        case Key.G:
                            item.Text.font = rus;
                            item.Text.text = "П";
                            break;
                        case Key.H:
                            item.Text.font = rus;
                            item.Text.text = "Р";
                            break;
                        case Key.J:
                            item.Text.font = rus;
                            item.Text.text = "О";
                            break;
                        case Key.K:
                            item.Text.font = rus;
                            item.Text.text = "Л";
                            break;
                        case Key.L:
                            item.Text.font = rus;
                            item.Text.text = "Д";
                            break;

                        case Key.Z:
                            item.Text.font = rus;
                            item.Text.text = "Я";
                            break;
                        case Key.X:
                            item.Text.font = rus;
                            item.Text.text = "Ч";
                            break;
                        case Key.C:
                            item.Text.font = rus;
                            item.Text.text = "С";
                            break;
                        case Key.V:
                            item.Text.font = rus;
                            item.Text.text = "М";
                            break;
                        case Key.B:
                            item.Text.font = rus;
                            item.Text.text = "И";
                            break;
                        case Key.N:
                            item.Text.font = rus;
                            item.Text.text = "Т";
                            break;
                        case Key.M:
                            item.Text.font = rus;
                            item.Text.text = "Ь";
                            break;
                    }
                }
                break;


        }
    }
}