using Clases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Wappo.Services
{
    public class RepositoriosServicios
    {
        public async Task<Repo[]> ObtenerRepoByUser(string user)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = "https://api.github.com/users/" + user + "/repos";
                var response = await client.GetStringAsync(url);

                return JsonConvert.DeserializeObject<Repo[]>(response);
            }
        }
    }
}
