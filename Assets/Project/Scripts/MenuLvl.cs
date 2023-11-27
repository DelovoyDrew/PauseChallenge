using UnityEngine;
using UnityEngine.UI;
using YG;

public class MenuLvl : MonoBehaviour
{
    [field: SerializeField] public int Number { get; private set; }

    [SerializeField] private MenuLvlVisuals _visual;
    [SerializeField] private Button _playLvlButton;


    public void Initilize(bool isOpen, float progressPercent, int triesCount)
    {
        _visual.Initialize(isOpen, Number, progressPercent, triesCount);

        if (isOpen)
            _playLvlButton.onClick.AddListener(() => Menu.Instance.StartLvl(Number));
        else
            _playLvlButton.enabled = false;

        if(!isOpen)
        {
            YandexGame.RewardVideoEvent += OpenForAd;
        }
    }

    public void ShowAd()
    {
        YandexGame.RewVideoShow(Number);
    }

    public void OpenForAd(int id)
    {
        if (id != Number)
            return;

        //YandexGame.RewardVideoEvent -= OpenForAd;

        var globalLvlSave = GlobalSaver.Instance.GameSave.Data.Find(x => x.LvlNumber == Number);
        globalLvlSave.OpenLvl();
        _visual.Initialize(globalLvlSave.IsOpen, Number, globalLvlSave.DonePercent, globalLvlSave.Tries);
    }
}
