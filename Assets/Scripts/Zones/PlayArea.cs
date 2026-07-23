using UnityEngine;

public class PlayArea : Zone
{
    private readonly float cardSpacing = 244;

    // Methods
    //---------------------------------------------------------------------------------------------------------
    public override void UpdateVisuals()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            float relativePosition = i - ((Cards.Count - 1f) / 2f);
            
            float x = relativePosition * cardSpacing;

            Vector2 targetPosition = new(x, 0);

            Cards[i].Container.transform.SetAsLastSibling();
            Cards[i].Container.SetTargetPosition(this.transform.position + (Vector3)targetPosition);
            Cards[i].Container.ShowVisual(true);
        }
    }

    protected override void ClickCard(Card card)
    {
        GameManager.Actions.AddAction(new UnplayCard(card));
    }
}
