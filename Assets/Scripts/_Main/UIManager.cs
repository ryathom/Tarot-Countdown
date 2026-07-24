using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set;}

    [SerializeField] private TextMeshProUGUI turnCounter;
    [SerializeField] private TextMeshProUGUI fateCounter;
    [SerializeField] private TextMeshProUGUI doomCounter;

    [SerializeField] private CardBrowser cardBrowser;
    [SerializeField] private GameObject helpScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Image sacrificeZone;

    public bool BrowserOpen {get => cardBrowser.isOpen;}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        cardBrowser.gameObject.SetActive(false);
        gameOverScreen.transform.localScale = Vector2.zero;
        helpScreen.transform.localScale = Vector2.zero;

        InputManager.Instance.OnCancelAction += CloseHelpScreen;
    }

    private void Update()
    {
        UpdateCounters();
        UpdateSacrificeZone();
    }

    private void UpdateCounters()
    {
        turnCounter.text = "Turns Remaining: " + GameManager.Instance.Turn;
        fateCounter.text = "Fate: " + GameManager.Instance.Fate;
        doomCounter.text = "Doom: " + GameManager.Instance.Doom;
    }

    private void UpdateSacrificeZone()
    {
        if (GameManager.Instance.CanSacrifice)
        {
            sacrificeZone.color = Color.white;
        } else
        {
            sacrificeZone.color = Color.grey;
        }
    }

    public void OpenBrowser(Zone zone, bool canClose = true)
    {
        cardBrowser.gameObject.SetActive(true);
        cardBrowser.Open(zone, canClose);
    }

    public void CloseBrowser()
    {
        cardBrowser.Close();
        cardBrowser.gameObject.SetActive(false);
    }

    public void OpenHelpScreen()
    {
        helpScreen.SetActive(true);
        Tween.Scale(helpScreen.transform, Vector2.one, 0.2f);
    }

    public void CloseHelpScreen()
    {
        Tween.Scale(helpScreen.transform, Vector2.zero, 0.1f);
    }

    public void ShowGameOverScreen(string text)
    {
        gameOverScreen.SetActive(true);
        Tween.Scale(gameOverScreen.transform, Vector2.one, 0.2f);
        gameOverText.text = text;
    }

    public void HideGameOverScreen()
    {
        Tween.Scale(gameOverScreen.transform, Vector2.zero, 0.1f);
    }
    
}