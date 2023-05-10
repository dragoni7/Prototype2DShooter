using System;

namespace dragoni7
{
    public interface IPoolable<T>
    {
        void Initialize(Action<T> returnAction);

        void ReturnToPool();
    }
}
