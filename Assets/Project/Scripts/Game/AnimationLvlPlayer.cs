using System.Collections;
using UnityEngine;

public class AnimationLvlPlayer : LvlPlayer
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audio;

    public override bool isPlaying => _isPlaying;
    public override float length => _length;
    public override float time => (_state.normalizedTime * length) % length;
    public override float showTime => _audio.time;

    private bool _isPlaying;
    private float _length;

    private AnimatorStateInfo _state => _animator.GetCurrentAnimatorStateInfo(0);

    private void Awake()
    {
        _length = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
    }

    public override void Play()
    {
        _animator.speed = 1.0f;

        if (!_audio.isPlaying)
            _audio.Play();

        _audio.pitch = 1f;

        _isPlaying = true;
    }

    public override void Pause()
    {
        _animator.speed = 0f;
        _audio.pitch = 0f;
        _isPlaying = false;
    }

    public override void Stop()
    {
        _animator.speed = 0f;
        _audio.Stop();
        _isPlaying = false;
    }
}
