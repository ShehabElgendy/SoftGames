using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using SoftGames.Core;

namespace SoftGames.AceOfShadows
{
    /// <summary>
    /// Main orchestrator for Ace of Shadows.
    /// Creates cards, manages stacks, coordinates animations.
    /// Uses EventBus for decoupled communication.
    /// </summary>
    public class CardStackManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CardStack leftStack;
        [SerializeField] private CardStack rightStack;
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private AceOfShadowsConfig config;
        [SerializeField] private CardAnimator cardAnimator;

        [Header("Completion")]
        [SerializeField] private GameObject completionPanel;

        [Header("Events (Optional - also uses EventBus)")]
        public UnityEvent OnAllAnimationsComplete;

        private CardStack sourceStack;
        private CardStack targetStack;
        private bool isAnimating = false;
        private Coroutine moveCoroutine;
        private int totalCardsMoved = 0;

        private void Start()
        {
            Initialize();
        }

        /// <summary>
        /// Initialize the game: create cards and start move cycle.
        /// </summary>
        public void Initialize()
        {
            // Stop any existing coroutine
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            // Clear any existing cards
            ClearAllCards();

            // Configure animator
            if (cardAnimator != null && config != null)
            {
                cardAnimator.Configure(config.moveDuration, config.moveEaseType, config.moveArcHeight);
            }

            // Configure stack offsets
            if (config != null)
            {
                leftStack.SetCardOffset(config.cardStackOffset);
                rightStack.SetCardOffset(config.cardStackOffset);
            }

            // Create all cards in left stack
            int totalCards = config != null ? config.totalCards : 144;
            for (int i = 0; i < totalCards; i++)
            {
                GameObject cardObj = Instantiate(cardPrefab, leftStack.CardContainer);
                cardObj.name = $"Card_{i:D3}";

                CardController card = cardObj.GetComponent<CardController>();
                if (card != null)
                {
                    leftStack.AddCard(card);
                }
            }

            // Set initial source and target
            sourceStack = leftStack;
            targetStack = rightStack;
            totalCardsMoved = 0;

            // Hide completion panel
            if (completionPanel != null)
                completionPanel.SetActive(false);

            // Start the move cycle
            moveCoroutine = StartCoroutine(MoveCardsCycle());
        }

        /// <summary>
        /// Main loop: move one card every interval.
        /// Waits for animation to complete before starting next move.
        /// </summary>
        private IEnumerator MoveCardsCycle()
        {
            float interval = config != null ? config.moveInterval : 1f;
            WaitForSeconds waitInterval = new WaitForSeconds(interval);

            while (sourceStack.CardCount > 0)
            {
                yield return waitInterval;

                // Wait for current animation to complete (prevents race condition)
                while (isAnimating)
                {
                    yield return null;
                }

                MoveTopCard();
            }

            // Wait for final animation to complete
            while (isAnimating)
            {
                yield return null;
            }

            // All cards moved - show completion
            OnAnimationsFinished();
        }

        /// <summary>
        /// Move the top card from source to target stack.
        /// </summary>
        private void MoveTopCard()
        {
            CardController card = sourceStack.RemoveTopCard();
            if (card == null) return;

            isAnimating = true;
            int cardIndex = totalCardsMoved;

            // Publish event: card move started
            EventBus.Publish(new CardMoveStartedEvent
            {
                CardIndex = cardIndex,
                FromStackId = sourceStack.StackId,
                ToStackId = targetStack.StackId
            });

            // Get target position in world space
            Vector3 targetWorldPos = targetStack.GetNextCardWorldPosition();
            int targetOrder = targetStack.GetNextSortingOrder();

            // Animate the move
            cardAnimator.AnimateCardToPosition(card, targetWorldPos, targetOrder, () =>
            {
                // Add to target stack after animation
                targetStack.AddCard(card);

                isAnimating = false;
                totalCardsMoved++;

                // Publish event: card move completed
                EventBus.Publish(new CardMoveCompletedEvent
                {
                    CardIndex = cardIndex,
                    ToStackId = targetStack.StackId,
                    RemainingCards = sourceStack.CardCount
                });

                // Swap stacks after each card (alternating)
                SwapStacks();
            });
        }

        /// <summary>
        /// Swap source and target stacks.
        /// </summary>
        private void SwapStacks()
        {
            (sourceStack, targetStack) = (targetStack, sourceStack);
        }

        /// <summary>
        /// Called when all animations are complete.
        /// </summary>
        private void OnAnimationsFinished()
        {
            // Publish event via EventBus
            EventBus.Publish(new AllCardsMovedEvent
            {
                TotalCardsMoved = totalCardsMoved
            });

            if (completionPanel != null)
            {
                completionPanel.SetActive(true);

                // Animate panel pop-in
                completionPanel.transform.localScale = Vector3.zero;
                completionPanel.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
            }

            // Also invoke UnityEvent for Inspector-wired callbacks
            OnAllAnimationsComplete?.Invoke();
        }

        private void ClearAllCards()
        {
            // Clear left stack
            if (leftStack != null && leftStack.CardContainer != null)
            {
                foreach (Transform child in leftStack.CardContainer)
                {
                    Destroy(child.gameObject);
                }
                leftStack.ClearCards();
            }

            // Clear right stack
            if (rightStack != null && rightStack.CardContainer != null)
            {
                foreach (Transform child in rightStack.CardContainer)
                {
                    Destroy(child.gameObject);
                }
                rightStack.ClearCards();
            }
        }

        /// <summary>
        /// Reset and restart the demo.
        /// </summary>
        public void Restart()
        {
            StopAllCoroutines();
            DOTween.KillAll();
            Initialize();
        }

        private void OnDestroy()
        {
            DOTween.KillAll();
        }
    }
}
