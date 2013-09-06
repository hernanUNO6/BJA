using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    public class MedicoMetaData
    {
        [Display(Name = "Fecha de Registro")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaRegistro { get; set; }

        [Required]
        [StringLength(50)]
        public String Nombres { get; set; }

        [Display(Name = "Primer Apellido")]
        [StringLength(50)]
        public String PrimerApellido { get; set; }

        [Display(Name = "Segundo Apellido")]
        [StringLength(50)]
        public String SegundoApellido { get; set; }

        [Display(Name = "Tercer Apellido")]
        [StringLength(50)]
        public String TercerApellido { get; set; }

        [Display(Name = "Documento de Identidad")]
        [Required]
        [StringLength(15)]
        public String DocumentoIdentidad { get; set; }

        [Display(Name = "Tipo de Documento de Identidad")]
        [Required]
        public TipoDocumentoIdentidad TipoDocumentoIdentidad { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.DateTime, ErrorMessage="El campo debe ser del tipo fecha")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaNacimiento { get; set; }

        [Display(Name = "Localidad de Nacimiento")]
        [Required]
        public String LocalidadNacimiento { get; set; }

        [Display(Name = "Matricula")]
        [Required]
        public String MatriculaColegioMedico { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public String CorreoElectronico { get; set; }


        // [RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed")]
        // [Range(0.01, 100.00, ErrorMessage = "Price must be between 0.01 and 100.00")]
        // [Required(ErrorMessage = "An Album Title is required")]
        // [Required, StringLength(50)]
        // [ScaffoldColumn(false)]
        // [StringLength(50,MinimumLength=3,ErrorMessage="First Name must be between 3 and 50 characters!")]
        // [EmailAddress(ErrorMessage="Invalid Email")]
        // [Url(ErrorMessage = "Invalid URL!")]
        
        // Required – Indicates that the property is a required field
        // DisplayName – Defines the text we want used on form fields and validation messages
        // StringLength – Defines a maximum length for a string field
        // Range – Gives a maximum and minimum value for a numeric field
        // Bind – Lists fields to exclude or include when binding parameter or form values to model properties
        // ScaffoldColumn – Allows hiding fields from editor forms
    }

    public class ReclamoMetaData
    {
        [Display(Name = "Fecha")]
        [DataType(DataType.DateTime)]
        public DateTime FechaReclamo { get; set; }

        [Display(Name = "Tipo de reclamo")]
        [Required]
        public long IdTipoReclamo { get; set; }

        [Display(Name = "Nombre")]
        [StringLength(50)]
        [Required]
        public String NombreBeneficiario { get; set; }

        [Display(Name = "Detalle del reclamo")]
        [StringLength(250)]
        [Required]
        public String DetalleReclamo { get; set; }
    }

    public class EncargadoMetaData
    {
        [Display(Name = "Tipo Encargado")]
        [Required]
        public long IdTipoEncargado { get; set; }

        [Required]
        public String Nombres { get; set; }

        [Display(Name = "Primer Apellido")]
        public String PrimerApellido { get; set; }

        [Display(Name = "Segundo Apellido")]
        public String SegundoApellido { get; set; }

        [Display(Name = "Tercer Apellido")]
        public String TercerApellido { get; set; }

        [Display(Name = "Documento de Identidad")]
        [Required]
        public String DocumentoIdentidad { get; set; }

        [Display(Name = "Tipo Documento de Identidad")]
        [Required]
        public long TipoDocumentoIdentidad { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaNacimiento { get; set; }

        [StringLength(1)]
        [Required]
        public String Sexo { get; set; } //char(1)

        //ojo
        /*
        [Display(Name = "Estado Registro")]
        [Required]
        public long TipoEstadoRegistro { get; set; }*/
    }

    public class DepartamentoMetaData
    {
        [Required]
        [StringLength(4)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(30)]
        public string Descripcion { get; set; }
    }

    public class ProvinciaMetaData
    {
        [Required]
        [StringLength(4)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        [Display(Name = "Departamento")]
        [Required]
        public long IdDepartamento { get; set; }
    }

    public class MunicipioMetaData
    {
        [Required]
        [StringLength(4)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        [Display(Name = "Provincia")]
        [Required]
        public long IdProvincia { get; set; }
    }

    public class EstablecimientoSaludMetaData
    {
        [Required]
        [StringLength(4)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        public string Direccion { get; set; }

        //[Display(Name = "Municipio")]
        //[Required]
        //public long IdMunicipio { get; set; }
    }



    public class AsignacionMedicoMetaData
    {
        [Display(Name = "Fecha de Inicio")]
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Fecha de culminaciòn")]
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FechaFin { get; set; }

        [Display(Name = "Médico")]
        [Required]
        public long IdMedico { get; set; }

        [Display(Name = "Establecimiento de Salud")]
        [Required]
        public long IdEstablecimientoSalud { get; set; }
    }

    
}
