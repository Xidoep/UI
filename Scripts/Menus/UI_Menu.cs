using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using XS_Utils;

[CreateAssetMenu(menuName = "Xido Studio/Menu", fileName = "Menu")]
public class UI_Menu : ScriptableObject
{
    const string MENU_PAUSA = "MenuPausa";
    const string MENU_CONFIGURACIO = "Configuracio";
    const string MENU_CONTROLS = "Controls";
    const string MENU_CREDITS = "Credits";

    const string GAME_PLAY = "GamePlay";
    const string UI = "UI";

    [SerializeField] Guardat guardat;
    [SerializeField] GameObject prefab_blurShader;
    [SerializeField] Action onPlay;
    [SerializeField] InputActionReference[] escoltadors;

    PlayerInput playerInput;
    AnimacioPerCodi blurShader;
    Coroutine amagarBlur;

    bool mostrat = false;

    private void OnEnable()
    {
        mostrat = false;
        Debugar.Log("Registrar accions per mostrar el menu");
        for (int i = 0; i < escoltadors.Length; i++)
        {
            escoltadors[i].action.performed += MostrarViaAction;
        }
        InputSystem.onDeviceChange += MostrarPerqueNoDevice;
    }

    private void OnDisable()
    {
        Debugar.Log("Desregistrar accions per mostrar el menu");
        for (int i = 0; i < escoltadors.Length; i++)
        {
            escoltadors[i].action.performed -= MostrarViaAction;
        }
        InputSystem.onDeviceChange -= MostrarPerqueNoDevice;
    }

    void MostrarPerqueNoDevice(InputDevice inputDevice, InputDeviceChange change)
    {
        if(change == InputDeviceChange.Removed || change == InputDeviceChange.Disconnected)
            Mostrar();
    }
    void MostrarViaAction(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed)
            return;
            
        Mostrar();
    }

    public void Mostrar()
    {
        if (mostrat)
            return;

        mostrat = true;

        if (playerInput == null) playerInput = FindObjectOfType<PlayerInput>(true);
        if (guardat) guardat.Carregar();
        if (playerInput) playerInput.SwitchCurrentActionMap(UI);

        if (blurShader == null) blurShader = Instantiate(prefab_blurShader, Camera.main.transform).GetComponent<AnimacioPerCodi>();
        if (blurShader)
        {
            if (amagarBlur != null)
            {
                //StopCoroutine(amagarBlur);
                amagarBlur = null;
            }
            blurShader.gameObject.SetActive(true);
            //blurShader.Play(0);
        }

        MenuPausaShow();
        Time.timeScale = 0;
    }


    /*private void OnEnable()
    {
        if (playerInput == null) FindObjectOfType<PlayerInput>(true);

        if (guardat) guardat.Carregar();
        if (playerInput) playerInput.SwitchCurrentActionMap(UI);

        if (blurShader)
        {
            if (amagarBlur != null)
            {
                StopCoroutine(amagarBlur);
                amagarBlur = null;
            }
            blurShader.gameObject.SetActive(true);
            blurShader.Play(0);
        }

        MenuPausaShow();
        Time.timeScale = 0;
    }
    private void OnDisable()
    {
        if (playerInput == null) FindObjectOfType<PlayerInput>(true);

        if (playerInput) playerInput.SwitchCurrentActionMap(GAME_PLAY);
        if (guardat) guardat.Guardar();

        if (blurShader)
        {
            blurShader.Play(1);
            amagarBlur = blurShader.gameObject.SetActive(false, 0.26f);
        }

        Time.timeScale = 1;
    }*/

    public void Suport()
    {
        Application.OpenURL("https://www.xidostudio.com/support");
    }
    public void QuitGame()
    {
        Application.Quit();
    }


    bool carregantMenu;
    Action carregar;


    public void Play()
    {
        onPlay?.Invoke();
    }
    public void MenuPausaShow()
    {
        SceneManager.LoadSceneAsync(MENU_PAUSA, LoadSceneMode.Additive);
        carregar = null;
        carregantMenu = false;
    }
    public void MenuPausaHide()
    {
        if (!mostrat)
            return;

        mostrat = false;

        if (playerInput == null) playerInput = FindObjectOfType<PlayerInput>(true);
        if (playerInput) playerInput.SwitchCurrentActionMap(GAME_PLAY);
        if (guardat) guardat.Guardar();
        if (blurShader)
        {
            //blurShader.Play(1);
            amagarBlur = blurShader.gameObject.SetActive(false, 0.26f);
        }

        SceneManager.UnloadSceneAsync(MENU_PAUSA);
        Time.timeScale = 1;
    }

    public void CreditsShow()
    { 
        SceneManager.LoadSceneAsync(MENU_CREDITS, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(MENU_PAUSA);
    }
    public void CreditsHide()
    {
        SceneManager.UnloadSceneAsync(MENU_CREDITS);
        MenuPausaShow();
    }

    public void ControlsShow()
    {
        SceneManager.LoadSceneAsync(MENU_CONTROLS, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(MENU_PAUSA);
    }
    public void ControlsHide()
    {
        SceneManager.UnloadSceneAsync(MENU_CONTROLS);
        MenuPausaShow();
    }

    public void ConfiguracioShow()
    {
        SceneManager.LoadSceneAsync(MENU_CONFIGURACIO, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(MENU_PAUSA);
    }
    public void ConfiguracioHide()
    {
        SceneManager.UnloadSceneAsync(MENU_CONFIGURACIO);
        MenuPausaShow();
    }



}
