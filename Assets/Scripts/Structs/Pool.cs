using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Structs
{
    [Serializable]
    public struct Pool
    {
        public Tag tag;
        public GameObject prefab;
        public int size;
        [CanBeNull] public Transform parent;
    }
}