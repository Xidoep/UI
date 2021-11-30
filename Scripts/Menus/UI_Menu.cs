using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using XS_Utils;

[CreateAssetMenu(menuName = "Xido Studio/Menu", fileName = "Menu")]
public class UI_Menu : ScriptableObject
{
    const string GAME_PLAY = "GamePlay";
    const string UI = "UI";

    [SerializeField] Guardat guardat;
    [SerializeField] GameObject prefab_blurShader;

    PlayerInput playerInput;
    AnimacioPerCodi blurShader;
    Coroutine amagarBlur;

    bool mostrat = false;

    private void OnEnable()
    {
        mostrat = false;
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



    public void MenuPausaShow()
    {
        SceneManager.LoadSceneAsync("MenuPausa", LoadSceneMode.Additive);
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

        SceneManager.UnloadSceneAsync("MenuPausa");
        Time.timeScale = 1;
    }

    public void CreditsShow()
    {
        SceneManager.LoadSceneAsync("Credits", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("MenuPausa");
    }
    public void CreditsHide()
    {
        SceneManager.UnloadSceneAsync("Credits");
        MenuPausaShow();
    }

    public void ControlsShow()
    {
        SceneManager.LoadSceneAsync("Controls", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("MenuPausa");
    }
    public void ControlsHide()
    {
        SceneManager.UnloadSceneAsync("Controls");
        MenuPausaShow();
    }

    public void ConfiguracioShow()
    {
        SceneManager.LoadSceneAsync("Configuracio", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("MenuPausa");
    }
    public void ConfiguracioHide()
    {
        SceneManager.UnloadSceneAsync("Configuracio");
        MenuPausaShow();
    }



}
