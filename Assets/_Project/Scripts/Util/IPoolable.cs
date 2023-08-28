using System;

namespace Util
{
    public interface IPoolable<T>
    {
        void Initialize(Action<T> returnAction);

        void ReturnToPool();
    }
}
