using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Hive.Application.DTOs;

public class MidiaDTO
{
    public int Id { get; set; }

    [DisplayName("Duration")]
    public string Duration { get;  set; }

    [DisplayName("Resolution")]
    public string Resolution { get;  set; }

    [DisplayName("Proporção")]
    public string AspectRatio { get;  set; }

    [DisplayName("Formato")]
    public string Format { get;  set; }

    [DisplayName("Tamanho")]
    public string size { get;  set; }

    [DisplayName("Url")]
    [Required(ErrorMessage = "The Url is Required")]
    public string Url { get;  set; }
}
