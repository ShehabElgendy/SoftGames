namespace SoftGames.PhoenixFlame
{
    /// <summary>
    /// Pure C# class for color cycling logic.
    /// No Unity dependencies - fully testable in EditMode.
    /// </summary>
    public class ColorCycleModel
    {
        private static readonly string[] ColorNames = { "Orange", "Green", "Blue" };

        private int currentIndex;

        public int CurrentIndex => currentIndex;
        public string CurrentColorName => ColorNames[currentIndex];
        public int ColorCount => ColorNames.Length;

        public ColorCycleModel(int startIndex = 0)
        {
            currentIndex = startIndex % ColorNames.Length;
        }

        public void CycleNext()
        {
            currentIndex = (currentIndex + 1) % ColorNames.Length;
        }

        public void CyclePrevious()
        {
            currentIndex = (currentIndex - 1 + ColorNames.Length) % ColorNames.Length;
        }

        public void SetIndex(int index)
        {
            currentIndex = index % ColorNames.Length;
        }

        public void Reset()
        {
            currentIndex = 0;
        }
    }
}
