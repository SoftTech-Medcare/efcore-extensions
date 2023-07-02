using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zomp.EFCore.WindowFunctions.Npgsql.Tests;

[Collection(nameof(NpgsqlCollection))]
public class NormalQueryTest : TestBase
{
    public NormalQueryTest(ITestOutputHelper output)
        : base(output)
    {

    }

    [Fact]
    public void NormalQuery_Should_Not_Affected__ANY()
    {
        int[] checkList = new int[] { 17, 2, 3 };
        var query = DbContext.TestRows.Where(x => checkList.Contains(x.Id));

        var result = query.ToList();
        //Assert.Equal(17, result.Count);
    }
}
