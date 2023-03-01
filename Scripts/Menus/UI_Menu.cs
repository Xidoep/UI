using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using UnityEngine.SceneManagement;
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

    [SerializeField] Guardat guardat;
    //[SerializeField] GameObject prefab_blurShader;
    [SerializeField] AccesToMenu[] accessos;
    //[SerializeField] InputActionReference[] escoltadors;
    //[SerializeField] UI_Submenu pausa;
    //[SerializeField] UI_Submenu main;
    [SerializeField] Utils_InstantiableFromProject pausa;
    [SerializeField] GameObject[] menus;

    [SerializeField] GameObject previous;
    [SerializeField] GameObject current;

    PlayerInput playerInput;
    //Coroutine amagarBlur;
    //AnimacioPerCodi blurShader;

    /*bool entrat = false;
    int index = 0;
    bool trobat = false;
    */

    
    public GameObject Previous => previous;

    [SerializeField] UnityEvent onPause;
    [SerializeField] UnityEvent onResume;
    [SerializeField] UnityEvent onToMainMenu;

    [System.Serializable]
    public struct AccesToMenu
    {
        [SerializeField] InputActionReference[] escoltadors;

        [SerializeField] Utils_InstantiableFromProject menu;
        Action<Utils_InstantiableFromProject> pausa;

        public void RegistrarAccions(Action<Utils_InstantiableFromProject> pausa)
        {
            //current = null;
            //entrat = false;
            for (int i = 0; i < escoltadors.Length; i++)
            {
                escoltadors[i].action.performed += Pause_ViaAction;
            }
            this.pausa += pausa;
        }
        public void DesregistrarAccions(Action<Utils_InstantiableFromProject> pausa)
        {
            for (int i = 0; i < escoltadors.Length; i++)
            {
                escoltadors[i].action.performed -= Pause_ViaAction;
            }
            this.pausa -= pausa;
        }
        void Pause_ViaAction(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed)
                return;

            pausa?.Invoke(menu);
        }
    }
    void OnEnable()
    {
        Debugar.Log("[UI_Menu] OnEnable => RegistrarAccions()");
        RegistrarAccions();
    }
    void OnDisable()
    {
        Debugar.Log("[UI_Menu] OnDisable() => DesregistrarAccions()");
        DesregistrarAccions();
        current = null;
        previous = null;
    }

    public void RegistrarAccions()
    {
        current = null;
        //entrat = false;

        for (int i = 0; i < accessos.Length; i++)
        {
            accessos[i].RegistrarAccions(Pause);
        }
        InputSystem.onDeviceChange += Pause_OnNoDevice;
    }


    public void DesregistrarAccions()
    {
        for (int i = 0; i < accessos.Length; i++)
        {
            accessos[i].RegistrarAccions(Pause);
        }
        InputSystem.onDeviceChange -= Pause_OnNoDevice;
    }

    void Pause_OnNoDevice(InputDevice inputDevice, InputDeviceChange change)
    {
        if(change == InputDeviceChange.Removed || change == InputDeviceChange.Disconnected)
            Pause();
    }

    public void Pause(Utils_InstantiableFromProject menu)
    {
        if (current != null)
            return;

        EnterMenuMode();
        Switch(menu);
        onPause.Invoke();
    }
    public void Pause()
    {
        if (current != null)
            return;

        EnterMenuMode();
        Switch(pausa);
        onPause.Invoke();
    }
    public void Resume()
    {
        Close();
        ExitMenuMode();
        onResume.Invoke();
    }


    GameObject ToPrevious(GameObject current)
    {
        if (current == null)
            return null;

        GameObject trobat = null;
        for (int i = 0; i < menus.Length; i++)
        {
            if (current.name.StartsWith(menus[i].name))
            {
                trobat = menus[i];
                break;
            }
        }
        return trobat;
    }
    public void Switch(Utils_InstantiableFromProject submenu)
    {
        previous = ToPrevious(current);

        if (!Application.isPlaying)
            return;

        if (current != null) 
        {
            if (current.TryGetComponent(out AnimacioPerCodi_GameObject_Referencia animacio))
                animacio.Destroy();
            else
                Destroy(current.gameObject);
        }

        current = submenu.InstantiateReturn();
    }
    public void Close()
    {
        if (current.TryGetComponent(out AnimacioPerCodi_GameObject_Referencia animacio))
            animacio.Destroy();
        else
            Destroy(current.gameObject);

        current = null;
    }
    public void ComeToPrevious()
    {
        if(current != null)
        {
            if (current.TryGetComponent(out AnimacioPerCodi_GameObject_Referencia animacio))
                animacio.Destroy();
            else
                Destroy(current.gameObject);
        }
        current = previous.GetComponent<Utils_InstantiableFromProject>().InstantiateReturn();
    }



    public void Suport() => Application.OpenURL("https://www.xidostudioScom/support");
    /*public void QuitGame() 
    {
        //onExitGame.Invoke();

        //Application.wantsToQuit += Wait fore some secons to do fadeout to black animation.

        Application.Quit();
    } */

    /*public void ToMainMenu() 
    { 
        Close();
        //SceneManager.LoadScene("Vestibul");
    } */

    void EnterMenuMode()
    {
        if (!Application.isPlaying)
            return;

        if (current != null)
            return;

        if (playerInput == null) playerInput = FindObjectOfType<PlayerInput>(true);
        //if (guardat) guardat.Carregar();
        if (playerInput) playerInput.SwitchCurrentActionMap(UI);

        Time.timeScale = 0;
    }
    void ExitMenuMode()
    {
        if (playerInput == null) playerInput = FindObjectOfType<PlayerInput>(true);
        if (playerInput) playerInput.SwitchCurrentActionMap(GAME_PLAY);
        if (guardat) guardat.Guardar();

        Time.timeScale = 1;
    }

    private void OnValidate()
    {
        guardat = XS_Editor.LoadGuardat<Guardat>();
    }
}
