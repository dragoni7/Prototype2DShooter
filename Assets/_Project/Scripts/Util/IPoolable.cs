using System;

namespace Utils
{
    public interface IPoolable<T>
    {
        void Initialize(Action<T> returnAction);

        void ReturnToPool();
    }
}
