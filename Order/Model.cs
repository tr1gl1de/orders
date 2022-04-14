using System.ComponentModel.DataAnnotations.Schema;

namespace Orders.Order;

[Table("orders")]
public class OrderEntity
{
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("city_sender")]
    public string CitySender { get; set; } = string.Empty;
    
    [Column("address_sender")]
    public string AddressSender { get; set; } = string.Empty;
    
    [Column("city_receiver")]
    public string CityReceiver { get; set; }= string.Empty;
    
    [Column("address_receiver")]
    public string AddressReceiver { get; set; }= string.Empty;
    
    [Column("weight")]
    public double Weight { get; set; }
    
    [Column("date_receiving")]
    public DateTime DateReceiving { get; set; }
}