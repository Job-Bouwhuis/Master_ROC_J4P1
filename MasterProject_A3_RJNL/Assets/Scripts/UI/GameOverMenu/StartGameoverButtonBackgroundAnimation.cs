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
        void Start()
        {
            TextBackgroundAnimator animator = GetComponent<TextBackgroundAnimator>();
            TextButtonAnimator buttonAnimator = GetComponentInParent<TextButtonAnimator>();
            if(buttonAnimator == null)
            {
                Windows.MessageBox("Gameover menu button hiracghy invalid. Parent of background must be a button with a TextButtonAnimator component.");
                return;
            }
            buttonAnimator.OnAnimationStart += isEntry =>
            {
                Log.Push("Starting gameover background animation");
                if (isEntry)
                {   
                    animator.AnimateIn();
                }
                else
                {
                    animator.AnimateOut();
                }
                
            };
        }
    }
}