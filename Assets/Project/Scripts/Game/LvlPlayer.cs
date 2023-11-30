using UnityEngine;

public class LvlPlayer : MonoBehaviour
{
    public virtual bool isPlaying { get; protected set; }
    public virtual bool isPrepared { get; protected set; }

    public virtual float time { get; protected set; }
    public virtual float showTime { get; protected set; }
    public virtual float length { get; protected set; }

    public virtual string url { get; set; }

    public virtual void Stop() { }
    public virtual void Pause() { }
    public virtual void Play() { }
    public virtual void Prepare() { }
}
