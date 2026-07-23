public class DiscardPile : Zone
{
    protected override void ClickCard(Card card)
    {
        UIManager.Instance.OpenBrowser(this);
    }
}