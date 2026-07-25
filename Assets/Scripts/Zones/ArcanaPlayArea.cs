public class ArcanaPlayArea : Zone
{
    public override void UpdateVisuals()
    {
        base.UpdateVisuals();

        foreach (Card card in Cards)
        {
            card.Container.SetScale(new(1.25f, 1.25f, 1));
            card.Container.ShowPopUp(false);
        }
    }
}
