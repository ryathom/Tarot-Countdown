using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardVisual : MonoBehaviour
{
    [SerializeField] private Image front;
    [SerializeField] private Image back;

    [SerializeField] private Color wandsColour = Color.red;
    [SerializeField] private Color pentaclesColour = Color.green;
    [SerializeField] private Color cupsColour = Color.blue;
    [SerializeField] private Color swordsColour = Color.yellow;

    [SerializeField] private Vector2 borderThickness = new Vector2(3f, -3f);

    private Outline suitOutline;

    [SerializeField] private GameObject majorPopUp;
    [SerializeField] private GameObject minorPopUp;

    [SerializeField] private TextMeshProUGUI majorName;
    [SerializeField] private TextMeshProUGUI majorText;
    [SerializeField] private TextMeshProUGUI minorName;
    [SerializeField] private TextMeshProUGUI minorText;

    private Card card;

    private void Awake()
    {
        suitOutline = front.GetComponent<Outline>();

        if (suitOutline == null)
        {
            suitOutline = front.gameObject.AddComponent<Outline>();
        }

        suitOutline.effectDistance = borderThickness;
        suitOutline.useGraphicAlpha = false;
        suitOutline.enabled = false;
    }

    public void SetCard(Card card)
    {
        this.card = card;
        majorPopUp.transform.localScale = Vector3.zero;
        minorPopUp.transform.localScale = Vector3.zero;
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        if (card is MinorArcana ma) ma.SetName();

        majorName.text = card.Name;
        minorName.text = card.Name;
        back.sprite = card.CardSO.CardBack;
        back.enabled = !card.FaceUp;

        front.sprite = SetFrontSprite();
        UpdateSuitBorder();

        if (card is MajorArcana majorArcana)
        {
            majorText.text = "Cost: " + majorArcana.FateCost + " Fate\n" + majorArcana.Text;
            majorPopUp.SetActive(true);
            minorPopUp.SetActive(false);
        } else if (card is MinorArcana)
        {
            minorText.text = "Cost: Mill " + card.GetMillCost() + " cards.";
            majorPopUp.SetActive(false);
            minorPopUp.SetActive(true);
        } else if (card is Death)
        {
            minorText.text = "You die if you draw or mill this card.";
            majorPopUp.SetActive(false);
            minorPopUp.SetActive(true);
        }
    }

    public void ShowMajorPopUp()
    {
        if (majorPopUp.transform.localScale == Vector3.one) return;

        Tween.Scale(majorPopUp.transform, Vector3.one, 0.1f);
    }

    public void HideMajorPopUp()
    {
        if (majorPopUp.transform.localScale == Vector3.zero) return;

        Tween.Scale(majorPopUp.transform, Vector3.zero, 0.1f);
    }

    public void ShowMinorPopUp()
    {
        if (minorPopUp.transform.localScale == Vector3.one) return;

        Tween.Scale(minorPopUp.transform, Vector3.one, 0.1f);
    }

    public void HideMinorPopUp()
    {
        if (minorPopUp.transform.localScale == Vector3.zero) return;

        Tween.Scale(minorPopUp.transform, Vector3.zero, 0.1f);
    }

    private void UpdateSuitBorder()
    {
        if (card is MinorArcana minorArcana)
        {
            suitOutline.enabled = true;
            suitOutline.effectColor = GetSuitColour(minorArcana.Suit);
        }
        else
        {
            suitOutline.enabled = false;
        }
    }

    private Color GetSuitColour(Suit suit)
    {
        return suit switch
        {
            Suit.Wands => wandsColour,
            Suit.Pentacles => pentaclesColour,
            Suit.Cups => cupsColour,
            Suit.Swords => swordsColour,
            _ => Color.white
        };
    }

    public Sprite SetFrontSprite()
    {
        if (card is MinorArcana minor && card.CardSO is MinorArcanaSO minorSO)
        {
            return minor.Suit switch
            {
                Suit.Wands => minorSO.wandSprites[minor.Number - 1],
                Suit.Cups => minorSO.cupSprites[minor.Number - 1],
                Suit.Pentacles => minorSO.coinSprites[minor.Number - 1],
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
                Temperance => majorSO.temperanceSprite,
                TheEmperor => majorSO.emperorSprite,
                TheEmpress => majorSO.empressSprite,
                TheHermit => majorSO.hermitSprite,
                TheHierophant => majorSO.heirophantSprite,
                TheHighPriestess => majorSO.highPriestessSprite,
                TheLovers => majorSO.loversSprite,
                TheTower => majorSO.towerSprite,
                WheelofFortune => majorSO.wheelSprite,
                World => majorSO.worldSprite,
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