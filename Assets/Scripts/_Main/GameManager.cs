using PrimeTween;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    [Header("Zones")]
    public Canvas Canvas;
    public Deck Deck;
    public Hand Hand;
    public PlayArea PlayArea;
    public DiscardPile DiscardPile;

    [Header("Cards")]
    public CardContainer cardContainerPrefab;
    public CardSO testCard;
    public CardSO deathCard;

    public readonly int startingHandSize = 3;

    // Unity methods
    //------------------------------------------------------------------------------------
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(this.gameObject);
        }
    }

    public void Start()
    {
        PrimeTweenConfig.SetTweensCapacity(1600);

        InstantiateCards();
        StartGame();
    }

    // Game setup
    //------------------------------------------------------------------------------------
    private void InstantiateCards()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 1; j < 11; j++)
            {
                CardContainer cardContainer = Instantiate(cardContainerPrefab, Canvas.transform);
                MinorArcana card = new(testCard, j, (Suit)i);

                cardContainer.SetCard(card);
                Deck.AddCard(card);
            }            
        }

        Deck.Shuffle();
        Debug.Log(Deck.Cards.Count);
    }

    // Gameplay
    //------------------------------------------------------------------------------------
    public void StartGame()
    {
        for (int i = 0; i < startingHandSize; i++)
        {
            DrawCard();
        }
    }

    public void DrawCard()
    {
        Card card = Deck.Cards[0];
        Deck.RemoveCard(card);
        Hand.AddCard(card);
    }

    public void PlayCard(Card card)
    {
        Hand.RemoveCard(card);
        PlayArea.AddCard(card);
    }

    public void DiscardCard(Card card)
    {
        card.Zone.RemoveCard(card);
        DiscardPile.AddCard(card);
    }
}
