// Creator: Job
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.PreviewModels
{
    /// <summary>
    /// Scriptable object that holds the data for the preview model
    /// </summary>
    [CreateAssetMenu(fileName = "New preview model data", menuName = "PreviewModels/Preview Model Data")]
    public class PreviewModelData : ScriptableObject
    {
        [SerializeField] GameObject prefab;
        public GameObject Prefab => prefab;

        [SerializeField] Vector3 scale = Vector3.one;
        public Vector3 Scale => scale;
    }
}