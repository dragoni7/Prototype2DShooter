namespace dragoni7
{
    public interface IPool<T>
    {
        T Pull();

        void Push(T t);
    }
}
