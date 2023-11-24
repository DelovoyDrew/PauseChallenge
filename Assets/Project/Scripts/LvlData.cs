using UnityEngine;

[System.Serializable]
public class LvlData
{
    [field: SerializeField] public int LvlNumber { get; private set; }
    [field: SerializeField] public bool IsOpen { get; private set; }
    [field: SerializeField] public float DonePercent { get; private set; }
    [field: SerializeField] public int Tries { get; private set; }

    public void OpenLvl()
    {
        IsOpen = true;
    }

    public LvlData(int lvlNumber, bool isOpen, float donePercent, int tries)
    {
        LvlNumber = lvlNumber;
        IsOpen = isOpen;
        DonePercent = donePercent;
        Tries = tries;
    }
}
