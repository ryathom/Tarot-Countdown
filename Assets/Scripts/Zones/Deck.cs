public class Deck : Zone
{
    protected override void ClickCard(Card card)
    {
        GameManager.Instance.DrawCard(card);
    }
}