using System.Collections;
using UnityEngine;

public class PlayCard : IAction
{
    public Card Card {get; private set;}

    public PlayCard(Card card)
    {
        Card = card;
    }

    public IEnumerator Execute()
    {
        if (GameManager.Instance.SacrificeArea.HasPendingCards)
        {
            GameManager.Instance.SacrificeArea.ReturnAllCardsToHand();
        }

        if (Card is MinorArcana)
        {
            yield return PlayMinorArcana();
        } else if (Card is MajorArcana majorArcana)
        {
            yield return PlayMajorArcana(majorArcana);
        }
    }

    public IEnumerator PlayMinorArcana()
    {
        PlayArea playArea = GameManager.Instance.PlayArea;

        if (playArea.Cards.Count == 0 && Card.Number == 1)
        {
            Card.EffectiveNumber = 15;
        } else
        {
            Card.EffectiveNumber = Card.Number;
        }

        yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(Card, playArea));

        yield return GameManager.Actions.ExecuteImmediate(new MillCards(GameManager.Instance.Doom));

        GameManager.Instance.IncreaseScore(1);

        GameManager.Actions.AddAction(new EndTurn());
    }

    public IEnumerator PlayMajorArcana(MajorArcana arcana)
    {
        if (arcana.CanPlay() == false) yield break;

        yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(arcana, GameManager.Instance.ArcanaPlayArea));
        GameManager.Instance.ArcanaPlayArea.UpdateVisuals();

        SoundFXManager.Instance.PlaytarotSoundClip(GameManager.Instance.transform);

        GameManager.Instance.Deck.ShowDeathCount(false);

        yield return new WaitForSeconds(0.5f);

        yield return GameManager.Actions.ExecuteImmediate(new GainFate(-arcana.FateCost));

        yield return arcana.ExecuteEffect();

        yield return new WaitForSeconds(0.5f);

        yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(arcana, GameManager.Instance.TarotDiscardPile));

        GameManager.Actions.AddAction(new EndTurn());

        GameManager.Instance.Deck.ShowDeathCount(true);
    }
}
