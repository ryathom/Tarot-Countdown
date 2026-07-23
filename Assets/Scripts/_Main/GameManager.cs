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
    public PlayArea PlayArea;
    public DiscardPile DiscardPile;

    [Header("Cards")]
    public CardContainer cardContainerPrefab;
    public CardSO testCard;
    public CardSO deathCard;

    public int Fate {get; private set;}
    public int Doom {get; private set;}
    public int Turn {get; private set;}
    public int HandSize {get => startingHandSize;}

    private readonly int startingHandSize = 5;
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
        PrimeTweenConfig.SetTweensCapacity(1600);
        Actions = new();

        InstantiateCards();
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
            for (int j = 1; j < 11; j++)
            {
                CardContainer cardContainer = Instantiate(cardContainerPrefab, Canvas.transform);
                MinorArcana card = new(testCard, j, (Suit)i);

                cardContainer.SetCard(card);
                Deck.AddCard(card);
            }            
        }

        Deck.Shuffle();

        CardContainer deathContainer = Instantiate(cardContainerPrefab, Canvas.transform);
        Death death = new(deathCard);

        deathContainer.SetCard(death);
        Deck.InsertCard(death, startingDeathPosition - 1);
    }

    // Gameplay
    //------------------------------------------------------------------------------------
    public void StartGame()
    {
        for (int i = 0; i < startingHandSize; i++)
        {
            Actions.AddAction(new DrawCard());
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

    public void EndTurn()
    {
        Actions.AddAction(new EndTurn());
    }

    public void DecrementTurn()
    {
        Turn -= 1;
    }
}
