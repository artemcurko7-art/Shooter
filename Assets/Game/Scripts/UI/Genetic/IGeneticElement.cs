namespace Game.Scripts.UI.Genetic
{
    public interface IGeneticElement
    {
        void Refresh();
    }

    public interface IIndexedGeneticElement : IGeneticElement
    {
        int Index { get; }
    }
}