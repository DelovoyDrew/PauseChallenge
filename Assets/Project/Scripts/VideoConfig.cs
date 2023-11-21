using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "MyConfigs/VideoConfig")]
public class VideoConfig : ScriptableObject
{
    [field: SerializeField] public List<Vector2> WinTime { get; private set; }
    [field: SerializeField] public VideoClip Clip { get; private set; }
}
