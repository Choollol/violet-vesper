using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScreen : ImageUtil
{
    private const float WAIT_TIME_SECONDS = 1;

    private Animator animator;
    public override void OnEnable()
    {
        EventMessenger.StartListening(EventKey.BeginScreenTransition, BeginTransition);
        EventMessenger.StartListening(EventKey.EndScreenTransition, EndTransition);
    }
    public override void OnDisable()
    {
        EventMessenger.StopListening(EventKey.BeginScreenTransition, BeginTransition);
        EventMessenger.StopListening(EventKey.EndScreenTransition, EndTransition);
    }

    public override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
    }

    // Stop transition if it is currently active
    private void StopTransition()
    {
        StopAllCoroutines();

        // Check if the transition is active
        if (DataMessenger.GetBool(BoolKey.IsScreenTransitioning))
        {
            // Screen is no longer transitioning, update DataMessenger accordingly
            DataMessenger.SetBool(BoolKey.IsScreenTransitioning, false);
        }
    }
    
    private void BeginTransition()
    {
        StopTransition();

        StartCoroutine(BeginAndWaitForAnimation("TransitionScreenBeginAnimation", true));
    }
    private void EndTransition()
    {
        StopTransition();

        StartCoroutine(BeginAndWaitForAnimation("TransitionScreenEndAnimation", false));
    }
    private IEnumerator BeginAndWaitForAnimation(string animationName, bool isTransitioningIn)
    {
        DataMessenger.SetBool(BoolKey.IsScreenTransitioning, true);

        Enable();

        animator.Play(animationName);

        yield return null;

        // Wait for animation to stop playing
        while (animator.IsCurrentAnimationPlaying()) yield return null;

        if (isTransitioningIn)
        {
            yield return new WaitForSeconds(WAIT_TIME_SECONDS);
        }
        else
        {
            Disable();
        }

        DataMessenger.SetBool(BoolKey.IsScreenTransitioning, false);
    }
}