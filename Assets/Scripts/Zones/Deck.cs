using UnityEngine;

public class Deck : Zone
{
    private readonly Vector3 hoverScale = new(1.2f, 1.2f, 1f);

    public override void AddCard(Card card)
    {
        base.AddCard(card);
        card.SetFaceUp(false);
    }

    protected override void ClickCard(Card card)
    {
        UIManager.Instance.OpenBrowser(this);
    }

    public override void InsertCard(Card card, int position)
    {
        base.InsertCard(card, position);
        card.SetFaceUp(false);
    }

    public override void UpdateVisuals()
    {
        for (int i = Cards.Count - 1; i >= 0; i--)
        {
            Card card = Cards[i];
            card.Container.SetTargetPosition(this.transform.position);
            card.Container.transform.SetParent(this.transform);
            card.Container.transform.SetAsLastSibling();
        }
    }

    public void Shuffle()
    {
        for (int i = 0; i < Cards.Count; i++) 
        {
            Card temp = Cards[i];
            int randomIndex = Random.Range(i, Cards.Count);
            Cards[i] = Cards[randomIndex];
            Cards[randomIndex] = temp;
        }

        UpdateVisuals();
    }

    public int DeathCardPosition()
    {
        foreach(Card card in Cards)
        {
            if (card is Death)
            {
                return Cards.IndexOf(card);
            }
        }

        return -1;
    }

    public int DeathCount()
    {
        int count = 0;

        foreach (Card card in Cards)
        {
            if (card is Death) count++;
        }

        return count;
    }

    protected override void EnterContainer(CardContainer container)
    {
        container.SetScale(hoverScale);
        
        if (container.Card is Death) container.ShowPopUp(true);
    }

    protected override void ExitContainer(CardContainer container)
    {
        container.SetScale(Vector3.one);
        container.ShowPopUp(false);
    }
}