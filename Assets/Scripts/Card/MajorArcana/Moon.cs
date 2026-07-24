using System.Collections;
using UnityEngine;

public class Moon : MajorArcana
{
    public Moon(CardSO cardSO) : base(cardSO)
    {
        Name = "The Moon";
        FateCost = 4;
        Text = "Shuffle all Wands from your discard pile back into your deck.";
    }

    public override IEnumerator ExecuteEffect()
    {
        SoundFXManager.Instance.PlayMoonSoundClip(GameManager.Instance.transform);
        yield return ShuffleBackSuit(Suit.Wands);
    }
}

