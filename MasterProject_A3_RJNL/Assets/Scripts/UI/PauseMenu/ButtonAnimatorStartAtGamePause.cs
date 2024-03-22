using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.UI.PauseMenu
{
    /// <summary>
    /// Makes the <see cref="TextButtonAnimator"/> component activate when the pause menu is showm and deactivate when the pause menu is hidden
    /// </summary>
    [RequireComponent(typeof(TextButtonAnimator))]
    public class ButtonAnimatorStartAtGamePause : MonoBehaviour
    {
        TextButtonAnimator animator;
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<TextButtonAnimator>();
            PauseMenuManager.Instance.OnPauseMenuShow += i =>
            {
                StopAllCoroutines();
                animator.StopAllCoroutines();
                StartCoroutine(animator.WaitToStartAnimation());
            };

            PauseMenuManager.Instance.OnPauseMenuHide.Subscribe(() =>
            {
                StopAllCoroutines();
                animator.StopAllCoroutines();
                StartCoroutine(animator.AnimationHide());
                return animator.delayBeforeUnlock;
            });
        }
    }
}