using MyRestFullApp.Core.Interfaces;
using MyRestFullApp.Core.Interfaces.Repositories;
using MyRestFullApp.Infrastructure.Data;
using MyRestFullApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyRestFullApp.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            _usuarioRepository = new UsuarioRepository(context);
        }

        public IUsuarioRepository Usuarios => _usuarioRepository ?? new UsuarioRepository(_context);


        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
