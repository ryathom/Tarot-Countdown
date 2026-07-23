using UnityEngine;

public class PlayArea : Zone
{
    private readonly float cardSpacing = 244;
    private readonly Vector3 hoverScale = new(1.1f, 1.1f, 1f);

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
            Cards[i].Container.ShowPopUp(false);
            Cards[i].Container.SetScale(Vector3.one);
        }
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
