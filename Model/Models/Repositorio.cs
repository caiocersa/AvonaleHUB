using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class Repositorio
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Full_name { get; set; }
        public string description { get; set; }
        public string language { get; set; }
        public DateTime updated_at { get; set; }
        public Usuario owner { get; set; }
        public string contributors_url { get; set; }
        public List<Usuario> contributors { get; set; }

        public Repositorio() { }

        public Repositorio(int Id,string name,string language)
        {
            this.Id = Id;
            this.Name = name;
            this.language = language;
        }
        
    }
}
