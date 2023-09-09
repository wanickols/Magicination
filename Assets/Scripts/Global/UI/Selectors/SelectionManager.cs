namespace MGCNTN
{
    public abstract class SelectionManager
    {
        public virtual Selector CurrentSelector { get; set; }
        public abstract bool Accept();
        public abstract void Cancel();
        public abstract void checkHover();
    }
}