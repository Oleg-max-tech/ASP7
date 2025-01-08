namespace WebApplication1.Services
{
    public class IClientService
    {
        IEnumerable<Client> GetClients(int skip, int take);
        Client GetClientById(int id);
        Client AddClient(Client client);
        Client UpdateClient(Client client);
        void DeleteClient(int id)
    }
}
