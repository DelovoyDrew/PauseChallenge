using UnityEngine;
using YG;

public class GlobalSaver : MonoBehaviour
{
    public static GlobalSaver Instance { get; private set; }

    public LvlsConfig GameSave { get; private set; }
    public int CurrentLvl { get; private set; }

    [SerializeField] private LvlsConfig _startLvlData;

    [SerializeField] private bool IsClear;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        if (IsClear)
            RemoveSaves();

        GetSave();
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
        YandexGame.savesData.config = GameSave;
        YandexGame.SaveCloud();
    }

    private void GetSave()
    {
        var data = YandexGame.savesData;
        GameSave = data.config;

        if(GameSave == null)
        {
            GameSave = _startLvlData;
        }
    }

}
