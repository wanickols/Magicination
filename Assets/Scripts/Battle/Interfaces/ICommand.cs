using System.Collections;

namespace Battle
{
    public interface ICommand
    {
        bool isFinished { get; }
        public IEnumerator Co_Execute();
    }
}
