using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI turnCounter;
    [SerializeField] private TextMeshProUGUI fateCounter;
    [SerializeField] private TextMeshProUGUI doomCounter;

    private void Update()
    {
        UpdateCounters();
    }

    private void UpdateCounters()
    {
        turnCounter.text = "Turn: " + GameManager.Instance.Turn;
        fateCounter.text = "Fate: " + GameManager.Instance.Fate;
        doomCounter.text = "Doom: " + GameManager.Instance.Doom;
    }
    
}