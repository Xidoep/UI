using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using XS_Utils;

public class UI_Menu : MonoBehaviour
{
    const string GAME_PLAY = "GamePlay";
    const string UI = "UI";

    [SerializeField] PlayerInput playerInput;
    [SerializeField] Guardat guardat;
    [SerializeField] AnimacioPerCodi animacioBlur;
    [SerializeField] AnimacioPerCodi_All blur;
    [SerializeField] AnimacioPerCodi_Shader blurShader;
    [SerializeField] GameObject submenuInicial;

    WaitForSeconds waitForSeconds;

    Coroutine amagarBlur;

    private void OnEnable()
    {
        if (playerInput == null) FindObjectOfType<PlayerInput>(true);

        if (guardat) guardat.Carregar();
        if (playerInput) playerInput.SwitchCurrentActionMap(UI);
        if (animacioBlur) 
        {
            animacioBlur.gameObject.SetActive(true);
            animacioBlur.Play(0);
        }
        if (blur)
        {
            if(amagarBlur != null)
            {
                StopCoroutine(amagarBlur);
                amagarBlur = null;
            }
            blur.gameObject.SetActive(true);
            //blur.SetTemps(1);
            blur.Play(0);
        }
        if (blurShader)
        {
            if (amagarBlur != null)
            {
                StopCoroutine(amagarBlur);
                amagarBlur = null;
            }
            blurShader.gameObject.SetActive(true);
            //blur.SetTemps(1);
            blurShader.Play(0);
        }

        if (submenuInicial) submenuInicial.SetActive(true);

        Time.timeScale = 0;
    }
    private void OnDisable()
    {
        if (playerInput == null) FindObjectOfType<PlayerInput>(true);

        if (playerInput) playerInput.SwitchCurrentActionMap(GAME_PLAY);
        if (guardat) guardat.Guardar();
        if (animacioBlur) 
        {
            waitForSeconds = new WaitForSeconds(1);
            animacioBlur.Play(1);
            animacioBlur.gameObject.SetActive(false, 1);
        }
        if (blur)
        {
            //blur.SetTemps(0.25f);
            blur.Play(1);
            amagarBlur = blur.gameObject.SetActive(false, 0.26f);
        }
        if (blurShader)
        {
            //blur.SetTemps(0.25f);
            blurShader.Play(1);
            amagarBlur = blurShader.gameObject.SetActive(false, 0.26f);
        }

        Time.timeScale = 1;
    }

    public void Suport()
    {
        Application.OpenURL("https://www.xidostudio.com/support");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
