using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class GlobalSaver : MonoBehaviour
{
    public static GlobalSaver Instance { get; private set; }

    public LvlsData GameSave { get; private set; }
    public LvlData CurrentLvlData => GameSave.Data[CurrentLvl];
    public int CurrentLvl { get; private set; }
    public LvlConfig VideoLvl => GetConfigByIndex(CurrentLvl);

    [SerializeField] private float _autoSaveDelay;

    [SerializeField] private LvlsConfig _startLvlData;
    [SerializeField] private List<LvlConfig> _lvls;

    [SerializeField] private bool IsClear;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (IsClear == true)
            RemoveSaves();

        StartCoroutine(GetSave());
        StartCoroutine(SaveRoutine());
    }

    public LvlConfig GetConfigByIndex(int index)
    {
        if (index < 0 || index >= _lvls.Count)
            Debug.LogError("Wrong index");

        return _lvls[index];
    }

    private void RemoveSaves()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveCloud();
    }

    public void StartGame(int lvlNumber)
    {
        CurrentLvl = lvlNumber;
    }

    public void Save()
    {
        YandexGame.savesData.data = GameSave;
        YandexGame.SaveProgress();
    }

    private IEnumerator GetSave()
    {
        yield return new WaitUntil(() => YandexGame.SDKEnabled);
        YandexGame.FullscreenShow();
        YandexGame.LoadProgress();
        var data = YandexGame.savesData;
        GameSave = data.data;

        if(GameSave == null)
        {
            GameSave = new LvlsData(_startLvlData.LvlsData.Data);
        }
    }

    private IEnumerator SaveRoutine()
    {
        yield return new WaitUntil(() => GameSave != null);
        var delay = new WaitForSeconds(_autoSaveDelay);

        while(gameObject.activeSelf)
        {
            yield return delay;
            Save();
        }
    }

}
