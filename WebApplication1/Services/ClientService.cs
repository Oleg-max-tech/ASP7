using System;
using System.Collections.Generic;
using System.Linq;
using OnlineShopProject.Data;
using OnlineShopProject.Models;


namespace WebApplication1.Services
{
    public class ClientService

    {
        private readonly ApplicationDbContext _context;

        public ClientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Client> GetClients(int skip, int take)
        {
            return _context.Clients
                .Where(c => c.DeletedAt == null) // Фільтруємо видалені клієнти
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public Client GetClientById(int id)
        {
            return _context.Clients.FirstOrDefault(c => c.Id == id && c.DeletedAt == null);
        }

        public Client AddClient(Client client)
        {
            client.CreatedAt = DateTime.UtcNow; // Встановлюємо CreatedAt
            _context.Clients.Add(client);
            _context.SaveChanges();
            return client;
        }

        public Client UpdateClient(Client client)
        {
            var existingClient = _context.Clients.FirstOrDefault(c => c.Id == client.Id && c.DeletedAt == null);
            if (existingClient == null) throw new Exception("Client not found");

            existingClient.Name = client.Name;
            existingClient.Email = client.Email;
            existingClient.Phone = client.Phone;
            _context.SaveChanges();

            return existingClient;
        }

        public void DeleteClient(int id)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Id == id && c.DeletedAt == null);
            if (client == null) throw new Exception("Client not found");

            client.DeletedAt = DateTime.UtcNow; // Soft-delete
            _context.SaveChanges();
        }
    }
}
