namespace SoftGames.Core
{
    // ============================================
    // SCENE EVENTS
    // ============================================

    public struct SceneLoadStartedEvent : IEvent
    {
        public int SceneIndex;
        public string SceneName;
    }

    public struct SceneLoadedEvent : IEvent
    {
        public int SceneIndex;
        public string SceneName;
    }

    // ============================================
    // ACE OF SHADOWS EVENTS
    // ============================================

    public struct CardMoveStartedEvent : IEvent
    {
        public int CardIndex;
        public int FromStackId;
        public int ToStackId;
    }

    public struct CardMoveCompletedEvent : IEvent
    {
        public int CardIndex;
        public int ToStackId;
        public int RemainingCards;
    }

    public struct AllCardsMovedEvent : IEvent
    {
        public int TotalCardsMoved;
    }

    // ============================================
    // MAGIC WORDS EVENTS
    // ============================================

    public struct DialogueLoadStartedEvent : IEvent { }

    public struct DialogueLoadedEvent : IEvent
    {
        public int DialogueCount;
    }

    public struct DialogueLoadErrorEvent : IEvent
    {
        public string ErrorMessage;
    }

    public struct DialogueDisplayedEvent : IEvent
    {
        public int Index;
        public string CharacterName;
    }
}
