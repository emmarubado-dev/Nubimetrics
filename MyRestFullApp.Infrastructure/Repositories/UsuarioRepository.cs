using MyRestFullApp.Core.Entities;
using MyRestFullApp.Core.Interfaces.Repositories;
using MyRestFullApp.Infrastructure.Data;

namespace MyRestFullApp.Infrastructure.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationContext context) : base(context)
        {

        }
    }
}
