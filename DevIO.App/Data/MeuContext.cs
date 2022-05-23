using Microsoft.EntityFrameworkCore;
using DevIO.App.ViewModels;

namespace DevIO.App.Data
{
    public class MeuContext : DbContext
    {
        public MeuContext(DbContextOptions opt) : base(opt)
        {

        }
        public DbSet<DevIO.App.ViewModels.EnderecoViewModel>? EnderecoViewModel { get; set; }
    }
}
