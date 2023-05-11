using System.Net.Http;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

using static System.Console;
using static System.Text.Encoding;

var ip = await getPublicIP();
WriteLine(ip);

var endpoint = new IPEndPoint(IPAddress.Any, 8000);
var listener = new Socket(
    AddressFamily.InterNetwork,
    SocketType.Stream,
    ProtocolType.Tcp
);
listener.Bind(endpoint);
listener.Listen();

while (true)
{
    var clientSocket = listener.Accept();

    var buffer = new byte[1024];
    var bytesReceived = clientSocket.Receive(buffer);
    var message = UTF8.GetString(buffer, 0, bytesReceived);

    WriteLine($"Mensagem recebida do cliente: {message}");

    // Envia uma resposta para o cliente
    var response = System.Text.Encoding.UTF8.GetBytes("Ola, cliente!");
    clientSocket.Send(response);

    // Fecha a conexão com o cliente
    clientSocket.Shutdown(SocketShutdown.Both);
    clientSocket.Close();

}

async Task<string> getPublicIP()
{
    HttpClient http = new HttpClient();
    var response = await http.GetAsync("https://api.ipify.org");
    return await response.Content.ReadAsStringAsync();
}