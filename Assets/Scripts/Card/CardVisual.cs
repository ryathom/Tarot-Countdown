using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardVisual : MonoBehaviour
{
    [SerializeField] private Image front;
    [SerializeField] private Image back;
    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private TextMeshProUGUI cardText;

    private Card card;

    public void SetCard(Card card)
    {
        this.card = card;
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        cardName.text = card.Name;
        back.sprite = card.CardSO.CardBack;
        back.enabled = !card.FaceUp;

        if (card is MajorArcana majorArcana)
        {
            cardText.text = majorArcana.Text;
        } else
        {
            cardText.text = "";
        }
    }
}