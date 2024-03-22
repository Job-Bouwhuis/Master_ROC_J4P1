// Creator: job
using UnityEngine;

namespace ShadowUprising.UI.Loading
{
    public class LoadingSpinner : MonoBehaviour
    {
        public float speed = 1;
        bool spinning = false;

        public void StartSpinning()
        {
            spinning = true;
        }

        public void StopSpinning()
        {
            spinning = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (spinning)
            {
                transform.Rotate(Vector3.forward, speed * 360 * Time.deltaTime);
            }
        }
    }
}