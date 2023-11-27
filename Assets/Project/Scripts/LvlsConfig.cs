using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyConfigs/LvlsConfig")]
public class LvlsConfig : ScriptableObject
{
    [field: SerializeField] public LvlsData LvlsData { get; private set; }
}

[System.Serializable]
public class LvlsData
{
    [field: SerializeField] public List<LvlData> Data { get; private set; }

    public LvlsData()
    {
        Data = null;
    }

    public LvlsData(List<LvlData> data)
    {
        Data = new List<LvlData>();
        foreach (var item in data)
        {
            Data.Add(new LvlData(item.LvlNumber, item.IsOpen, item.DonePercent, item.Tries));
        }
    }
}