using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Start,
    Idle,
    Fishing,
    Dead
}

public class GameState
{
    public PlayerState PlayerState { get; set; }
    public bool IsPaused { get; set; }
    public int Health { get; set; }
    public int FishCaught { get; set; }

    public static readonly GameState Default = new()
    {
        PlayerState = PlayerState.Start,
        IsPaused = false,
        Health = 100,
        FishCaught = 0
    };
}


