using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyConfigs/LvlsConfig")]
public class LvlsConfig : ScriptableObject
{
    [field: SerializeField] public List<LvlData> LvlsData { get; private set; }
}
