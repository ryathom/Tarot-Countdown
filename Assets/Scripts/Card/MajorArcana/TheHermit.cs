using System.Collections;
using UnityEngine;


public class TheHermit : MajorArcana
{
    public TheHermit(CardSO cardSO) : base(cardSO)
    {
        Name = "The Hermit";
        FateCost = 3;
        Text = "Discard all cards in your row. <i>(You do not gain Doom)</i>";
    }

    public override IEnumerator ExecuteEffect()
    {
        PlayArea playArea = GameManager.Instance.PlayArea;

        playArea.isScoring = true;
        while (playArea.Cards.Count > 0)
        {
            SoundFXManager.Instance.PlayDiscardSoundClip(playArea.Cards[0].Container.transform);

            yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(playArea.Cards[0], GameManager.Instance.DiscardPile));
        }
        playArea.isScoring = false;
        playArea.UpdateVisuals();
    }
}
