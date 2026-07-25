using System.Collections;
using UnityEngine;

public class Moon : MajorArcana
{
    public Moon(CardSO cardSO) : base(cardSO)
    {
        Name = "The Moon";
        FateCost = 2;
        Text = "Transform all cards in your hand into Wands.";
    }

    public override IEnumerator ExecuteEffect()
    {
        SoundFXManager.Instance.PlayMoonSoundClip(GameManager.Instance.transform);
        yield return TransformHandIntoSuit(Suit.Wands);
    }
}

