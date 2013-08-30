using Bja.AccesoDatos;
using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Modelo
{
    public class Transportador
    {
        public static Envio generarEnvio()
        {
            BjaContext context = new BjaContext();

            var nuevoEnvio = new Envio();

            nuevoEnvio.Id = IdentifierGenerator.NewId();
            nuevoEnvio.IdSesion = SessionManager.getSessionIdentifier();
            nuevoEnvio.FechaUltimaTransaccion = DateTime.Now;
            nuevoEnvio.FechaRegistro = DateTime.Now;
            nuevoEnvio.EstadoRegistro = TipoEstadoRegistro.VigenteNuevoRegistro;

            nuevoEnvio.IdEstablecimientoSalud = AdministradorConfiguracion.LeerConfiguracionEnteroLargo("general.idEstablecimientoSalud");
            nuevoEnvio.IdMedico = AdministradorConfiguracion.LeerConfiguracionEnteroLargo("general.idMedicoAsignado");
            nuevoEnvio.FechaEnvio = DateTime.Now;
            nuevoEnvio.CodigoVerificacion = "";

            //buscar registros de madres nuevas
            nuevoEnvio.NuevasMadres = (from m in context.Madres
                                     where m.EstadoRegistro == TipoEstadoRegistro.VigenteNuevoRegistro
                                     select m).ToList();
            nuevoEnvio.NuevosTutores = (from t in context.Tutores
                                       where t.EstadoRegistro == TipoEstadoRegistro.VigenteNuevoRegistro
                                       select t).ToList();


            return nuevoEnvio;
        }
    }
}
