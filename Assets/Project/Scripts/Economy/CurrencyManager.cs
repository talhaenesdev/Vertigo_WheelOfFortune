namespace Assets.Project.Scripts.Economy
{
    internal class CurrencyManager
    {
        public int Coins { get; private set; }

        public void AddCoins(int amount)
        {
            Coins += amount;
        }

        public bool SpendCoins(int amount)
        {
            if (Coins >= amount)
            {
                Coins -= amount;
                return true;
            }
            return false;
        }
    }
}
