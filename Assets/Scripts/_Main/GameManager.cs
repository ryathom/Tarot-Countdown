using System.Collections.Generic;
using PrimeTween;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public static ActionSystem Actions {get; private set;}

    [Header("Zones")]
    public Canvas Canvas;
    public Deck Deck;
    public HandArea Hand;
    public DiscardPile DiscardPile;
    public PlayArea PlayArea;
    public Deck TarotDeck;
    public HandArea TarotHand;
    public DiscardPile TarotDiscardPile;

    [Header("Cards")]
    public CardContainer cardContainerPrefab;
    public CardSO minorArcanaSO;
    public CardSO majorArcanaSO;
    public CardSO deathCardSO;

    public int Fate {get; private set;}
    public int Doom {get; private set;}
    public int Turn {get; private set;}

    public int HandSize = 5;
    public int TarotHandSize = 3;

    private readonly int startingDeathPosition = 30;
    private readonly int turnsToSurvive = 20;

    // Unity methods
    //------------------------------------------------------------------------------------
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(this.gameObject);
        }
    }

    public void Start()
    {
        PrimeTweenConfig.SetTweensCapacity(3200);
        Actions = new();

        InstantiateCards();
        InstantiateTarotCards();
        StartGame();
    }

    public void Update()
    {
        if (Actions == null) return;

        if (Actions.ActionQueue.Count > 0 && !Actions.Busy)
        {
            StartCoroutine(Actions.ExecuteNextAction());
        }
    }

    // Game setup
    //------------------------------------------------------------------------------------
    private void InstantiateCards()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 1; j < 15; j++)
            {
                MinorArcana card = InstantiateMinorArcana(j, (Suit)i);
                Deck.AddCard(card);
            }            
        }

        Deck.Shuffle();

        Death death = InstantiateDeathCard();
        Deck.InsertCard(death, startingDeathPosition - 1);
    }

    public Death InstantiateDeathCard()
    {
        CardContainer deathContainer = Instantiate(cardContainerPrefab, Canvas.transform);
        Death death = new(deathCardSO);

        deathContainer.SetCard(death);

        return death;
    }

    public MinorArcana InstantiateMinorArcana(int number, Suit suit)
    {
        CardContainer cardContainer = Instantiate(cardContainerPrefab, Canvas.transform);
        MinorArcana card = new(minorArcanaSO, number, suit);

        cardContainer.SetCard(card);
        return card;
    }

    private void InstantiateTarotCards()
    {
        List<MajorArcana> majorArcana = new()
        {
            new Fool(majorArcanaSO),
            new Devil(majorArcanaSO),
            // new Star(majorArcanaSO),
            // new Moon(majorArcanaSO),
            // new Sun(majorArcanaSO),
            // new World(majorArcanaSO),
            // new HangedMan(majorArcanaSO),
            new TheEmpress(majorArcanaSO),
            // new TheLovers(majorArcanaSO),
            // new TheTower(majorArcanaSO),
            // new TheHierophant(majorArcanaSO),
            // new TheHighPriestess(majorArcanaSO),
            // new Temperance(majorArcanaSO),
            // new TheEmperor(majorArcanaSO),
            // new Strength(majorArcanaSO),
            // new TheHermit(majorArcanaSO),
            // new Judgement(majorArcanaSO),
            // new WheelofFortune(majorArcanaSO),
            // new Magician(majorArcanaSO),
            // new Justice(majorArcanaSO),
            // new Chariot(majorArcanaSO),
        };

        foreach (MajorArcana arcana in majorArcana)
        {
            CardContainer cardContainer = Instantiate(cardContainerPrefab, Canvas.transform);

            cardContainer.SetCard(arcana);
            TarotDeck.AddCard(arcana);
        }

        TarotDeck.Shuffle();
    }

    public void DestroyCard(Card card)
    {
        card.Zone.RemoveCard(card);
        Destroy(card.Container.gameObject);
    }

    // Gameplay
    //------------------------------------------------------------------------------------
    public void StartGame()
    {
        for (int i = 0; i < HandSize; i++)
        {
            Actions.AddAction(new DrawCard());
        }

        for (int i = 0; i < TarotHandSize; i++)
        {
            Actions.AddAction(new DrawTarotCard());
        }

        Turn = turnsToSurvive;
    }

    public void GainFate(int gain)
    {
        Fate += gain;
    }

    public void GainDoom(int gain)
    {
        Doom += gain;
    }

    public void SetDoom(int doom)
    {
        Doom = doom;
    }

    public void EndTurn()
    {
        Actions.AddAction(new EndTurn());
    }

    public void DecrementTurn()
    {
        Turn -= 1;

        if (Turn == 0)
        {
            Debug.Log("You win!");
        }
    }
}
