using ShadowUprising.UI;
using ShadowUprising.UI.PauseMenu;
using UnityEngine;
using WinterRose;

namespace ShadowUprising.GameOver
{
    [RequireComponent(typeof(TextBackgroundAnimator))]
    public class StartGameoverButtonBackgroundAnimation : MonoBehaviour
    {
        // Start is called before the first frame update
        private bool mayDoHoverAnimation = false;

        void Start()
        {
            TextBackgroundAnimator animator = GetComponent<TextBackgroundAnimator>();
            TextButtonAnimator buttonAnimator = GetComponentInParent<TextButtonAnimator>();
            TextButton button = GetComponentInParent<TextButton>();
            if(buttonAnimator == null)
            {
                Windows.MessageBox("Gameover menu button hiracghy invalid. Parent of background must be a button with a TextButtonAnimator component.");
                return;
            }
            if(button == null)
            {
                Windows.MessageBox("Gameover menu button hiracghy invalid. Parent of background must be a button with a TextButton component.");
                return;
            }

            buttonAnimator.OnAnimationStart += isEntry =>
            {
                Log.Push("Starting gameover background animation");
                if (isEntry)
                    animator.AnimateIn();
                else
                    animator.AnimateOut();
            };

            button.OnHover += hoverEnter =>
            {
                if (!mayDoHoverAnimation)
                {
                    mayDoHoverAnimation = true;
                    return;
                }

                if (hoverEnter)
                    animator.IncreaseWidth(button.hoverAnimationExtend / 2);
                else
                    animator.DecreaseWidth();
            };
        }
    }
}