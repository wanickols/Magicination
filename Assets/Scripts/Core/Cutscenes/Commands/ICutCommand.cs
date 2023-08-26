
using System.Collections;

namespace MGCNTN
{
    public interface ICutCommand
    {
        IEnumerator CO_Execute();
        bool isFinished { get; }
    }
}
