using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Canvas canvas;

    public Button DefaultButton;
    public EventSystem EventSystem;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        EventSystem = GetComponent<EventSystem>();

        GameManager.OnPauseStateChanged += PauseStateChange;
    }

    public void PlayGame()
    {
        // if player has just loaded in, use the canvas to start the game, otherwise use pause behavior
        if (GameManager.State.PlayerState == PlayerState.Start)
        {
            Debug.Log("Starting game...");
            GameManager.Instance.UpdatePlayerState(PlayerState.Idle);
            canvas.enabled = false;
        }
        else
        {
            GameManager.Instance.TogglePause();
        }

    }

    public void ShowHowToPlay()
    {
        Debug.Log("ShowHowToPlay()");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void PauseStateChange(bool isPaused)
    {
        // remove lingering selections
        EventSystem.current.SetSelectedGameObject(null);


        if (isPaused)
        {
            canvas.enabled = true;
        }
        else
        {
            canvas.enabled = false;
        }
    }
}
