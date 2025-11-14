namespace OrderManagementSystem.Application.DTOs
{
    public record OrderSummaryDto(
        int OrderId,
        decimal Total,
        DateTime Timestamp,
        List<OrderSummaryItem> Items);

    public record OrderSummaryItem(string ProductName, int Quantity, decimal Price);
}
