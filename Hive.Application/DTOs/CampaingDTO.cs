using Hive.Domain.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hive.Application.DTOs;

public class CampaingDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The Name is Required")]
    [MinLength(3)]
    [MaxLength(100)]
    [DisplayName("Name")]
    public string? Name { get; set; }

    [MinLength(5)]
    [MaxLength(200)]
    [DisplayName("Message")]
    public string? Message { get; set; }

    [Required(ErrorMessage = "The Budget is Required")]
    [Column(TypeName = "decimal(18,2)")]
    [DisplayFormat(DataFormatString = "{0:C2}")]
    [DataType(DataType.Currency)]
    [DisplayName("Budget")]
    public decimal Budget { get; set; }

    [Required(ErrorMessage = "The Stock is Required")]
    [DisplayName("Stock")]
    public int Stock { get; set; }

    [MaxLength(250)]
    [DisplayName("Campaing Type")]
    public string? CampaingType { get; set; }

    [DisplayName("Usuario Id")]
    public int UsuarioId { get; set; }

    [DisplayName("Initial Date")]
    public DateTime InitialDate { get; set; }

    [DisplayName("Final Date")]
    public DateTime FinalDate { get; private set; }

    [Required(ErrorMessage = "The campaing status is Required")]
    [DisplayName("Campaing Type")]
    public string CampaingStatus { get; private set; }

    [Required(ErrorMessage = "The target public is Required")]
    [DisplayName("Campaing Type")]
    public string TargetPublic { get; private set; }

}
