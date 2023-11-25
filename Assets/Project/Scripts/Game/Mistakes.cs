using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mistakes : MonoBehaviour
{
    [SerializeField] private List<Image> _crosses;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _mistaketColor;

    private int _maxMistakes => _crosses.Count;
    private int _currentMistakesCount;

    public void ResetMistakes()
    {
        _currentMistakesCount = 0;
        foreach (var m in _crosses)
        {
            m.color = _defaultColor;
        }
    }

    public bool IsLooseGame()
    {
        GetMistake();
        bool isLoose = _currentMistakesCount >= _maxMistakes;
        if (isLoose)
            _currentMistakesCount = 0;

        return isLoose;
    }

    private void GetMistake()
    {
        _currentMistakesCount++;
        _crosses[_currentMistakesCount - 1].color = _mistaketColor;
    }
}
