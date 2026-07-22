using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardVisual : MonoBehaviour
{
    [SerializeField] private Image front;
    [SerializeField] private Image back;
    [SerializeField] private TextMeshProUGUI cardName;

    private Card card;

    public void SetCard(Card card)
    {
        this.card = card;
        UpdateVisuals();
        
    }

    public void UpdateVisuals()
    {
        cardName.text = card.Name;
        back.enabled = !card.FaceUp;
    }
}