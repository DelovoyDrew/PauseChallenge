using System;
using UnityEngine;
using UnityEngine.Video;

public class VideoStopper : MonoBehaviour
{
    public static VideoStopper Instance { get; private set; }

    public Action OnWin;
    public Action<float> OnPause;

    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private VideoConfig _currentConfig;

    private float _mostFarDistance;
    private bool _isGameEnded;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        _mostFarDistance = FindMostFarDistance();

        OnPause += (float value) => {
            _videoPlayer.Pause();
        };

        OnWin += () => {
            _isGameEnded = true;
            Debug.Log("win");
        };

    }

    public void Initialize(VideoConfig config)
    {
        _currentConfig = config;

        _videoPlayer.clip = _currentConfig.Clip;
        _mostFarDistance = FindMostFarDistance();
    }

    public void RestartVideo()
    {
        _videoPlayer.Stop();
        _videoPlayer.Play();
        _isGameEnded = false;
    }

    public void StopVideo()
    {
        if (_isGameEnded)
            return;

        if (_videoPlayer.isPlaying)
        {
            OnPause?.Invoke(FindPercent());
        }
        else
        {
            _videoPlayer.Play();
        }

    }

    private float FindPercent()
    {
        var time = _videoPlayer.clockTime;
        var percent = FindClosePercent();

        foreach (var item in _currentConfig.WinTime)
        {
            if (time >= item.x && time <= item.y)
            {
                OnWin?.Invoke();
                return 100;
            }
        }

        return percent;
    }

    private float FindClosePercent()
    {
        float distance = float.MaxValue;
        var stopTime = _videoPlayer.clockTime;

        foreach (var item in _currentConfig.WinTime)
        {
            if(stopTime < item.x)
            {
                var newDistance = Mathf.Abs(item.x - (float)stopTime);
                if (distance > newDistance)
                {
                    distance = newDistance;
                }
            }
            else if(stopTime > item.y)
            {
                var newDistance = Mathf.Abs(item.y - (float)stopTime);
                if (distance > newDistance)
                {
                    distance = newDistance;
                }
            }
            
        }

        return distance / _mostFarDistance * 100;
    }

    private float FindMostFarDistance()
    {
        float distance = float.MinValue;
        float prevValue = 0;

        foreach (var item in _currentConfig.WinTime)
        {
            var currDistance = Mathf.Abs(item.x - prevValue);
            prevValue = item.y;
            if (distance < currDistance)
                distance = currDistance;
        }

        var newcurrDistance = Mathf.Abs((float)_currentConfig.Clip.length - _currentConfig.WinTime[_currentConfig.WinTime.Count - 1].y);
        if (distance < newcurrDistance)
            distance = newcurrDistance;

        return distance;
    }
}
