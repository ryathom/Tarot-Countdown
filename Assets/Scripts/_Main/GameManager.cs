using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    [Header("Zones")]
    public Canvas Canvas;
    public Deck Deck;
    public Hand Hand;

    [Header("Cards")]
    public CardContainer cardContainerPrefab;
    public CardSO testCard;
    public CardSO deathCard;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Poo");

        InstantiateCards();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(InputManager.Instance.GetPointInput());
    }

    // Game setup
    //------------------------------------------------------------------------------------
    private void InstantiateCards()
    {
        for (int i = 0; i < 10; i++)
        {
            CardContainer cardContainer = Instantiate(cardContainerPrefab, Canvas.transform);
            Card card = new(testCard);

            cardContainer.SetCard(card);
            Deck.AddCard(card);
        }
    }

}
