using UnityEngine;

public class LvlPlayer : MonoBehaviour
{
    public bool isPlaying { get; protected set; }
    public bool isPrepared { get; protected set; }

    public float time { get; protected set; }
    public float length { get; protected set; }

    public string url { get; set; }

    public virtual void Stop() { }
    public virtual void Pause() { }
    public virtual void Play() { }
    public virtual void Prepare() { }
}
