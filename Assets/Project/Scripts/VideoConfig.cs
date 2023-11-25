using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyConfigs/VideoConfig")]
public class VideoConfig : ScriptableObject
{
    public string Path => System.IO.Path.Combine(Application.streamingAssetsPath, _fileName);

    [field: SerializeField] public List<Vector2> WinTime { get; private set; }
    [SerializeField] public string _fileName;
}
