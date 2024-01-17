namespace Pip_Boy.Sounds
{
    internal class PipBoy
    {
        #region Constructors
        public PipBoy()
        {

        }

        public PipBoy(int width, int height)
        {
            Console.BufferWidth = width;
            Console.BufferHeight = height;
        }
        #endregion

        enum MenuPage
        {
            STAT,
            INV,
            DATA,
            MAP,
            RADIO
        }
    }
}
