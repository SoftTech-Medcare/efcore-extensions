namespace Zomp.EFCore.WindowFunctions.SqlServer.Tests;

[Collection(nameof(SqlServerCollection))]
public class MaxTests : TestBase
{
    private readonly Testing.MaxTests maxTests;

    public MaxTests(ITestOutputHelper output)
        : base(output)
    {
        maxTests = new Testing.MaxTests(DbContext);
    }

    [Fact]
    public void SimpleMax() => maxTests.SimpleMax();

    [Fact]
    public void MaxWithOrder() => maxTests.MaxWithOrder();

    [Fact]
    public void SimpleMaxNullable() => maxTests.SimpleMaxNullable();

    [Fact]
    public void MaxBetweenCurrentRowAndOne() => maxTests.MaxBetweenCurrentRowAndOne();

    [Fact]
    public void MaxBetweenTwoPreceding() => maxTests.MaxBetweenTwoPreceding();

    [Fact]
    public void MaxBetweenTwoFollowing() => maxTests.MaxBetweenTwoFollowing();

    [Fact]
    public void MaxBetweenFollowingAndUnbounded() => maxTests.MaxBetweenFollowingAndUnbounded();

    [Fact]
    public void SimpleMaxWithCast() => maxTests.SimpleMaxWithCast();

    [Fact]
    public void MaxWithCastToString() => maxTests.MaxWithCastToString();

    [Fact]
    public void MaxWithPartition() => maxTests.MaxWithPartition();

    [Fact]
    public void MaxWith2Partitions() => maxTests.MaxWith2Partitions();

    [Fact]
    public void MaxBinary() => maxTests.MaxBinary();
}