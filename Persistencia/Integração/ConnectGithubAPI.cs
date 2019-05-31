using Model.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Integração
{
    public class ConnectGithubAPI
    {

        public List<Repositorio> GetMeusRepositorios()
        {
            HttpClient ConexaoGitHub = new HttpClient();
            ConexaoGitHub.BaseAddress = new Uri("https://api.github.com/");
            ConexaoGitHub.DefaultRequestHeaders.Add("User-Agent", "request");

            List<Repositorio> Repositorios = new List<Repositorio>();
            var Pacote = ConexaoGitHub.GetAsync("users/caiocersa/repos");
            Pacote.Wait();

            if (Pacote.Result.IsSuccessStatusCode)
            {
                var resultado = JsonParaRepositorios(Pacote.Result);
                resultado.Wait();

                Repositorios = resultado.Result;

                foreach(Repositorio repositorio in Repositorios)
                {
                    HttpClient ConexaoContribuidores = new HttpClient();                 
                    ConexaoContribuidores.BaseAddress = new Uri(repositorio.contributors_url);
                    ConexaoContribuidores.DefaultRequestHeaders.Add("User-Agent", "request");

                    var responseTask = ConexaoContribuidores.GetAsync("");
                    responseTask.Wait();

                    if (responseTask.Result.IsSuccessStatusCode)
                    {
                        var Contribuidores = JsonParaContribuidores(responseTask.Result);
                        Contribuidores.Wait();

                        repositorio.contributors = Contribuidores.Result;
                    }

                }
            }
            return Repositorios;
        }

        public List<Repositorio> GetPesquisarRepositorios(string chave)
        {
            HttpClient ConexaoGitHub = new HttpClient();
            ConexaoGitHub.BaseAddress = new Uri("https://api.github.com/");
            ConexaoGitHub.DefaultRequestHeaders.Add("User-Agent", "request");

            List<Repositorio> Repositorios = new List<Repositorio>();
            var Pacote = ConexaoGitHub.GetAsync("search/repositories?q="+chave);
            Pacote.Wait();

            if (Pacote.Result.IsSuccessStatusCode)
            {
                var resultado = JsonPesquisarParaRepositorios(Pacote.Result);

                resultado.Wait();

                Repositorios = resultado.Result;
            }
            return Repositorios;
        }

        public Repositorio GetRepositorio(int id)
        {
            HttpClient ConexaoGitHub = new HttpClient();
            ConexaoGitHub.BaseAddress = new Uri("https://api.github.com/");
            ConexaoGitHub.DefaultRequestHeaders.Add("User-Agent", "request");

            Repositorio repositorio = new Repositorio();
            var Pacote = ConexaoGitHub.GetAsync("repositories/"+id);
            Pacote.Wait();

            if (Pacote.Result.IsSuccessStatusCode)
            {
                var resultado = JsonParaRepositorio(Pacote.Result);
                resultado.Wait();

                repositorio = resultado.Result;
                HttpClient ConexaoContribuidores = new HttpClient();
                ConexaoContribuidores.BaseAddress = new Uri(repositorio.contributors_url);
                ConexaoContribuidores.DefaultRequestHeaders.Add("User-Agent", "request");

                var responseTask = ConexaoContribuidores.GetAsync("");
                responseTask.Wait();
                if (responseTask.Result.IsSuccessStatusCode)
                {
                    var Contribuidores = JsonParaContribuidores(responseTask.Result);
                    Contribuidores.Wait();

                        repositorio.contributors = Contribuidores.Result;
                }

            }
        
            return repositorio;
        }

        static async Task<List<Repositorio>> JsonParaRepositorios(HttpResponseMessage pacote)
        {
            string resultado = await pacote.Content.ReadAsStringAsync();
            List<Repositorio> objeto = JsonConvert.DeserializeObject<List<Repositorio>>(resultado, new KeysJsonConverter(typeof(Repositorio)));
            return objeto;
        }

        static async Task<List<Repositorio>> JsonPesquisarParaRepositorios(HttpResponseMessage pacote)
        {
            string resultado = await pacote.Content.ReadAsStringAsync();

            //Transforma Json em Objeto
            Object ob = JsonConvert.DeserializeObject(resultado);

            //Separa as 3º propriedades do objeto em Array
            string[] arr = ((IEnumerable)ob).Cast<object>()
                                 .Select(x => x.ToString())
                                 .ToArray();
            //arr[2] = Remover o encapsulamento dos repositorios de Item
            string json = arr[2].Remove(0,9);

            List<Repositorio> objeto = JsonConvert.DeserializeObject<List<Repositorio>>(json, new KeysJsonConverter(typeof(Repositorio)));
            return objeto;
        }

        static async Task<List<Usuario>> JsonParaContribuidores(HttpResponseMessage pacote)
        {
            string resultado = await pacote.Content.ReadAsStringAsync();
            List<Usuario> objeto = JsonConvert.DeserializeObject<List<Usuario>>(resultado, new KeysJsonConverter(typeof(Usuario)));
            return objeto;
        }

        static async Task<Repositorio> JsonParaRepositorio(HttpResponseMessage pacote)
        {
            string resultado = await pacote.Content.ReadAsStringAsync();
            Repositorio objeto = JsonConvert.DeserializeObject<Repositorio>(resultado, new KeysJsonConverter(typeof(Repositorio)));
            return objeto;
        }


    }
}
