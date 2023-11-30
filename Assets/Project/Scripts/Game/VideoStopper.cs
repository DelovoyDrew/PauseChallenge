using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class VideoStopper : MonoBehaviour
{
    public static VideoStopper Instance { get; private set; }

    public float showTime=>_lvlPlayer.showTime;

    public Action OnWin;
    public Action<float> OnPause;
    public Action OnTry;

    [SerializeField] private LvlPlayer _lvlPlayer;
    [SerializeField] private LvlConfig _currentConfig;
    [SerializeField] private PausePanel _pausePanel;

    [SerializeField] private Transform _lvlParent;

    private float _mostFarDistance;
    private bool _isGameEnded;

    private List<Vector2> WinTime = new List<Vector2>();

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        OnPause += (float value) => {
            _lvlPlayer.Pause();
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

    public IEnumerator Initialize(LvlConfig config)
    {
        _currentConfig = config;
        
        if (config.LvlTemplate != null)
            yield return StartCoroutine(InitializeAsAnimationLvl());
        else if (config.Path != null)
            yield return StartCoroutine(InitializeAsVideoLvl());

        yield return StartCoroutine(FindMostFarDistance());
    }

    private IEnumerator InitializeAsVideoLvl()
    {
        string path = _currentConfig.Path;

        using (UnityWebRequest www = UnityWebRequest.Get(path))
        {
            yield return www.SendWebRequest();
            Debug.Log(www.url);
            WinTime = _currentConfig.WinTime;
            _lvlPlayer.url = www.url;
            _lvlPlayer.Prepare();
            _lvlPlayer.Play();
            OnTry?.Invoke();
        }
    }

    private IEnumerator InitializeAsAnimationLvl()
    {
        _lvlPlayer = Instantiate(_currentConfig.LvlTemplate, Vector3.zero, Quaternion.identity).GetComponent<LvlPlayer>();
        _lvlPlayer.transform.SetParent(_lvlParent);
        _lvlPlayer.Play();
        yield return new WaitUntil(() => _lvlPlayer.isActiveAndEnabled);
        RecalculateWinTimeForAnimation();
        OnTry?.Invoke();
    }

    private void RecalculateWinTimeForAnimation()
    {
        foreach (var winTime in _currentConfig.WinTime)
        {
            var recalculated = winTime * _lvlPlayer.length / _currentConfig.FramesCount;
            WinTime.Add(recalculated);
        }
    }

    public void RestartVideo()
    {
        _lvlPlayer.Stop();
        _lvlPlayer.Play();
        _isGameEnded = false;
        _pausePanel.Open(false);

        OnTry?.Invoke();
    }

    public void EndGamePauseVideo()
    {
        _lvlPlayer.Pause();
    }

    public void OpenPausePanel()
    {
        if(_lvlPlayer.isPlaying)
            _pausePanel.Open(false);
        else
            _pausePanel.Open(true);
    }

    public void StopVideo()
    {
        if (_isGameEnded)
            return;


        if (_lvlPlayer.isPlaying)
        {
            OnPause?.Invoke(FindPercent());

        }
        else
        {
            _pausePanel.Open(false);
            _lvlPlayer.Play();
        }

    }

    private float FindPercent()
    {
        var time = _lvlPlayer.time;
        var percent = FindClosePercent();

        foreach (var item in WinTime)
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
        var stopTime = _lvlPlayer.time;

        foreach (var item in WinTime)
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

        //Debug.Log($"Most Far: {_mostFarDistance}, Distance: {distance}, Player Length: {_lvlPlayer.length}");

        return (_mostFarDistance - distance) / _mostFarDistance * 100;
    }

    private IEnumerator FindMostFarDistance()
    {
        yield return new WaitUntil(() => _lvlPlayer.isActiveAndEnabled);

        float distance = float.MinValue;
        float prevValue = 0;

        foreach (var item in WinTime)
        {
            var currDistance = Mathf.Abs(item.x - prevValue);
            prevValue = item.y;
            if (distance < currDistance)
                distance = currDistance;
        }

        var newcurrDistance = Mathf.Abs((float)_lvlPlayer.length - prevValue);
        if (distance < newcurrDistance)
            distance = newcurrDistance;

        _mostFarDistance = distance;
    }
}
