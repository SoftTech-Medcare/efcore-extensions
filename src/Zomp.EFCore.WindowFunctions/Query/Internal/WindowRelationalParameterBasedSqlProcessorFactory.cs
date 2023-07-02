namespace Zomp.EFCore.WindowFunctions.Query.Internal;

/// <summary>
/// Factory for producing <see cref="WindowRelationalParameterBasedSqlProcessor{T}"/> instances.
/// </summary>
/// <typeparam name="TParameterBasedSqlProcessor"> RelationalParameterBasedSqlProcessor Type. </typeparam>
public class WindowRelationalParameterBasedSqlProcessorFactory<TParameterBasedSqlProcessor> : RelationalParameterBasedSqlProcessorFactory
    where TParameterBasedSqlProcessor : RelationalParameterBasedSqlProcessor
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WindowRelationalParameterBasedSqlProcessorFactory{TParameterBasedSqlProcessor}"/> class.
    /// </summary>
    /// <param name="dependencies">Relational Parameter Based Sql ProcessorDependencies.</param>
    public WindowRelationalParameterBasedSqlProcessorFactory(RelationalParameterBasedSqlProcessorDependencies dependencies)
        : base(dependencies)
    {
    }

    /// <inheritdoc/>
    public override RelationalParameterBasedSqlProcessor Create(bool useRelationalNulls)
        => (TParameterBasedSqlProcessor)Activator.CreateInstance(typeof(TParameterBasedSqlProcessor), Dependencies, useRelationalNulls);
}