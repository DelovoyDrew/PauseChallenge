using UnityEngine;

public class AnimationLvlPlayer : LvlPlayer
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audio;

    public override bool isPlaying => _isPlaying;
    public override float length => _state.length;
    public override float time => _state.normalizedTime * length;
    public override float showTime => _audio.time;

    private bool _isPlaying;

    private AnimatorStateInfo _state;

    private void Awake()
    {
        _state = _animator.GetCurrentAnimatorStateInfo(0);
    }

    public override void Play()
    {
        _animator.speed = 1.0f;
        _audio.Play();
        _isPlaying = true;
    }

    public override void Pause()
    {
        _animator.speed = 0f;
        _audio.Pause();
        _isPlaying = false;
    }

    public override void Stop()
    {
        _animator.speed = 0f;
        _audio.Stop();
        _isPlaying = false;
    }
}
