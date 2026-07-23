using UnityEngine;

public class HandArea : Zone
{
    [SerializeField] private float cardSpacing = 180;
    private int yScale = 0;

    private readonly Vector3 hoverScale = new(1.2f, 1.2f, 1f);

    // Methods
    //---------------------------------------------------------------------------------------------------------
    public override void UpdateVisuals()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            float relativePosition = i - ((Cards.Count - 1f) / 2f);
            
            float x = relativePosition * cardSpacing;

            float y = -1 - (relativePosition * relativePosition / (Cards.Count * 2));
            y *= yScale;


            Vector2 targetPosition = new(x, y);

            Cards[i].Container.transform.SetAsLastSibling();
            Cards[i].Container.transform.SetParent(this.transform);
            Cards[i].Container.SetTargetPosition(this.transform.position + (Vector3)targetPosition);
            Cards[i].Container.ShowVisual(true);
        }
    }

    // Gameplay
    //---------------------------------------------------------------------------------------------------------
    protected override void ClickCard(Card card)
    {
        GameManager.Actions.AddAction(new PlayCard(card));
    }

    protected override void EnterContainer(CardContainer container)
    {
        container.SetScale(hoverScale);
        container.ShowPopUp(true);
    }

    protected override void ExitContainer(CardContainer container)
    {
        container.SetScale(Vector3.one);
        container.ShowPopUp(false);
    }
}