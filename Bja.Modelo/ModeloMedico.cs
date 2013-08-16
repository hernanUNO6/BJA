using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bja.AccesoDatos;
using Bja.Entidades;

namespace Bja.Modelo
{
    public class ModeloMedico
    {
        BjaContext db = new BjaContext();
        const int TAMANIO_PAGINA = 3;

        public List<Medico> Listar()
        {
            return db.Medicos.ToList();
            //return db.Medicos.OrderBy(p => p.Id).Skip(2).Take(5).ToList();
        }

        public List<Medico> Listar(string criterio)
        {
            //return db.Medicos.Where(p => criterio == null || p.Nombres.StartsWith(criterio)).ToList();

            return (from m in db.Medicos
                    where m.Nombres.Contains(criterio) ||
                    m.PrimerApellido.Contains(criterio) ||
                    m.SegundoApellido.Contains(criterio)
                    select m).ToList();
        }

        public List<Medico> Listar(string criterio, int pagina)
        {
            if (criterio != null)
            {
                return (from m in db.Medicos
                        where m.Nombres.Contains(criterio) ||
                        m.PrimerApellido.Contains(criterio) ||
                        m.SegundoApellido.Contains(criterio)
                        select m).OrderBy(p=>p.Nombres).Skip((pagina - 1) * TAMANIO_PAGINA).Take(TAMANIO_PAGINA).ToList();
            }
            else
            {
                return db.Medicos.OrderBy(p => p.Id).Skip((pagina - 1) * TAMANIO_PAGINA).Take(TAMANIO_PAGINA).ToList();
            }
        }

        public int TotalRegistros(string criterio)
        {
            if (criterio != null)
            {
                return (from m in db.Medicos
                        where m.Nombres.Contains(criterio) ||
                        m.PrimerApellido.Contains(criterio) ||
                        m.SegundoApellido.Contains(criterio)
                        select m).Count();
            }
            else
            {
                return db.Medicos.Count();
            }
        }

        public int TamanioPagina()
        {
            return TAMANIO_PAGINA;
        }

        public void Crear(Medico medico)
        {
            db.Medicos.Add(medico);
            db.SaveChanges();
        }

        public Medico Buscar(long id = 0)
        {
            Medico medico = db.Medicos.Find(id);
            if (medico == null)
            {
                return null;
            }
            return medico;
        }

        public void Editar(Medico medico)
        {
            db.Entry(medico).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Eliminar(long id)
        {
            Medico medico = this.Buscar(id);
            db.Medicos.Remove(medico);
            db.SaveChanges();
        }
    }
}
