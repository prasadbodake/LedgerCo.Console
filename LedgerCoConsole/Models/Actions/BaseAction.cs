namespace LedgerCo.Models.Actions
{
    internal abstract class BaseAction
    {
        public abstract ActionType Type { get; }

        public abstract BaseAction Parse(string line);       
    }
}
