using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;

public class VideoStopper : MonoBehaviour
{
    public static VideoStopper Instance { get; private set; }

    public Action OnWin;
    public Action<float> OnPause;
    public Action OnTry;

    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private VideoConfig _currentConfig;
    [SerializeField] private PausePanel _pausePanel;

    private float _mostFarDistance;
    private bool _isGameEnded;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        OnPause += (float value) => {
            _videoPlayer.Pause();
        };

        OnWin += () => {
            _isGameEnded = true;
            Debug.Log("win");
        };

    }

    private void Start()
    {
        StartCoroutine(Initialize(GlobalSaver.Instance.VideoLvl));
    }

    public IEnumerator Initialize(VideoConfig config)
    {
        _currentConfig = config;
        string path = _currentConfig.Path;

        using (UnityWebRequest www = UnityWebRequest.Get(path))
        {
            yield return www.SendWebRequest();
            Debug.Log(www.url);
            _videoPlayer.url = www.url;
            _videoPlayer.Prepare();
            _videoPlayer.Play();
            OnTry?.Invoke();
        }

        StartCoroutine(FindMostFarDistance());
    }

    public void RestartVideo()
    {
        _videoPlayer.Stop();
        _videoPlayer.Play();
        _isGameEnded = false;
        _pausePanel.Open(false);

        OnTry?.Invoke();
    }

    public void PauseVideo()
    {
        _videoPlayer.Pause();
    }

    public void OpenPausePanel()
    {
        _pausePanel.Open(true);
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
            _pausePanel.Open(false);
            _videoPlayer.Play();
        }

    }

    private float FindPercent()
    {
        var time = _videoPlayer.time;
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
        var stopTime = _videoPlayer.time;

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

        //Debug.Log($"Most Far: {_mostFarDistance}, Distance: {distance}, Player Length: {_videoPlayer.length}");

        return (_mostFarDistance - distance) / _mostFarDistance * 100;
    }

    private IEnumerator FindMostFarDistance()
    {
        yield return new WaitUntil(() => _videoPlayer.isPrepared);

        float distance = float.MinValue;
        float prevValue = 0;

        foreach (var item in _currentConfig.WinTime)
        {
            var currDistance = Mathf.Abs(item.x - prevValue);
            prevValue = item.y;
            if (distance < currDistance)
                distance = currDistance;
        }

        var newcurrDistance = Mathf.Abs((float)_videoPlayer.length - prevValue);
        if (distance < newcurrDistance)
            distance = newcurrDistance;

        _mostFarDistance = distance;
    }
}
