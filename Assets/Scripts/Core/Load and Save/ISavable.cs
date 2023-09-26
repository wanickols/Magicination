public interface ISavable
{
    public abstract string CustomPath { get; }
    public abstract string ErrorMessage { get; }
    public abstract bool SaveData();
    public abstract bool LoadData();
}
