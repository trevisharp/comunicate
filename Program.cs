using System.Net.Http;
using System.Threading.Tasks;

using static System.Console;

var ip = await getPublicIP();
WriteLine(ip);

async Task<string> getPublicIP()
{
    HttpClient http = new HttpClient();
    var response = await http.GetAsync("https://api.ipify.org");
    return await response.Content.ReadAsStringAsync();
}