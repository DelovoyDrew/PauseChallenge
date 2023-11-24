using System.Collections.Generic;
using UnityEngine;

public class MenuInitializer : MonoBehaviour
{
    [SerializeField] private List<MenuLvl> _lvls;

    private void Start()
    {
        var saveData = GlobalSaver.Instance.GameSave;
        foreach (var item in _lvls)
        {
            var lvlData = saveData.LvlsData.Find(x => x.LvlNumber == item.Number);
            item.Initilize(lvlData.IsOpen, lvlData.DonePercent);
        }
    }

}
