using NuiN.NExtensions;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Prefab Reference")]
public class PoolObjectPrefabReferenceSO : ScriptableObject
{
    [field: SerializeField] public PoolObject Prefab { get; private set; }
}