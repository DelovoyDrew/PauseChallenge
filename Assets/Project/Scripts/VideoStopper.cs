using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoStopper : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;

    [SerializeField] private VideoConfig _currentConfig;

    private float _mostFarDistance;

    private void Awake()
    {
        _mostFarDistance = FindMostFarDistance();
    }

    public void Initialize(VideoConfig config)
    {
        _currentConfig = config;

        _videoPlayer.clip = _currentConfig.Clip;
        _mostFarDistance = FindMostFarDistance();
    }

    public void StopVideo()
    {
        _videoPlayer.Pause();
        var time = _videoPlayer.clockTime;
        FindClosePercent();

        foreach (var item in _currentConfig.WinTime)
        {
            if(time >= item.x && time <= item.y)
            {
                Debug.Log("win");
                return;
            }
        }
        _videoPlayer.Play();
    }

    private int FindClosePercent()
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

        Debug.Log("Percent: " + (int)((distance / _mostFarDistance) * 100) + "%");

        return 0;
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

        Debug.Log("Max DIstance: " + distance);

        return distance;
    }
}
