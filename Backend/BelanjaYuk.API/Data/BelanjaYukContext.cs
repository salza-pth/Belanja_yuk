using BelanjaYuk.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BelanjaYuk.API.Data
{
    public class BelanjaYukContext : DbContext
    {
        public BelanjaYukContext(DbContextOptions<BelanjaYukContext> options)
            : base(options)
        {
        }

        public DbSet<MsProduct> MsProduct { get; set; } = default!;
        public DbSet<LtCategory> LtCategory { get; set; } = default!;
        public DbSet<LtPayment> LtPayment { get; set; } = default!;
        public DbSet<LtGender> LtGender { get; set; } = default!;
        public DbSet<MsUserSeller> MsUserSeller { get; set; } = default!;
        public DbSet<MsUser> MsUser { get; set; } = default!;
        public DbSet<MsUserPassword> MsUserPassword { get; set; } = default!;
        public DbSet<TrBuyerCart> TrBuyerCart { get; set; } = default!;
        public DbSet<TrBuyerTransaction> TrBuyerTransaction { get; set; } = default!;
        public DbSet<TrBuyerTransactionDetail> TrBuyerTransactionDetail { get; set; } = default!;
    }
}