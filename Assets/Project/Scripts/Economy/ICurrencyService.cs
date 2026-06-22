namespace Assets.Project.Scripts.Economy
{
    internal interface ICurrencyService
    {
        void AddCoins(int amount);
        bool SpendCoins(int amount);
        int GetCurrentCoins();
    }
}
