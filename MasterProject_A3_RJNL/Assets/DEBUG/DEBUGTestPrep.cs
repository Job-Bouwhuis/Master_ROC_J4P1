using ShadowUprising.UI.Loading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.DEBUG
{

#nullable enable

    public class DEBUGTestPrep : MonoBehaviour, IScenePrepOperation
    {
        public bool IsComplete { get; set; }

        bool waited = false;

        public void FinishPrep()
        {
        }

        public void StartPrep()
        {
        }

        YieldInstruction? IScenePrepOperation.PrepUpdate()
        {
            if (!waited)
            {
                waited = true;
                return new WaitForSeconds(2);
            }
            return new Completed();
        }
    }
}