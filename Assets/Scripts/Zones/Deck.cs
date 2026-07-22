public class Deck : Zone
{
    public override void AddCard(Card card)
    {
        base.AddCard(card);
        card.SetFaceUp(false);
    }

    protected override void ClickCard(Card card)
    {
        GameManager.Instance.DrawCard(card);
    }
}