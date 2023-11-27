using UnityEngine;
using UnityEngine.UI;

public class MenuLvl : MonoBehaviour
{
    [field: SerializeField] public int Number { get; private set; }

    [SerializeField] private MenuLvlVisuals _visual;
    [SerializeField] private Button _playLvlButton;


    public void Initilize(bool isOpen, float progressPercent)
    {
        _visual.Initialize(isOpen, progressPercent);

        if (isOpen)
            _playLvlButton.onClick.AddListener(() => Menu.Instance.StartLvl(Number));
        else
            _playLvlButton.enabled = false;
    }

    public void OpenForAd()
    {
        var globalLvlSave = GlobalSaver.Instance.GameSave.Data.Find(x => x.LvlNumber == Number);
        globalLvlSave.OpenLvl();
        _visual.Initialize(globalLvlSave.IsOpen, globalLvlSave.DonePercent);
    }
}
