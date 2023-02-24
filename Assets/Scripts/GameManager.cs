using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private static GameState _state;
    public static GameState State => _state;

    public static event Action<GameState> OnGameStateChanged;
    public static event Action<PlayerState> OnPlayerStateChanged;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _state = GameState.Default;
    }

    public void TogglePause()
    {
        Debug.Log($"TogglePause(): {(_state.IsPaused ? "unpausing..." : "pausing...")}");

        // Game is already paused, unpause
        if (_state.IsPaused)
        {
            Time.timeScale = 1;
            _state.IsPaused = false;
            return;
        }

        // Pause
        Time.timeScale = 0;
        _state.IsPaused = true;
    }


    public void UpdatePlayerState(PlayerState newState)
    {
        _state.PlayerState = newState;
        Debug.Log($"UpdatePlayerState(): PlayerState {newState}");

        OnPlayerStateChanged?.Invoke(_state.PlayerState);
        OnGameStateChanged?.Invoke(_state);
    }
}

