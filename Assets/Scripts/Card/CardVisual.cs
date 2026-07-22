using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardVisual : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI cardName;

    public void SetCard(Card card)
    {
        cardName.text = card.Name;
    }
}