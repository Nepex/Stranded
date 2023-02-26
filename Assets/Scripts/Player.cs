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
        if (GameManager.State.PlayerState == PlayerState.Start) return;


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.TogglePause();
        }


        if (GameManager.State.IsPaused) return;


        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (GameManager.State.PlayerState)
            {
                case PlayerState.FishOnHook:
                    StartCoroutine(AttemptFishCatch());
                    break;
                default:
                    ToggleFishing();
                    break;
            }
        }
    }

    private void ToggleFishing()
    {
        Debug.Log("ToggleFishing()");

        // If Player is Idle, start fishing
        if (GameManager.State.PlayerState == PlayerState.Idle)
        {
            StartFishing();
        }
        // If Player is fishing, return to idle
        else if (GameManager.State.PlayerState == PlayerState.Fishing)
        {
            StopFishing();
        }
    }

    private void StartFishing()
    {

        GameManager.Instance.UpdatePlayerState(PlayerState.Fishing);
        animator.Play("CharFishingIdle");
        transform.position = new Vector3(1.1f, 2.79f, 0);

        InvokeRepeating(nameof(CheckForCatch), 0f, 1f); // calls every 2 second
    }

    private void StopFishing()
    {
        GameManager.Instance.UpdatePlayerState(PlayerState.Idle);
        animator.Play("CharIdle");
        transform.position = new Vector3(.08f, 2.84f, 0);

        CancelInvoke(nameof(CheckForCatch));
    }

    private void CheckForCatch()
    {
        int hookRoll = Random.Range(1, 101); // generate 1-100 number

        // 20% chance we catch a fish on every check
        if (hookRoll >= 80)
        {
            CancelInvoke(nameof(CheckForCatch));
            GameManager.Instance.UpdatePlayerState(PlayerState.FishOnHook);
            animator.Play("CharFishOnHook");
            transform.position = new Vector3(1.1f, 2.84f, 0);

            Debug.Log("Catch detected!");
        }

    }

    private IEnumerator AttemptFishCatch()
    {
        Debug.Log("AttemptFishCatch()");
        GameManager.Instance.UpdatePlayerState(PlayerState.AttempingCatch);
        animator.Play("CharAttemptingCatch");
        transform.position = new Vector3(1.1f, 2.69f, 0);

        int catchRoll = Random.Range(0, 101); // generate 0-100 number
        int randomTimeToCatch = Random.Range(2, 6); // generate 2-5 number

        yield return new WaitForSeconds(randomTimeToCatch);

        // 80% chance we catch a fish
        if (catchRoll <= 80)
        {
            Debug.Log("Caught fish");
        }
        else
        {
            Debug.Log("Failed to catch fish");
        }

        this.StopFishing();
    }
}
