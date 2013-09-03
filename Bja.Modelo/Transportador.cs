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

            //buscar registros de madres nuevas, modificadas, borradas
            nuevoEnvio.Madres = (from m in context.Madres
                                     where m.EstadoSincronizacion == TipoEstadoSincronizacion.Pendiente
                                     select m).ToList();
            nuevoEnvio.Tutores = (from t in context.Tutores
                                  where t.EstadoSincronizacion == TipoEstadoSincronizacion.Pendiente
                                       select t).ToList();
            nuevoEnvio.Menores = (from t in context.Menores
                                  where t.EstadoSincronizacion == TipoEstadoSincronizacion.Pendiente
                                        select t).ToList();
            nuevoEnvio.CorresponsabilidadMadres = (from m in context.CorresponsabilidadesMadre
                                         where m.EstadoSincronizacion == TipoEstadoSincronizacion.Pendiente
                                         select m).ToList();
            nuevoEnvio.ControlMadres = (from m in context.ControlesMadre
                                         where m.EstadoSincronizacion == TipoEstadoSincronizacion.Pendiente
                                       select m).ToList();
            nuevoEnvio.CorresponsabilidadMenores = (from m in context.CorresponsabilidadesMenor
                                                    where m.EstadoSincronizacion == TipoEstadoSincronizacion.Pendiente
                                                    select m).ToList();
            nuevoEnvio.ControlMenores = (from t in context.ControlesMenor
                                         where t.EstadoSincronizacion == TipoEstadoSincronizacion.Pendiente
                                        select t).ToList();

            return nuevoEnvio;
        }

        public static void generarArchivoEnvio(String path)
        {
            BjaContext context = new BjaContext();

            Envio datosEnvio = generarEnvio();

            context.Envios.Add(datosEnvio);

            Serializer.serializar(datosEnvio, path);

            //cambiar estados de registros a generados
            datosEnvio.Madres.ForEach(delegate(Madre madre)
            {
                madre.EstadoSincronizacion = TipoEstadoSincronizacion.Generado;
            });

            datosEnvio.Tutores.ForEach(delegate(Tutor registro)
            {
                registro.EstadoSincronizacion = TipoEstadoSincronizacion.Generado;
            });

            datosEnvio.Menores.ForEach(delegate(Menor registro)
            {
                registro.EstadoSincronizacion = TipoEstadoSincronizacion.Generado;
            });
            datosEnvio.CorresponsabilidadMadres.ForEach(delegate(CorresponsabilidadMadre registro)
            {
                registro.EstadoSincronizacion = TipoEstadoSincronizacion.Generado;
            });
            datosEnvio.ControlMadres.ForEach(delegate(ControlMadre registro)
            {
                registro.EstadoSincronizacion = TipoEstadoSincronizacion.Generado;
            });
            datosEnvio.CorresponsabilidadMenores.ForEach(delegate(CorresponsabilidadMenor registro)
            {
                registro.EstadoSincronizacion = TipoEstadoSincronizacion.Generado;
            });
            datosEnvio.ControlMenores.ForEach(delegate(ControlMenor registro)
            {
                registro.EstadoSincronizacion = TipoEstadoSincronizacion.Generado;
            });

            context.SaveChanges();
        }

    }
}
