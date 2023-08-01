
using System.Collections;

namespace Core
{
    public interface ICutCommand
    {
        IEnumerator CO_Execute();
        bool isFinished { get; }
    }
}
