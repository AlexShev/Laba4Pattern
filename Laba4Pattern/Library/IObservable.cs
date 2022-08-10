namespace library_laba4
{
    public interface IObservable
    {
        void AddObserver(IObserver o, int days);
        void RemoveObserver(IObserver o);
        void NotifyObservers();
        void NotifyAllObservers();
    }
}