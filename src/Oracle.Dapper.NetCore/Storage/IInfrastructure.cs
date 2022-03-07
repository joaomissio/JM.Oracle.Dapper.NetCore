namespace Oracle.Dapper.NetCore.Storage
{
    public interface IInfrastructure<out T>
    {
        /// <summary>
        ///     Gets the value of the property being hidden.
        /// </summary>
        T Instance { get; }
    }
}
