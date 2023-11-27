using UnityEngine;

public class MenuVisuals : MonoBehaviour
{
    public static MenuVisuals Instance { get; private set; }

    [SerializeField] private Gradient _gradient;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public Color ColorByProgressPercent(float percent)
    {
        return _gradient.Evaluate((int)percent / 100f);
    }
}
