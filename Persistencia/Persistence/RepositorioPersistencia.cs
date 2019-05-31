using Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Persistencia.Persistence
{
    public class RepositorioPersistencia
    {
        public void CreateFavorito(Repositorio repositorio)
        {
            if (File.Exists("Repositorios.xml"))
            {
                var xml = XDocument.Load("Repositorios.xml");

                XElement root = new XElement("Repositorio",
                                         new XAttribute("Id", repositorio.Id),
                                         new XAttribute("name", repositorio.Name),
                                         new XAttribute("language", repositorio.language));

                xml.Element("Repositorios").Add(root);
                xml.Save("Repositorios.xml");
            }
            else
            {
                var xml = new XDocument(new XElement("Repositorios",
                                        new XElement("Repositorio",
                                         new XAttribute("Id", repositorio.Id),
                                         new XAttribute("name", repositorio.Name),
                                         new XAttribute("language", repositorio.language))));
                xml.Save("Repositorios.xml");
            }
          
        }

        public List<Repositorio> GetFavoritos()
        {
            List<Repositorio> Lista = new List<Repositorio>();
            if (File.Exists("Repositorios.xml"))
            {
                var xml = XDocument.Load("Repositorios.xml");

                var re = xml.Element("Repositorios").Elements("Repositorio").Select(x => new Repositorio(int.Parse(x.Attribute("Id").Value), 
                                                                                                                   x.Attribute("name").Value, 
                                                                                                                   x.Attribute("language").Value)).ToArray();
                foreach(Repositorio item in re)
                {
                    Lista.Add(item);
                }
            }

            return Lista;
        }
    }
}
