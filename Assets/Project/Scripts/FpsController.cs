using UnityEngine;

public class FpsController : MonoBehaviour
{
    public static FpsController Instance { get; private set; }
    [SerializeField] private bool _isShowFPS;
    private float count;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        Application.targetFrameRate = 120;

        GUI.depth = 2;
    }

    private void Update()
    {
        if (!_isShowFPS)
            return;

        float current = 0;
        current = Time.frameCount / Time.time;
        count = (int)current;
    }

    private void OnGUI()
    {
        if (!_isShowFPS)
            return;

        Rect location = new Rect(5, 5, 255, 75);
        string text = $"FPS: {Mathf.Round(count)}";
        Texture black = Texture2D.linearGrayTexture;
        GUI.DrawTexture(location, black, ScaleMode.StretchToFill);
        GUI.color = Color.green;
        GUI.skin.label.fontSize = 40;
        GUI.Label(location, text);
    }
}
