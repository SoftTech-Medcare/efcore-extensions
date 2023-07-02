namespace Zomp.EFCore.Testing;

public record TestRow(int Id, int? Col1, long Col2, long? Col3, Guid SomeGuid, DateTime Date, byte[] IdBytes);