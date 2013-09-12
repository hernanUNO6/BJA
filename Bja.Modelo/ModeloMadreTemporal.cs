using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bja.AccesoDatos;
using Bja.Entidades;

namespace Bja.Modelo
{
    public class ModeloMadreTemporal
    {
        BjaContext db = new BjaContext();

        public List<MadreTemporal> Listar()
        {
            return db.MadresTemporal.ToList();
        }

        public void Crear(MadreTemporal madreTemp)
        {
            db.MadresTemporal.Add(madreTemp);
            db.SaveChanges();
        }

        public MadreTemporal Buscar(long id = 0)
        {
            MadreTemporal madreTemporal = (from mt in db.MadresTemporal
                                           where mt.Id == id
                                           select mt).FirstOrDefault();
            if (madreTemporal == null)
            {
                return null;
            }
            return madreTemporal;
        }

        public List<MadreTemporal> Espejo(string id)
        {
            return Listar().Where(m => m.Id == Convert.ToInt64(id)).ToList();
        }

        private string ConstruirParametroMadreTemporal(MadreTemporal madreTemp, Dictionary<string, object> propiedadesObejtoBusqueda)
        {
            string cadenaA = "";
            foreach (var item in propiedadesObejtoBusqueda)
            {
                if (item.Key == "Identificador") continue;
                if ((bool)item.Value)
                {
                    switch (item.Key)
                    {
                        case "Nombres": cadenaA += madreTemp.Nombres; break;
                        case "PrimerApellido": cadenaA += madreTemp.PrimerApellido; break;
                        case "SegundoApellido": cadenaA += madreTemp.SegundoApellido; break;
                        case "TercerApellido": cadenaA += madreTemp.TercerApellido; break;
                        case "NombreCompleto": cadenaA += madreTemp.NombreCompleto; break;
                        case "DocumentoIdentidad": cadenaA += madreTemp.DocumentoIdentidad; break;
                        case "FechaNacimiento": cadenaA += madreTemp.FechaNacimiento; break;
                        case "LocalidadNacimiento": cadenaA += madreTemp.LocalidadNacimiento; break;
                    }
                }
            }
            return cadenaA;
        }

        private string ConstruirParametroMadre(Madre madre, Dictionary<string, object> propiedadesObejtoBusqueda)
        {
            string cadenaB = "";
            foreach (var item in propiedadesObejtoBusqueda)
            {
                if (item.Key == "Identificador") continue;
                if ((bool)item.Value)
                {
                    switch (item.Key)
                    {
                        case "Nombres": cadenaB += madre.Nombres; break;
                        case "PrimerApellido": cadenaB += madre.PrimerApellido; break;
                        case "SegundoApellido": cadenaB += madre.SegundoApellido; break;
                        case "TercerApellido": cadenaB += madre.TercerApellido; break;
                        case "NombreCompleto": cadenaB += madre.NombreCompleto; break;
                        case "DocumentoIdentidad": cadenaB += madre.DocumentoIdentidad; break;
                        case "FechaNacimiento": cadenaB += madre.FechaNacimiento; break;
                        case "LocalidadNacimiento": cadenaB += madre.LocalidadNacimiento; break;
                    }
                }
            }
            return cadenaB;
        }

        public List<Madre> BuscarSemejantes(ParametrosBusqueda parBusqueda)
        {
            long identificador = Convert.ToInt64(parBusqueda.Identificador);
            MadreTemporal madreTemp = new MadreTemporal();
            madreTemp = Listar().Where(m => m.Id == identificador).FirstOrDefault();

            Dictionary<string, object> propiedadesBusqueda = ObtenerElementos(parBusqueda, MemberTypes.Property);

            List<Madre> madresCandidatas = new List<Madre>();
            Madre peorSemejanza = new Madre();            

            int distanciaCalculada;
            int peorDistancia = -1;

            //string strParametrosMadreTemporal = madreTemp.NombreCompleto;
            string cadenaMadreTemporal = ConstruirParametroMadreTemporal(madreTemp, propiedadesBusqueda);

            //foreach (Madre ma in madre.Listar())
            foreach (Madre ma in db.Madres.ToList())
            {
                distanciaCalculada = Distancia.Levenshtein(cadenaMadreTemporal, ConstruirParametroMadre(ma, propiedadesBusqueda));
                if (madresCandidatas.Count < 10)
                {
                    ma.IdMunicipio = distanciaCalculada;    //**
                    madresCandidatas.Add(ma);
                    if (distanciaCalculada > peorDistancia)
                    {
                        peorDistancia = distanciaCalculada;
                        peorSemejanza = ma;
                    }
                }
                else
                {
                    if (distanciaCalculada < peorDistancia)
                    {
                        ma.IdMunicipio = distanciaCalculada;    //**
                        madresCandidatas.Remove(peorSemejanza);
                        madresCandidatas.Add(ma);

                        // Peor distancia entre los 10
                        peorDistancia = -1;
                        foreach (var item in madresCandidatas)
                        {
                            distanciaCalculada = Distancia.Levenshtein(madreTemp.NombreCompleto, item.NombreCompleto);
                            if (distanciaCalculada > peorDistancia)
                            {
                                peorDistancia = distanciaCalculada;
                                peorSemejanza = item;
                            }
                        }
                    }
                }
            }
            return madresCandidatas.OrderBy(m => m.IdMunicipio).ToList();
        }

        /// <summary>
        /// Función encargada de devolver un tipo de elemento en concreto de un objeto en un conjunto
        /// de pares clave - valor.
        /// TipoElemento puede ser de tipo método, propiedad, etc.
        /// haciendo referencia al enumerador MemberTypes
        /// </summary>
        /// <changelog>
        /// Daniel García    15/05/2009    Creación
        /// </changelog>
        private Dictionary<string, object> ObtenerElementos(object objeto, MemberTypes TipoElemento)
        {
            
            try
            {
                // Declaramos un Diccionario que contendra el nombre de los elementos del objeto y el
                //contenido de cada elemento.
                Dictionary<string, object> Elementos = new Dictionary<string, object>();

                // Se recorren los miembros del objeto
                foreach (MemberInfo infoMiembro in objeto.GetType().GetMembers())
                {
                    // Si el tipo del objeto es del tipo que buscamos, se añade al diccionario
                    if (infoMiembro.MemberType == TipoElemento)
                    {
                        if ((PropertyInfo)infoMiembro != null)
                            Elementos.Add(((PropertyInfo)infoMiembro).Name, ((PropertyInfo)infoMiembro).GetValue(objeto, null));
                    }
                }
                return Elementos;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }



    }
}
