using System;
using NUnit.Framework;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Deck : Zone
{
    private readonly Vector3 hoverScale = new(1.2f, 1.2f, 1f);

    [SerializeField] private GameObject deckCounterField;
    [SerializeField] private TextMeshProUGUI deckCount;

    [SerializeField] private GameObject deathCounterField;
    [SerializeField] private TextMeshProUGUI deathCount;

    public Action<Card> OnClickCardInDeck;
    private bool canShowDeathCounter = false;
    private readonly Queue<int> deathPositionQueue = new();
    private Coroutine deathCountRoutine;
    private int queuedDeathPosition = -1;

    protected override void Start()
    {
        base.Start();
        deckCounterField.transform.localScale = Vector2.zero;

        if (deathCounterField != null)
        {
            deathCounterField.transform.localScale = Vector2.zero;

            Tween.Delay(3f, () =>
            {
                canShowDeathCounter = true;
                ShowDeathCount(true);
            });
        }
    }

    public override void AddCard(Card card)
    {
        base.AddCard(card);
        card.SetFaceUp(false);
    }

    protected override void ClickCard(Card card)
    {
        if (isBrowsing)
        {
            OnClickCardInDeck?.Invoke(card);
        } else
        {
            if (card is MajorArcana)
            {
                UIManager.Instance.OpenTarotBrowser();
            } else
            {
                UIManager.Instance.OpenBrowser(this);
            }
        }
    }

    public override void InsertCard(Card card, int position)
    {
        position = Mathf.Clamp(position, 0, Cards.Count);
        base.InsertCard(card, position);
        card.SetFaceUp(false);
    }

    public override void UpdateVisuals()
    {
        if (isBrowsing) return;

        for (int i = Cards.Count - 1; i >= 0; i--)
        {
            Card card = Cards[i];
            card.Container.SetTargetPosition(this.transform.position);
            card.Container.transform.SetParent(this.transform);
            card.Container.transform.SetAsLastSibling();
        }

        if (deathCounterField != null && canShowDeathCounter) ShowDeathCount(true); //DeathCardPosition() <= 10);
    }

    public void Shuffle()
    {
        foreach(Card card in Cards)
        {
            card.SetFaceUp(false);
        }

        for (int i = 0; i < Cards.Count; i++) 
        {
            Card temp = Cards[i];
            int randomIndex = UnityEngine.Random.Range(i, Cards.Count);
            Cards[i] = Cards[randomIndex];
            Cards[randomIndex] = temp;
        }

        UpdateVisuals();
    }

    public bool ContainsTarotCard(MajorArcana arcana)
    {
        foreach (Card card in Cards)
        {
            if (card is MajorArcana ma)
            {
                if (ma.GetType() == arcana.GetType())
                {
                    return true;
                }
            }
        }

        return false;
    }

    public int DeathCardPosition()
    {
        foreach(Card card in Cards)
        {
            if (card is Death)
            {
                return Cards.IndexOf(card) + 1;
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

    public void ShowDeckCount(bool enabled)
    {
        if (DeathCount() == 0) return;

        if (enabled)
        {
            Tween.Scale(deckCounterField.transform, Vector2.one, 0.1f);
        } else
        {
            Tween.Scale(deckCounterField.transform, Vector2.zero, 0.1f);
        }

        deckCount.text = Cards.Count + " cards";
    }

    public void ShowDeathCount(bool enabled)
    {
        if (enabled)
        {
            Tween.Scale(deathCounterField.transform, Vector2.one, 0.5f);
        }
        else
        {
            Tween.Scale(deathCounterField.transform, Vector2.zero, 0.1f);
        }

        int position = DeathCardPosition();

        if (position != queuedDeathPosition)
        {
            deathPositionQueue.Enqueue(position);
            queuedDeathPosition = position;

            if (deathCountRoutine == null)
            {
                deathCountRoutine = StartCoroutine(ProcessDeathCountQueue());
            }
        }
    }
    private void FlashDeathCounter()
    {
        Tween.StopAll(deathCounterField.transform);

        deathCounterField.transform.localScale = Vector3.one;

        Sequence.Create()
            .Group(Tween.ShakeLocalPosition(
                deathCounterField.transform,
                strength: new Vector3(8f, 0f, 0f),
                duration: 0.22f))
            .Chain(Tween.Scale(deathCounterField.transform, Vector3.one * 1.45f, 0.08f))
            .Chain(Tween.Scale(deathCounterField.transform, Vector3.one * 0.9f, 0.06f))
            .Chain(Tween.Scale(deathCounterField.transform, Vector3.one, 0.08f));
    }
    private IEnumerator ProcessDeathCountQueue()
    {
        while (deathPositionQueue.Count > 0)
        {
            int position = deathPositionQueue.Dequeue();

            deathCount.text = position.ToString();

            bool isMilestone =
                position == 10 ||
                position == 5 ||
                position == 4 ||
                position == 3 ||
                position == 2 ||
                position == 1;

            if (isMilestone)
            {
                FlashDeathCounter();
                yield return new WaitForSeconds(0.45f);
            }
            else
            {
                yield return new WaitForSeconds(0.05f);
            }
        }

        deathCountRoutine = null;
    }
    protected override void EnterContainer(CardContainer container)
    {
        container.SetScale(hoverScale);
        
        if (container.Card is Death) container.ShowPopUp(true);

        if (!isBrowsing)
        {
            ShowDeckCount(true);
        }
    }

    protected override void ExitContainer(CardContainer container)
    {
        container.SetScale(Vector3.one);
        container.ShowPopUp(false);
        ShowDeckCount(false);
    }
}