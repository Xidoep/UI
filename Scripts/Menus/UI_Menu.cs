using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using XS_Utils;

[CreateAssetMenu(menuName = "Xido Studio/Menu/Menu", fileName = "Menu")]
public class UI_Menu : ScriptableObject
{
    const string MENU_PAUSA = "Menu";
    const string MENU_CONFIGURACIO = "Configuracio";
    const string MENU_CONTROLS = "Controls";
    const string MENU_CREDITS = "Credits";
    const string MENU_SUBMENU = "Submenu";
    
    //The name of the actions maps to swap between.
    const string GAME_PLAY = "GamePlay";
    const string UI = "UI";

    UI_Submenu previous;
    UI_Submenu current;

    [SerializeField] Guardat guardat;
    [SerializeField] GameObject prefab_blurShader;
    [SerializeField] InputActionReference[] escoltadors;
    [SerializeField] UI_Submenu pausa;
    [SerializeField] UI_Submenu main;

    PlayerInput playerInput;
    AnimacioPerCodi blurShader;
    Coroutine amagarBlur;

    bool entrat = false;
    int index = 0;
    bool trobat = false;

    public UI_Submenu Previous => previous;

    [SerializeField] UnityEvent onPlay;
    [SerializeField] UnityEvent onPause;
    [SerializeField] UnityEvent onResume;
    [SerializeField] UnityEvent onToMainMenu;
    [SerializeField] UnityEvent onExitGame;

    void OnEnable()
    {
        Debugar.Log("[UI_Menu] OnEnable => RegistrarAccions()");
        RegistrarAccions();
    }

    public void RegistrarAccions()
    {
        current = null;
        entrat = false;

        for (int i = 0; i < escoltadors.Length; i++)
        {
            escoltadors[i].action.performed += Pause_ViaAction;
        }
        InputSystem.onDeviceChange += Pause_OnNoDevice;
    }

    void OnDisable()
    {
        Debugar.Log("[UI_Menu] OnDisable() => DesregistrarAccions()");
        DesregistrarAccions();
    }
    public void DesregistrarAccions()
    {
        for (int i = 0; i < escoltadors.Length; i++)
        {
            escoltadors[i].action.performed -= Pause_ViaAction;
        }
        InputSystem.onDeviceChange -= Pause_OnNoDevice;
    }

    void Pause_OnNoDevice(InputDevice inputDevice, InputDeviceChange change)
    {
        if(change == InputDeviceChange.Removed || change == InputDeviceChange.Disconnected)
            Pause();
    }
    void Pause_ViaAction(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed)
            return;

        Pause();
    }

    public void MainMenu()
    {
        EnterMenuMode();
        Switch(main);
        onToMainMenu.Invoke();
    }
    public void Pause()
    {
        EnterMenuMode();
        Switch(pausa);
        onPause.Invoke();
    }
    public void Resume()
    {
        ExitMenuMode();
        Close();
        onResume.Invoke();
    }


 

    public void Play()
    {
        ExitMenuMode();
        onPlay.Invoke();
        SceneManager.LoadScene("Game");
    }
    public void MenuPausaShow()
    {
        SceneManager.LoadSceneAsync(MENU_PAUSA, LoadSceneMode.Additive);
    }

    public void Switch(UI_Submenu submenu)
    {
        previous = current;

        if (!Application.isPlaying)
            return;

        if (current != null) SceneManager.UnloadSceneAsync(current.name);
        SceneManager.LoadSceneAsync(submenu.name, LoadSceneMode.Additive);
        current = submenu;
    }
    public void Close()
    {
        previous = current;
        SceneManager.UnloadSceneAsync(current.name);
        current = null;
    }
    public void ComeToPrevious()
    {
        if (current != null) SceneManager.UnloadSceneAsync(current.name);
        SceneManager.LoadSceneAsync(previous.name, LoadSceneMode.Additive);
        current = previous;
    }


    public void Suport() => Application.OpenURL("https://www.xidostudio.com/support");
    public void QuitGame() 
    {
        onExitGame.Invoke();
        //Application.wantsToQuit += Wait fore some secons to do fadeout to black animation.
        Application.Quit();
    } 

    public void ToMainMenu() 
    { 
        Close();
        SceneManager.LoadScene("Vestibul");
    } 

    void EnterMenuMode()
    {
        if (!Application.isPlaying)
            return;

        if (entrat)
            return;

        entrat = true;

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

        Time.timeScale = 0;
    }
    void ExitMenuMode()
    {
        if (!entrat)
            return;

        entrat = false;

        if (playerInput == null) playerInput = FindObjectOfType<PlayerInput>(true);
        if (playerInput) playerInput.SwitchCurrentActionMap(GAME_PLAY);
        if (guardat) guardat.Guardar();
        if (blurShader)
        {
            //blurShader.Play(1);
            amagarBlur = blurShader.gameObject.SetActive(false, 0.26f);
        }

        Time.timeScale = 1;
    }

    private void OnValidate()
    {
        guardat = XS_Utils.XS_Editor.LoadGuardat<Guardat>();
    }
}
