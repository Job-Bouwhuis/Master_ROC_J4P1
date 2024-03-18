//Creator: Luke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.WeaponBehaviour
{
    /// <summary>
    /// makes it so the pistol ejects shells when the pistol is shot
    /// </summary>
    public class ShellEjector : MonoBehaviour
    {
        [SerializeField] GameObject shell;
        [SerializeField] Transform ejectPosition;

        public void EjectShell()
        {
            var gameObject = Instantiate(shell, ejectPosition.position, ejectPosition.rotation);
            var rigidBody = gameObject.GetComponent<Rigidbody>();
            rigidBody.AddForce(ejectPosition.up * Random.RandomRange(3,6), ForceMode.Impulse);
            Destroy(gameObject, 5);
        }

    }
}