// Creator: Ruben
using System.Collections;
using ShadowUprising;
using UnityEngine;


namespace ShadowUprising.WeaponBehaviour
{
    /// <summary>
    /// This component manages the interaction between the gun and the objects it hits.
    /// This component REQUIRES a PistolRaycastHandler in the same object to function correctly.
    /// </summary>
    [RequireComponent(typeof(PistolRaycastHandler))]
    public class PistolHitRegistration : MonoBehaviour
    {
        void Start()
        {
            PistolRaycastHandler pistolRaycastHandler = GetComponent<PistolRaycastHandler>();
            if (pistolRaycastHandler == null)
                Log.PushError("This scene contains a PistolHitRegistration but the object does not contain a PistolRaycastHandler. This will leave the PistolHitRegistration useless and cause errors");
            pistolRaycastHandler.onRaycastHit += OnRaycastHit;
        }

        void OnRaycastHit(RaycastHit hitinfo)
        {
            if (hitinfo.transform.gameObject == null)
                return;
                
            IHitable target = hitinfo.transform.GetComponent<IHitable>();
            if (target != null)
            {
                target.HitEvent();
            }
        }
    }
}