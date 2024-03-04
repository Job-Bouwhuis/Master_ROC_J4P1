//Creator: Luke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.WeaponBehaviour
{
    public class ShellEjector : MonoBehaviour
    {
        [SerializeField] GameObject shell;
        [SerializeField] Transform ejectPosition;

        public void EjectShell()
        {
            var gameObject = Instantiate(shell, ejectPosition.position, Quaternion.identity);
            var rigidBody = gameObject.GetComponent<Rigidbody>();
            rigidBody.AddForce(ejectPosition.up * Random.RandomRange(3,6), ForceMode.Impulse);
            Destroy(gameObject, 5);
        }

    }
}