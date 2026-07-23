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
        cardName.text = "";
        back.sprite = card.CardSO.CardBack;
        back.enabled = !card.FaceUp;

        front.sprite = SetFrontSprite();

        if (card is MajorArcana majorArcana)
        {
            cardText.text = majorArcana.Text;
        } else
        {
            cardText.text = "";
        }
    }

    public Sprite SetFrontSprite()
    {
        if (card is MinorArcana minor && card.CardSO is MinorArcanaSO minorSO)
        {
            return minor.Suit switch
            {
                Suit.Wands => minorSO.wandSprites[minor.Number - 1],
                Suit.Cups => minorSO.cupSprites[minor.Number - 1],
                Suit.Coins => minorSO.coinSprites[minor.Number - 1],
                Suit.Swords => minorSO.swordSprites[minor.Number - 1],
                _ => null,
            };
        } else if (card is MajorArcana major && card.CardSO is MajorArcanaSO majorSO)
        {
            return major switch
            {
                Chariot => majorSO.chariotSprite,
                Devil => majorSO.devilSprite,
                Fool => majorSO.foolSprite,
                HangedMan => majorSO.hangedManSprite,
                Judgement => majorSO.judgementSprite,
                Justice => majorSO.justiceSprite,
                Magician => majorSO.magicianSprite,
                Moon => majorSO.moonSprite,
                Star => majorSO.starSprite,
                Strength => majorSO.strengthSprite,
                Sun => majorSO.sunSprite,
                _ => null,
            };
        } else if (card is Death && card.CardSO is DeathSO deathSO)
        {
            return deathSO.deathSprite;
        } else
        {
            return null;
        }
    }
}