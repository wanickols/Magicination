namespace MGCNTN
{
    public abstract class SelectionManager
    {
        public virtual Selector CurrentSelector { get; set; }
        public abstract void Accept();
        public abstract void Cancel();
        public abstract void checkHover();
    }
}