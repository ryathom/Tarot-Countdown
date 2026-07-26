using System;
using System.Collections.Generic;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set;}

    [SerializeField] private TextMeshProUGUI highScoreCounter;
    [SerializeField] private TextMeshProUGUI scoreCounter;
    [SerializeField] private TextMeshProUGUI fateCounter;
    [SerializeField] private TextMeshProUGUI doomCounter;

    [SerializeField] private CardBrowser cardBrowser;
    [SerializeField] private TarotBrowser tarotBrowser;
    [SerializeField] private GameObject helpScreen;
    [SerializeField] private GameObject cardHelpScreen;
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
        cardHelpScreen.transform.localScale = Vector2.zero;
        tarotBrowser.transform.localScale = Vector2.zero;

        InputManager.Instance.OnCancelAction += CloseHelpScreen;
        InputManager.Instance.OnCancelAction += CloseTarotBrowser;
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnCancelAction -= CloseHelpScreen;
        InputManager.Instance.OnCancelAction -= CloseTarotBrowser;
    }

    private void Update()
    {
        UpdateCounters();
        UpdateSacrificeZone();
    }

    private void UpdateCounters()
    {
        highScoreCounter.text = "High Score: " + Math.Max(GameManager.Instance.Score, PlayerPrefs.GetInt("HighScore"));
        scoreCounter.text = "Score: " + GameManager.Instance.Score;
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

    public void OpenBrowser(Zone zone, bool canClose = true, List<Card> subset = null)
    {
        cardBrowser.gameObject.SetActive(true);
        cardBrowser.Open(zone, canClose, subset);
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

    public void OpenCardHelpScreen()
    {
        cardHelpScreen.SetActive(true);
        Tween.Scale(cardHelpScreen.transform, Vector2.one, 0.2f);
    }

    public void CloseCardHelpScreen()
    {
        Tween.Scale(cardHelpScreen.transform, Vector2.zero, 0.1f);
    }

    public void ShowGameOverScreen(string text)
    {
        gameOverScreen.SetActive(true);
        Tween.Scale(gameOverScreen.transform, Vector2.one, 0.2f);
        gameOverText.text = text;
    }

    public void HideGameOverScreen()
    {
        Tween.Scale(gameOverScreen.transform, Vector2.zero, 0.1f)
        .OnComplete(() => GameManager.Instance.LoadMainMenu());
    }

    public void OpenTarotBrowser()
    {
        tarotBrowser.gameObject.SetActive(true);
        Tween.Scale(tarotBrowser.transform, Vector2.one, 0.2f)
        .OnComplete(()=> tarotBrowser.Open());
    }

    public void CloseTarotBrowser()
    {
        Tween.Scale(tarotBrowser.transform, Vector2.zero, 0.1f)
        .OnComplete(() => tarotBrowser.gameObject.SetActive(false));
    }
    
}