using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bja.AccesoDatos;
using Bja.Entidades;

namespace Bja.Modelo
{
    public class ModeloAsignacionMedico
    {
        private BjaContext db = new BjaContext();

        public List<AsignacionMedico> Listar()
        {
            var asignacionesmedico = db.AsignacionesMedico.Include(a => a.Medico).Include(a => a.EstablecimientoSalud);
            return asignacionesmedico.ToList();
        }

        public void Crear(AsignacionMedico asignacionMedico)
        {
            db.AsignacionesMedico.Add(asignacionMedico);
            db.SaveChanges();
        }

        public AsignacionMedico Buscar(long id = 0)
        {
            AsignacionMedico asignacionMedico = db.AsignacionesMedico.Find(id);
            if (asignacionMedico == null)
            {
                return null;
            }
            return asignacionMedico;
        }

        public void Editar(AsignacionMedico asignacionMedico)
        {
            db.Entry(asignacionMedico).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Eliminar(long id)
        {
            AsignacionMedico asignacionMedico = this.Buscar(id);
            db.AsignacionesMedico.Remove(asignacionMedico);
            db.SaveChanges();
        }

        //public List<Municipio> GetMunicipiosPorProvincias(string idProvincia)
        //{
        //    Int64 Identificador = Convert.ToInt64(idProvincia);

        //    List<Municipio> provs = (from m in db.Municipios
        //                             where m.IdProvincia == Identificador
        //                             select m).ToList();
        //    return provs;
        //}
    }
}
