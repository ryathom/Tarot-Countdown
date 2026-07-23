using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set;}

    [SerializeField] private TextMeshProUGUI turnCounter;
    [SerializeField] private TextMeshProUGUI fateCounter;
    [SerializeField] private TextMeshProUGUI doomCounter;
    [SerializeField] private TextMeshProUGUI deckCounter;
    [SerializeField] private TextMeshProUGUI deathCounter;

    [SerializeField] private CardBrowser cardBrowser;

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
    }

    private void Update()
    {
        UpdateCounters();
    }

    private void UpdateCounters()
    {
        turnCounter.text = "Turns Remaining: " + GameManager.Instance.Turn;
        fateCounter.text = "Fate: " + GameManager.Instance.Fate;
        doomCounter.text = "Doom: " + GameManager.Instance.Doom;
        deckCounter.text = "Deck: " + GameManager.Instance.Deck.Cards.Count;
        deathCounter.text = "Cards until Death: " + GameManager.Instance.Deck.DeathCardPosition();
    }

    public void OpenBrowser(Zone zone)
    {
        cardBrowser.gameObject.SetActive(true);
        cardBrowser.Open(zone);
    }

    public void CloseBrowser()
    {
        cardBrowser.Close();
        cardBrowser.gameObject.SetActive(false);
    }
    
}