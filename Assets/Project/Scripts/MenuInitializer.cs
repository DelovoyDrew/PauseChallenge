using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInitializer : MonoBehaviour
{
    [SerializeField] private List<MenuLvl> _lvls;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => GlobalSaver.Instance.GameSave != null);
        var saveData = GlobalSaver.Instance.GameSave;
        foreach (var item in _lvls)
        {
            var lvlData = saveData.Data.Find(x => x.LvlNumber == item.Number);
            item.Initilize(lvlData.IsOpen, lvlData.DonePercent, lvlData.Tries);
        }
    }

}
