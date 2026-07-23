using UnityEngine;

public class TarotHandArea : Zone
{
    private readonly float cardSpacing = 250;

    // Methods
    //---------------------------------------------------------------------------------------------------------
    public override void UpdateVisuals()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            float relativePosition = i - ((Cards.Count - 1f) / 2f);
            
            float y = relativePosition * cardSpacing * -1;

            Vector2 targetPosition = new(0, y);

            Cards[i].Container.transform.SetAsLastSibling();
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
}
