using System.Collections;

namespace MGCNTN.Battle
{
    public interface ICommand
    {
        bool isFinished { get; }
        public IEnumerator Co_Execute();
    }
}
