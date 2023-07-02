using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zomp.EFCore.WindowFunctions.Npgsql.Query.Internal;

public class WindowFunctionsNpgsqlParameterBasedSqlProcessor : NpgsqlParameterBasedSqlProcessor
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WindowFunctionsSqlServerParameterBasedSqlProcessor"/> class.
    /// </summary>
    /// <param name="dependencies">Service dependencies.</param>
    /// <param name="useRelationalNulls">A bool value indicating if relational nulls should be used.</param>
    public WindowFunctionsNpgsqlParameterBasedSqlProcessor(RelationalParameterBasedSqlProcessorDependencies dependencies, bool useRelationalNulls)
        : base(dependencies, useRelationalNulls)
    {
    }

    /// <inheritdoc/>
#if !EF_CORE_6
    protected override Expression ProcessSqlNullability(Expression selectExpression, IReadOnlyDictionary<string, object?> parametersValues, out bool canCache)
        => new WindowFunctionsNpgsqlSqlNullabilityProcessor(Dependencies, UseRelationalNulls).Process(
            selectExpression, parametersValues, out canCache);
#else
    protected override SelectExpression ProcessSqlNullability(SelectExpression selectExpression, IReadOnlyDictionary<string, object?> parametersValues, out bool canCache)
        => new WindowFunctionsNpgsqlSqlNullabilityProcessor(Dependencies, UseRelationalNulls).Process(
            selectExpression, parametersValues, out canCache);
#endif
}
