using UnityEngine;

public class Hand : Zone
{
    private readonly float cardSpacing = 244;
    private int yScale = 0;

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
            Cards[i].Container.SetTargetPosition(this.transform.position + (Vector3)targetPosition);
            Cards[i].Container.ShowVisual(true);
        }
    }

    // Gameplay
    //---------------------------------------------------------------------------------------------------------
    protected override void ClickCard(Card card)
    {
        GameManager.Instance.PlayCard(card);
    }

    protected override void RightClickCard(Card card)
    {
        GameManager.Instance.DiscardCard(card);
    }
}
