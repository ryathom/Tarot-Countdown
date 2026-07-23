using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI turnCounter;
    [SerializeField] private TextMeshProUGUI fateCounter;
    [SerializeField] private TextMeshProUGUI doomCounter;
    [SerializeField] private TextMeshProUGUI deckCounter;
    [SerializeField] private TextMeshProUGUI deathCounter;

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
    
}