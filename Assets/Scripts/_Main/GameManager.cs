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
        
    }

    // Game setup
    //------------------------------------------------------------------------------------
    private void InstantiateCards()
    {
        for (int i = 0; i < 10; i++)
        {
            CardContainer card = Instantiate(cardContainerPrefab, Canvas.transform);
        }
    }

}
