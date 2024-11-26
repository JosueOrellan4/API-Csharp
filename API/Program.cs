using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
//antes de crair a API, instalar no pacote de Nuget: Newtonsoft.Json
//HttpClient : Usado para fazer requisições HTTP, como GET, POST, PUT, DELETE.

// GetAsync: Método assíncrono usado para fazer requisições GET.

//ReadAsStringAsync: Lê a resposta da API como uma string.

// JsonConvert.DeserializeObject: Usado para converter o Json da resposta em um Objeto C#

//Quando você marca um método como async, o compilador permite o uso  de await dentro dele, 
//que é a palavra chave que indica onde o codigo deve esperar por uma operação assincrona.

//quando usa o VOID: ele não retorna nenhum valor, ele apenas executa a ação de imprimir
//os dados, sempre depende de algum recurso para exibir algo. EX: Console.Write





namespace API
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //criação da instância do HTTPCLIENT
            HttpClient client = new HttpClient();

            //defini a URL do API
            string apiUrl = "https://fakestoreapi.com/products";

            try
            {
                // Enviar uma requisição GET para a API
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                //Verificar se a requisição foi bem-sucedida
                if (response.IsSuccessStatusCode)
                {
                    //Ler o conteúdo da resposta como uma string 
                    string jsonResult = await response.Content.ReadAsStringAsync();

                    //Converter p JSON para um objeto C#
                    var jsonObject = JsonConvert.DeserializeObject<dynamic>(jsonResult);
                    Console.WriteLine("Resposta da API: ");

                    //Exibir o resultado 
                    //Console.WriteLine(jsonResult);

                    foreach (var produto in jsonObject)
                    {
                        Console.WriteLine($"Nome: {produto.title}" + $"\nCategoria: {produto.category}\n");
                    }

                }
                else
                {
                    Console.WriteLine($"Erro na requisição: {response.StatusCode}");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                // Fechar o HttpClient
                client.Dispose();
            }
        }
    }
}
