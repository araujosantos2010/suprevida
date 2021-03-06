using BioStore.Shared.Helpers;
using BioStore.Site.Helper;
using FluentValidator;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BioStore.Site.Controllers
{
    public class BaseController : Controller
    {
        JsonIbSerializer json;
        public BaseController()
        {
            //User.AddIdentity(new ClaimsIdentity("Admin"));

            //var teste = User.Claims.

            json = new JsonIbSerializer();
        }
        protected T GetApiMethod<T>(string uri, object parameters)
        {

            using (var restClient = CriaRestClient())
            {
                var request = new RestRequest(uri, Method.GET);

                if (parameters != null)
                {
                    var parametros = parameters is Dictionary<string, object> ? (Dictionary<string, object>)parameters : parameters.ToDictionary();
                    if (parametros.Count > 0)
                    {
                        foreach (var (chave, valor) in parametros)
                        {
                            request.AddParameter(chave, valor);
                        }
                    }
                }
                var response = restClient.Execute(request);


                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.StatusCode == HttpStatusCode.OK ? json.Deserialize<T>(response) : json.Deserialize<T>(response);


                }
                else
                {
                    var mensagem = "Ocorreu Um erro durante a execução da Api";
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            mensagem = $"Erro ao executar Api {response.StatusCode.GetHashCode()}";
                            break;
                        case HttpStatusCode.InternalServerError:
                            mensagem = $"Erro ao executar Api{response.StatusCode.GetHashCode()}";
                            break;
                        default:
                            break;
                    }

                    TempData["Alerta"] = JsonConvert.SerializeObject(new BioResult
                    {
                        TipoMensagem = EtipoMensagemViewModel.Info,
                        Message = "Ocorreu Um erro durante a execução da Api",
                        Data = new List<Notification>
                        {
                            { new Notification("", mensagem) }
                        }
                    });

                }
                return response.StatusCode == HttpStatusCode.OK ? json.Deserialize<T>(response) : json.Deserialize<T>(response);


            }
        }

        protected T GetApiMethod<T>(string uri, Dictionary<string, object> parameters)
        {
            using (var restClient = CriaRestClient())
            {
                var request = new RestRequest(uri, Method.GET);
                if (parameters != null && parameters.Count > 0)
                {
                    foreach (var (chave, valor) in parameters)
                    {
                        request.AddParameter(chave, valor);
                    }
                }
                var response = restClient.Execute(request);
                return response.StatusCode == HttpStatusCode.OK ? json.Deserialize<T>(response) : json.Deserialize<T>(response);
            }
        }


        protected string GetApiMethod(string uri, Dictionary<string, object> parameters, out HttpStatusCode responseStatus)
        {
            using (var restClient = CriaRestClient())
            {
                var request = new RestRequest(uri, Method.GET);

                if (parameters != null && parameters.Count > 0)
                {
                    foreach (var (chave, valor) in parameters)
                    {
                        request.AddParameter(chave, valor);
                    }
                }

                var response = restClient.Execute(request);

                responseStatus = response.StatusCode;

                return response.StatusCode == HttpStatusCode.OK ? JsonConvert.DeserializeObject<string>(response.Content) : "";
            }
        }

        protected T PostApiMethod<T>(string uri, object model, out HttpStatusCode responseStatus)
        {
            using (
                var restClient = CriaRestClient())
            {
                var request = new RestRequest(uri, Method.POST) { RequestFormat = DataFormat.Json };
                FormatRequestBody(model, request);

                var response = restClient.Execute(request);

                responseStatus = response.StatusCode;

                return response.StatusCode == HttpStatusCode.OK ? json.Deserialize<T>(response) : (T)Activator.CreateInstance(typeof(T));
            }
        }

        protected BioResult PostApiMethodRsult(string uri, object model)
        {
            using (var restClient = CriaRestClient())
            {
                var request = new RestRequest(uri, Method.POST) { RequestFormat = DataFormat.Json };
                FormatRequestBody(model, request);

                var response = restClient.Execute(request);
                var result = response.StatusCode == HttpStatusCode.OK ? json.Deserialize<BioResult>(response) : (BioResult)Activator.CreateInstance(typeof(BioResult));

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    TempData["Alerta"] = JsonConvert.SerializeObject(result);
                }
                else
                {
                    var mensagem = "Ocorreu Um erro durante a execução da Api";
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            mensagem = $"Erro ao executar Api {response.StatusCode.GetHashCode()}";
                            break;
                        case HttpStatusCode.InternalServerError:
                            mensagem = $"Erro ao executar Api{response.StatusCode.GetHashCode()}";
                            break;
                        default:
                            break;
                    }

                    TempData["Alerta"] = JsonConvert.SerializeObject(new BioResult
                    {
                        TipoMensagem = EtipoMensagemViewModel.Info,
                        Message = "Ocorreu Um erro durante a execução da Api",
                        Data = new List<Notification>
                        {
                            { new Notification("", mensagem) }
                        }
                    });

                }
                return result;
            }
        }

        protected async Task<RestSharp.IRestResponse> PostApiMethodAsync(string uri, object model)
        {
            using (var restClient = CriaRestClient())
            {
                var request = new RestRequest(uri, Method.POST) { RequestFormat = DataFormat.Json };
                FormatRequestBody(model, request);

                var response = await restClient.ExecuteAsync(request);

                return response;
            }
        }

        private void FormatRequestBody(object model, RestRequest request)
        {
            if (model is Dictionary<string, object>)
            {
                foreach (var (chave, valor) in (Dictionary<string, object>)model)
                {
                    request.AddParameter(chave, valor);
                }
            }
            else
            {
                request.JsonSerializer = new JsonIbSerializer();
                request.AddJsonBody(model);
            }
        }

        protected void PostApiMethod(string uri, object model, out HttpStatusCode responseStatus)
        {
            using (var restClient = CriaRestClient())
            {
                var request = new RestRequest(uri, Method.POST) { RequestFormat = DataFormat.Json };

                request.AddJsonBody(model);

                var response = restClient.Execute(request);

                responseStatus = response.StatusCode;
            }
        }

        protected T PutApiMethod<T>(string uri, object model, out HttpStatusCode responseStatus)
        {
            using (var restClient = CriaRestClient())
            {
                var request = new RestRequest(uri, Method.PUT) { RequestFormat = DataFormat.Json };

                request.AddJsonBody(model);

                var response = restClient.Execute(request);

                responseStatus = response.StatusCode;

                return response.StatusCode == HttpStatusCode.OK ? json.Deserialize<T>(response) : (T)Activator.CreateInstance(typeof(T));
            }
        }

        protected void PutApiMethod(string uri, object model, out HttpStatusCode responseStatus)
        {
            using (var restClient = CriaRestClient())
            {
                var request = new RestRequest(uri, Method.PUT) { RequestFormat = DataFormat.Json };

                request.AddJsonBody(model);

                var response = restClient.Execute(request);

                responseStatus = response.StatusCode;
            }
        }

        protected BioResult PutApiMethodResult(string uri, object model)
        {
            using (var restClient = CriaRestClient())
            {
                var request = new RestRequest(uri, Method.PUT) { RequestFormat = DataFormat.Json };
                FormatRequestBody(model, request);

                var response = restClient.Execute(request);
                var result = response.StatusCode == HttpStatusCode.OK ? json.Deserialize<BioResult>(response) : (BioResult)Activator.CreateInstance(typeof(BioResult));

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    TempData["Alerta"] = JsonConvert.SerializeObject(result);
                }
                else
                {
                    var mensagem = "Ocorreu Um erro durante a execução da Api";
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            mensagem = $"Erro ao executar Api {response.StatusCode.GetHashCode()}";
                            break;
                        case HttpStatusCode.InternalServerError:
                            mensagem = $"Erro ao executar Api{response.StatusCode.GetHashCode()}";
                            break;
                        default:
                            break;
                    }

                    TempData["Alerta"] = JsonConvert.SerializeObject(new BioResult
                    {
                        TipoMensagem = EtipoMensagemViewModel.Info,
                        Message = "Ocorreu Um erro durante a execução da Api",
                        Data = new List<Notification>
                        {
                            { new Notification("", mensagem) }
                        }
                    });

                }
                return result;
            }
        }

        protected BioResult DeleteApiMethod(string uri, object model)
        {
            using (var restClient = CriaRestClient())
            {
                var request = new RestRequest(uri, Method.DELETE) { RequestFormat = DataFormat.Json };
                FormatRequestBody(model, request);

                var response = restClient.Execute(request);
                var result = response.StatusCode == HttpStatusCode.OK ? json.Deserialize<BioResult>(response) : (BioResult)Activator.CreateInstance(typeof(BioResult));

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    TempData["Alerta"] = JsonConvert.SerializeObject(result);
                }
                else
                {
                    var mensagem = "Ocorreu Um erro durante a execução da Api";
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            mensagem = $"Erro ao executar Api {response.StatusCode.GetHashCode()}";
                            break;
                        case HttpStatusCode.InternalServerError:
                            mensagem = $"Erro ao executar Api{response.StatusCode.GetHashCode()}";
                            break;
                        default:
                            break;
                    }

                    TempData["Alerta"] = JsonConvert.SerializeObject(new BioResult
                    {
                        TipoMensagem = EtipoMensagemViewModel.Info,
                        Message = "Ocorreu Um erro durante a execução da Api",
                        Data = new List<Notification>
                        {
                            { new Notification("", mensagem) }
                        }
                    });

                }
                return result;
            }
        }

        protected T DeleteApiMethod<T>(string uri, object model, out HttpStatusCode responseStatus)
        {
            using (var restClient = CriaRestClient())
            {
                var request = new RestRequest(uri, Method.DELETE) { RequestFormat = DataFormat.Json };

                request.AddJsonBody(model);

                var response = restClient.Execute(request);

                responseStatus = response.StatusCode;

                return response.StatusCode == HttpStatusCode.OK ? json.Deserialize<T>(response) : (T)Activator.CreateInstance(typeof(T));
            }
        }

        public RestClientDispose CriaRestClient()
        {
            var client = new RestClientDispose("http://localhost:6252/");
            //var accessToken = HttpContext.GetTokenAsync("access_token").Result;
            // client.AddDefaultHeader("Bearer", accessToken);

            //var acesso = Acesso;

            //if (acesso != null)
            //    client.AddDefaultHeader("Authorization", "Bearer " + acesso.access_token);
            return client;
        }

        public class RestClientDispose : RestClient, IDisposable
        {
            public RestClientDispose(string enderecoBaseApi)
                : base(enderecoBaseApi)
            {

            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                {

                }
                // free native resources if there are any.

            }
        }
    }
}