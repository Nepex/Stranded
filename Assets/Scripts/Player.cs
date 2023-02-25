using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ProcessInputs();
    }

    private void ProcessInputs()
    {
        if (GameManager.State.PlayerState == PlayerState.Start)
        {
            return;
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.TogglePause();
        }


        if (GameManager.State.IsPaused) return;


        if (Input.GetKeyDown(KeyCode.Z))
        {
            ToggleFishing();
        }

        if (Input.GetKeyDown(KeyCode.X) && GameManager.State.PlayerState == PlayerState.Fishing)
        {
            AttemptFishCatch();
        }

     
    }

    private void ToggleFishing()
    {
        Debug.Log("ToggleFishing()");
        
        // If Player is Idle, start fishing
        if (GameManager.State.PlayerState == PlayerState.Idle)
        {
            GameManager.Instance.UpdatePlayerState(PlayerState.Fishing);

            animator.Play("CharFishingIdle");
            transform.position = new Vector3(1, (float)2.69, 0);
        }
        // If Player is fishing, return to idle
        else if (GameManager.State.PlayerState == PlayerState.Fishing)
        {
            GameManager.Instance.UpdatePlayerState(PlayerState.Idle);

            animator.Play("CharIdle");
            transform.position = new Vector3((float).08, (float)2.84, 0);
        }
    }

    private void AttemptFishCatch() {
        Debug.Log("AttemptFishCatch()");
    }
}
