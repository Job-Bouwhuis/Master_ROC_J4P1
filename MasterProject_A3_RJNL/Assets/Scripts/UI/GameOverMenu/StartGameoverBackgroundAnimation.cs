using ShadowUprising.UI;
using ShadowUprising.UI.Loading;
using UnityEngine;

namespace ShadowUprising.GameOver
{
    [RequireComponent(typeof(TextBackgroundAnimator))]
    public class StartGameoverBackgroundAnimation : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            TextBackgroundAnimator animator = GetComponent<TextBackgroundAnimator>();

            GameOverManager.Instance.OnGameOver += () =>
            {
                Log.Push("Starting gameover background animation");
                animator.AnimateIn();
            };

            if(LoadingScreen.Instance != null)
            {
                LoadingScreen.Instance.OnStartLoading.Subscribe(() =>
                {
                    animator.AnimateOut();
                    return 0;
                });
            }
        }
    }
}