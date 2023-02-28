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


        if (Input.GetKeyDown(KeyCode.X))
        {
            SetPlayerIdle();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (GameManager.State.PlayerState)
            {
                case PlayerState.Idle:
                    StartFishing();
                    break;
                case PlayerState.FishOnHook:
                    StartCoroutine(AttemptFishCatch());
                    break;
                case PlayerState.FishCaught:
                    EatFish();
                    break;
            }
        }
    }

    private void SetPlayerIdle()
    {
        // stop any asynchronous or interval actions in the current monobehavior
        // probably need a check for when to do this, as some actions need to finish
        CancelInvoke();
        StopAllCoroutines();

        GameManager.Instance.UpdatePlayerState(PlayerState.Idle);
        animator.Play("CharIdle");
        transform.position = new Vector3(.08f, 2.84f, 0);
    }

    private void StartFishing()
    {
        GameManager.Instance.UpdatePlayerState(PlayerState.Fishing);
        animator.Play("CharFishingIdle");
        transform.position = new Vector3(1.1f, 2.79f, 0);

        InvokeRepeating(nameof(CheckForCatch), 2f, 2f); // calls every 2 second
    }

    private void CheckForCatch()
    {
        // 20% chance we catch a fish on every check
        if (Util.DidProc(20))
        {
            CancelInvoke(nameof(CheckForCatch));
            GameManager.Instance.UpdatePlayerState(PlayerState.FishOnHook);
            animator.Play("CharFishOnHook");
            transform.position = new Vector3(1.1f, 2.84f, 0);

            Debug.Log("Fish on hook!");
        }

    }

    private IEnumerator AttemptFishCatch()
    {
        GameManager.Instance.UpdatePlayerState(PlayerState.AttempingCatch);
        animator.Play("CharAttemptingCatch");
        transform.position = new Vector3(1.1f, 2.69f, 0);

        // wait 2-5 seconds before determining if catch was successful
        yield return new WaitForSeconds(Util.Roll(2, 5));

        // 80% chance we catch a fish
        if (Util.DidProc(80))
        {
            Debug.Log("Caught fish");
            GameManager.Instance.UpdatePlayerState(PlayerState.FishCaught);
            animator.Play("CharCaughtFish");
            transform.position = new Vector3(1.1f, 3.28f, 0);
        }
        else
        {
            Debug.Log("Failed to catch fish");
            SetPlayerIdle();
        }
    }

    private void EatFish()
    {
        // Do eating animation
        // Heal player
        // Set to idle
    }
}
