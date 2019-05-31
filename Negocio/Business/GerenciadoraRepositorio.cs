using Model.Models;
using Persistencia.Integração;
using Persistencia.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.Business
{
    public class GerenciadoraRepositorio
    {
        private ConnectGithubAPI persistenciaAPI = new ConnectGithubAPI();
        private RepositorioPersistencia persistenciaARQ = new RepositorioPersistencia();

        public List<Repositorio> ObterMeusRepositorios()
        {
      
            return persistenciaAPI.GetMeusRepositorios();
        }

        public List<Repositorio> PesquisarRepositorios(string chave)
        {

            return persistenciaAPI.GetPesquisarRepositorios(chave);
        }

        public Repositorio ObterRepositorio(int id)
        {
            
            return persistenciaAPI.GetRepositorio(id);
        }

        public void AdicionarFavorito(Repositorio repositorio)
        {         
            persistenciaARQ.CreateFavorito(repositorio);
        }

        public List<Repositorio> ObterFavoritos()
        {

            return persistenciaARQ.GetFavoritos();
        }
    }
}
