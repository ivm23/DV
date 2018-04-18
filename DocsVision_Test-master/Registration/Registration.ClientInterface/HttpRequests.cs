using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Registration.Model;
using System.Text;

namespace Registration.ClientInterface
{
    internal class HttpRequests : IHttpRequests
    {
        private readonly HttpClient _client;
        public HttpRequests(string connectionString)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(connectionString);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Letter CreateLetter(Letter letter, string connectionString)
        {
            using (var response = _client.PostAsJsonAsync($"{connectionString}/letter", letter).Result)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return response.Content.ReadAsAsync<Letter>().Result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public IEnumerable<LetterView> GetLettersView(Guid idWorker, string connectionString)
        {
            return _client.GetAsync($"{connectionString}/letters/worker/{idWorker}").Result.Content.ReadAsAsync<IEnumerable<LetterView>>().Result;
        }

        public bool LetterIsRead(Guid letterId, Guid workerId, int isRead, string connectionString)
        {
            using (var response = _client.PutAsJsonAsync($"{connectionString}/letter/{letterId}/{workerId}", isRead).Result)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return response.Content.ReadAsAsync<bool>().Result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public void DeleteLetterFromDatabase(Guid letterId, string connectionString)
        {
            _client.DeleteAsync($"{connectionString}/letter/" + Convert.ToString(letterId));
        }
        public void DeleteLetter(Guid idLetter, Guid idWorker, Guid idFolder, string connectionString)
        {
            _client.DeleteAsync($"{connectionString}/letter/{Convert.ToString(idLetter)}/worker/{Convert.ToString(idWorker)}/folder/{Convert.ToString(idFolder)}");
        }
        public Worker CreateWorker(Worker worker, string connectionString)
        {
            using (var response = _client.PostAsJsonAsync($"{connectionString}/worker", worker).Result)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return response.Content.ReadAsAsync<Worker>().Result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }

            }
        }
        public Worker AuthorizationWorker(string login, string password, string connectionString)
        {
            return (_client.GetAsync($"{connectionString}/worker/{login}/{password}").Result.Content.ReadAsAsync<Worker>().Result);
        }

        public Worker GetWorker(string login, string connectionString)
        {
            using (var response = _client.GetAsync($"{connectionString}/worker/{login}").Result)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return response.Content.ReadAsAsync<Worker>().Result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public string GetWorkerName(Guid workerId, string connectionString)
        {
            return _client.GetAsync($"{connectionString}/worker/{workerId}/name").Result.Content.ReadAsAsync<string>().Result;
        }

        public IEnumerable<Worker> GetAllWorkers(string connectionString)
        {
            return _client.GetAsync($"{connectionString}/workers").Result.Content.ReadAsAsync<IEnumerable<Worker>>().Result;
        }

        public IEnumerable<string> GetDatabasesNames()
        {
            return _client.GetAsync("database").Result.Content.ReadAsAsync<IEnumerable<string>>().Result;
        }

        public IEnumerable<Folder> GetAllWorkerFolders(Guid workerId, string connectionString)
        {
            return _client.GetAsync($"{connectionString}/folders/{workerId}").Result.Content.ReadAsAsync<IEnumerable<Folder>>().Result;
        }

        public IEnumerable<LetterView> GetWorkerLettersInFolder(Guid folderId, Guid workerId, string connectionString)
        {
            return _client.GetAsync($"{connectionString}/letters/folder/{folderId}/{workerId}").Result.Content.ReadAsAsync<IEnumerable<LetterView>>().Result;
        }

        public Folder GetFolder(Guid folderId, string connectionString)
        {
            return _client.GetAsync($"{connectionString}/folder/{folderId}").Result.Content.ReadAsAsync<Folder>().Result;
        }
        public Folder CreateFolder(Folder folder, string connectionString)
        {
            using (var response = _client.PostAsJsonAsync($"{connectionString}/folder", folder).Result)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return response.Content.ReadAsAsync<Folder>().Result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }

            }
        }

        public Folder UpdateFolder(Folder folder, string connectionString)
        {
            using (var response = _client.PutAsJsonAsync($"{connectionString}/folder/update", folder).Result)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return response.Content.ReadAsAsync<Folder>().Result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public void DeleteFolder(Guid folderId, string connectionString)
        {
            StringBuilder request = new StringBuilder($"{connectionString}/folder/");
            request.Append(folderId);
            _client.DeleteAsync(request.ToString());
        }

        public int GetCountLettersInFolder(Guid folderId, string connectionString)
        {
            return _client.GetAsync($"{connectionString}/count/letters/{folderId}").Result.Content.ReadAsAsync<int>().Result;
        }

        public IEnumerable<FolderType> GetAllFolderTypes(string connectionString)
        {
            return _client.GetAsync($"{connectionString}/folder/types").Result.Content.ReadAsAsync<IEnumerable<FolderType>>().Result;
        }

        public FolderType GetFolderType(int folderTypeId, string connectionString)
        {
            return _client.GetAsync($"{connectionString}/folder/{folderTypeId}/type").Result.Content.ReadAsAsync<FolderType>().Result;
        }

        public IEnumerable<LetterType> GetAllLetterTypes(string connectionString)
        {
            return _client.GetAsync($"{connectionString}/letter/types").Result.Content.ReadAsAsync<IEnumerable<LetterType>>().Result;
        }

        public LetterType GetLetterType(int letterTypeId, string connectionString)
        {
            return _client.GetAsync($"{connectionString}/letter/{letterTypeId}/type").Result.Content.ReadAsAsync<LetterType>().Result;
        }
    }
}
