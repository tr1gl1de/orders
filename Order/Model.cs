namespace Orders.Order;

public class OrderEntity
{
    public Guid Id { get; set; }
    public string CitySender { get; set; } = string.Empty;
    public string AdressSender { get; set; } = string.Empty;
    public string CityReceiver { get; set; }= string.Empty;
    public string AdressReceiver { get; set; }= string.Empty;
    public double Weight { get; set; }
    public DateTime DateReceiving { get; set; }
}