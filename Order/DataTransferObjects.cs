using System.ComponentModel.DataAnnotations;

namespace Orders.Order;

public class CreateOrderDto
{
    /// <summary>Город отправителя</summary>
    /// <example>Москва</example>
    [Required]
    [MaxLength(20)]
    [RegularExpression(@"^[a-zA-Zа-яА-Я]*[\s\-]?[a-zA-Zа-яА-Я]*[\.\s]?$")]
    public string CitySender { get; set; } = string.Empty;

    /// <summary>Адресс отправителя</summary>
    /// <example>Москва, ул. Пушкина , д.1, кв 23</example>
    [Required]
    [MaxLength(100)]
    [RegularExpression(@"[a-zA-Zа-яА-Я\,0-9\-\s\.]*$")]
    public string AddressSender { get; set; } = string.Empty;

    /// <summary>Город получателя</summary>
    /// <example>Подольск</example>
    [Required]
    [MaxLength(20)]
    [RegularExpression(@"^[a-zA-Zа-яА-Я]*[\s\-]?[a-zA-Zа-яА-Я]*[\.\s]?$")]
    public string CityReceiver { get; set; } = string.Empty;

    /// <summary>Адресс получателя</summary>
    /// <example>обл. Московская, г. Подольск, ул. А.Невского, д. 123-4, кв 45</example>
    [Required]
    [MaxLength(100)]
    [RegularExpression(@"[a-zA-Zа-яА-Я\,0-9\-\s\.]*$")]
    public string AddressReceiver { get; set; } = string.Empty;

    /// <summary>Вес посылки (кг)</summary>
    /// <example>2.5</example>
    [Required]
    [Range(0.001, 1000)]
    public double Weight { get; set; }
}

public class ReadOrderDto
{
    /// <summary>Идентификатор заказа</summary>
    /// <example>xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx</example>
    public Guid Id { get; set; }

    /// <summary>Город отправителя</summary>
    /// <example>Москва</example>
    [Required]
    [MaxLength(20)]
    [RegularExpression(@"^[a-zA-Zа-яА-Я]*[\s\-]?[a-zA-Zа-яА-Я]*[\.\s]?$")]
    public string CitySender { get; set; } = string.Empty;

    /// <summary>Адресс отправителя</summary>
    /// <example>Москва, ул. Пушкина , д.1, кв 23</example>
    [Required]
    [MaxLength(100)]
    [RegularExpression(@"[a-zA-Zа-яА-Я\,0-9\-\s\.]*$")]
    public string AddressSender { get; set; } = string.Empty;

    /// <summary>Город получателя</summary>
    /// <example>Подольск</example>
    [Required]
    [MaxLength(20)]
    [RegularExpression(@"^[a-zA-Zа-яА-Я]*[\s\-]?[a-zA-Zа-яА-Я]*[\.\s]?$")]
    public string CityReceiver { get; set; } = string.Empty;

    /// <summary>Адресс получателя</summary>
    /// <example>обл. Московская, г. Подольск, ул. А.Невского, д. 123-4, кв 45</example>
    [Required]
    [MaxLength(100)]
    [RegularExpression(@"[a-zA-Zа-яА-Я\,0-9\-\s\.]*$")]
    public string AddressReceiver { get; set; } = string.Empty;

    /// <summary>Вес посылки (кг)</summary>
    /// <example>2.5</example>
    [Required]
    [Range(0.001, 1000)]
    public double Weight { get; set; }

    /// <summary>Дата получения заказа</summary>
    /// <example>2022.04.13:18:31:3123+3:00</example>
    public DateTime DateReceiving { get; set; }
}