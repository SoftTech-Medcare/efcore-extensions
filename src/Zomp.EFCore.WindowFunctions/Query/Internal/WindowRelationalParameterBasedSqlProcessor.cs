namespace Zomp.EFCore.WindowFunctions.Query.Internal;

/// <summary>
/// Processes select expression.
/// </summary>
/// <typeparam name="T"> SqlNullabilityProcessor Type. </typeparam>
public class WindowRelationalParameterBasedSqlProcessor<T> : RelationalParameterBasedSqlProcessor
    where T : SqlNullabilityProcessor
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WindowRelationalParameterBasedSqlProcessor{T}"/> class.
    /// </summary>
    /// <param name="dependencies">RelationalParameterBasedSqlProcessorDependencies.</param>
    /// <param name="useRelationalNulls">A bool value indicating if relational nulls should be used.</param>
    public WindowRelationalParameterBasedSqlProcessor(RelationalParameterBasedSqlProcessorDependencies dependencies, bool useRelationalNulls)
        : base(dependencies, useRelationalNulls)
    {
    }

    /// <inheritdoc/>
#if !EF_CORE_6
    protected override Expression ProcessSqlNullability(Expression queryExpression, IReadOnlyDictionary<string, object?> parametersValues, out bool canCache)
        => ((T)Activator.CreateInstance(typeof(T), Dependencies, UseRelationalNulls)).Process(queryExpression, parametersValues, out canCache);
#else
    protected override SelectExpression ProcessSqlNullability(SelectExpression selectExpression, IReadOnlyDictionary<string, object?> parametersValues, out bool canCache)
        => ((T)Activator.CreateInstance(typeof(T), Dependencies, UseRelationalNulls)).Process(selectExpression, parametersValues, out canCache);
#endif
}