using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRecursivo
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<int> lista = new List<int>();
            lista.Add(1);
            lista.Add(2);
            lista.Add(3);
            lista.Add(4);
            lista.Add(5);
            lista.Add(6);
            Program a = new Program();

            a.MediaRecursiva(lista);
            a.InverterLista(lista);

            string resultado = "";
            foreach(int item in lista)
            {
                resultado += item+",";
            }
            Console.WriteLine("Lista invertida:" + resultado);
            Console.ReadKey();
        }


        public void MediaRecursiva(List<int> lista,int pos = 0,double soma = 0)
        {
            if(pos < lista.Count)
            {
                soma = soma + lista[pos];
                MediaRecursiva(lista, pos + 1,soma);
            }
            else
            {
                double media = soma / lista.Count;
                int cont = 0;
                for(int x = 0; x < lista.Count; x++)
                {
                    if(lista[x] < media)
                    {
                        cont++;
                    }
                }
                Console.WriteLine("Media:" + media);
                Console.WriteLine("Número de elementos maiores que a média:" + cont);

                
            }
                        
        }

        public void InverterLista(List<int> lista,int pos = 0)
        {
            if (pos < lista.Count / 2)
            {
                int tmp = lista[pos];
                int nova_posicao = lista.Count - pos - 1;
                lista[pos] = lista[nova_posicao];
                lista[nova_posicao] = tmp;
                InverterLista(lista, pos + 1);
            }
        }

    }
}
