using UnityEngine;

[System.Serializable]
public class LvlData
{
    [field: SerializeField] public int LvlNumber { get; private set; }
    [field: SerializeField] public bool IsOpen;
    [field: SerializeField] public float DonePercent;
    [field: SerializeField] public int Tries;

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

    public static string PercentToString(float percent)
    {
        if (percent >= 100)
            return percent.ToString();
        else
            return percent.ToString("0.00");
    }

    public static string RoundPercentToString(float percent)
    {
        if (percent >= 100)
            return percent.ToString();
        else
        {
            return percent.ToString("0.00");
        }
    }
}
