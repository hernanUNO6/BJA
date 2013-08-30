using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Bja.Modelo
{
    public class Serializer
    {
        public static void serializar(object objeto, string archivo)
        {
            //ShoppingCart shoppingCart = FetchShoppingCartFromSomewhere();
            var serializer = new XmlSerializer(objeto.GetType());
            using (var writer = XmlWriter.Create(archivo))
            {
                serializer.Serialize(writer, objeto);
            }

        }
        public static void deserializar(string archivo, object objeto){
            var serializer = new XmlSerializer(objeto.GetType());
            using (var reader = XmlReader.Create(archivo))
            {
                objeto = serializer.Deserialize(reader);
            }
        }
    }
}
