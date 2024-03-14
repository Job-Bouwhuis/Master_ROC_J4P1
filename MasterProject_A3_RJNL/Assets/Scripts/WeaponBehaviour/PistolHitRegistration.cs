// Creator: Ruben
using System.Collections;
using ShadowUprising;
using UnityEngine;


namespace ShadowUprising.WeaponBehaviour
{
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
            IHitable target = hitinfo.transform.GetComponent<IHitable>();
            if (target != null)
            {
                target.HitEvent();
            }
        }
    }
}