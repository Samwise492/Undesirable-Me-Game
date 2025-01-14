using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CardPuzzle
{
    public class CardPuzzleController : BasePuzzle
    {
        [Header("Component Data")]
        [SerializeField]
        private Card cardPrefab;

        [SerializeField]
        private HorizontalLayoutGroup layoutPrefab;

        [SerializeField]
        private CardColourData colourDatabase;

        [Header("Map Data")]
        [SerializeField]
        private CanvasGroup map;

        [SerializeField]
        private Transform spawnPoint;

        [SerializeField]
        private int rowNumber;

        [SerializeField]
        private int cardsInRowNumber;

        private Card currentSelectedCard;

        private List<Card> cards = new();
        private List<CardColourData.CardColourKeys> sequence = new();

        private List<HorizontalLayoutGroup> layouts = new();

        private int currentCardIndex;
        private int sequenceIndex;

        private bool wasSelected;
        private bool isDebugCardsShown;
        private bool isUnselectionStarted;

        private void OnDestroy()
        {
            foreach (Card card in cards)
            {
                card.OnSelected -= CheckCardSelection;
                card.OnSelected -= PairCards;
            }
        }

        private void Update()
        {
            if (isDebugOnStart)
            {
                if (Input.GetKeyDown(InputData.debugKey_One))
                {
                    DebugShowAllCards();
                }
            }
        }
        private void DebugShowAllCards()
        {
            isDebugCardsShown = !isDebugCardsShown;

            foreach (Card card in cards)
            {
                card.SetImage(isDebugCardsShown);
            }
        }

        public override void StartPuzzle()
        {
            base.StartPuzzle();

            if ((rowNumber * cardsInRowNumber) % 2 != 0)
            {
                Debug.LogError("Card number is odd.");
                return;
            }

            ResetValues();
            CreateCards();

            foreach (Card card in cards)
            {
                card.OnSelected += CheckCardSelection;
                card.OnSelected += PairCards;
            }
        }

        private void CreateCards()
        {
            CreateRandomSequence();

            for (int i = 0; i < rowNumber; i++)
            {
                HorizontalLayoutGroup spawnedLayout = Instantiate(layoutPrefab, spawnPoint);
                layouts.Add(spawnedLayout);

                for (int j = 0; j < cardsInRowNumber; j++)
                {
                    Card spawnedCard = Instantiate(cardPrefab, spawnedLayout.transform);
                    spawnedCard.SetKey(sequence[currentCardIndex], 
                        colourDatabase.Data.Where(x => x.ColourKey == sequence[currentCardIndex]).First().Colour);

                    cards.Add(spawnedCard);

                    currentCardIndex++;
                }
            }
        }

        private void CreateRandomSequence()
        {
            List<CardColourData.CardColourKeys> addedKeys = new();
            RegenerateAvailableColours(addedKeys);

            for (int i = 0; i < (rowNumber * cardsInRowNumber) / 2; i++)
            {
                CardColourData.CardColourKeys randomKey = colourDatabase.Data[sequenceIndex].ColourKey;
                sequenceIndex++;
                addedKeys.Remove(randomKey);

                sequence.Add(randomKey);

                if (addedKeys.Count == 0)
                {
                    RegenerateAvailableColours(addedKeys);
                }
            }

            sequence.AddRange(sequence);
            sequence.Shuffle();
        }
        private void RegenerateAvailableColours(List<CardColourData.CardColourKeys> addedKeys)
        {
            sequenceIndex = 0;

            for (int i = 0; i < colourDatabase.Data.Length; i++)
            {
                addedKeys.Add(colourDatabase.Data[i].ColourKey);
            }
        }

        private void CheckCardSelection(Card card, bool isSelected)
        {
            if (wasSelected)
            {
                return;
            }

            if (currentSelectedCard == null)
            {
                currentSelectedCard = card;
            }
            else
            {
                if (currentSelectedCard == card)
                {
                    wasSelected = true;
                    SetUnselection(false);
                }
            }
        }
        private void PairCards(Card card, bool isSelected)
        {
            if (isSelected && currentSelectedCard != null)
            {
                if (card != currentSelectedCard)
                {
                    if (currentSelectedCard.Key == card.Key)
                    {
                        cards.Remove(currentSelectedCard);
                        cards.Remove(card);

                        currentSelectedCard.Complete();
                        card.Complete();
                    }

                    SetUnselection(true);

                    if (cards.Count == 0)
                    {
                        StartCoroutine(DelayEnd());
                    }
                }
            }
        }

        private void ResetValues()
        {
            cards.Clear();
            sequence.Clear();
            ClearLayouts();

            wasSelected = false;
            isUnselectionStarted = false;

            currentCardIndex = 0;

            currentSelectedCard = null;
        }
        private void ClearLayouts()
        {
            foreach (HorizontalLayoutGroup layout in layouts)
            {
                Destroy(layout.gameObject);
            }

            layouts.Clear();
        }

        private void SetUnselection(bool isDelayed)
        {
            if (isDelayed)
            {
                if (isUnselectionStarted)
                {
                    return;
                }

                StartCoroutine(DelayUnselection());
            }
            else
            {
                UnselectAll();
            }
        }

        private void UnselectAll()
        {
            foreach (Card card in cards)
            {
                card.SetSelection(false);
            }

            currentSelectedCard = null;
            wasSelected = false;
            isUnselectionStarted = false;
        }

        private IEnumerator DelayUnselection()
        {
            isUnselectionStarted = true;
            SetMap(false);

            yield return new WaitForSeconds(1f);

            UnselectAll();
            SetMap(true);

            yield break;
        }
        private IEnumerator DelayEnd()
        {
            yield return new WaitForSeconds(1);

            EndPuzzle();

            yield break;
        }

        private void SetMap(bool value)
        {
            map.interactable = value;
        }
    }
}