using Microsoft.Data.SqlClient;
using System.Data;

namespace StajTakip.Data.Context
{
    // EF Core'a bağımlılık kalmadı. Bu sınıf artık sadece connection string'i tutup Dapper için bir SqlConnection üretiyor.
    public class StajTakipDbContext
    {
        private readonly string _connectionString;

        public StajTakipDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}

/*namespace StajTakip.Data.Context
{
    //DbContext hazır bir şablon, içinde "veritabanına bağlan", "veri kaydet", "veri sil" gibi hazır yetenekler var.Biz bu şablonu alıp kendi ismimizi (StajTakipDbContext) veriyoruz ve üstüne kendi tablomuzu (Stajyerler) ekliyoruz.
    public class StajTakipDbContext : DbContext
    {
        public IDbConnection CreateConnection()
             => new SqlConnection(Database.GetDbConnection().ConnectionString);


        public DbSet<Stajyer> Stajyerler { get; set; }
        //Veritabanında Stajyerler adında bir tablo olsun, ve bu tablonun her satırı Stajyer sınıfındaki gibi kolonlara sahip olsun (Ad, Soyad, DogumTarihi vs.)
        public StajTakipDbContext(DbContextOptions<StajTakipDbContext> options) : base(options)
        {
        }
    }

        //bir "ayar paketi" alıyoruz.Bu paketin içinde şu bilgi var: "hangi veritabanına, hangi adresle bağlanacaksın" (connection string).Bu ayar paketini, miras aldığımız DbContext şablonuna iletiyoruz.Yani "al bu ayarları, sen bağlan" diyoruz.
        //Biri bana bağlantı bilgilerini versin, ben de o bilgiyi asıl EF Core motoruna iletip veritabanına bağlanayım" demek. Kendi elimizle bağlantı kurma kodu yazmıyoruz, EF Core bizim için hallediyor — biz sadece bilgiyi ona aktarıyoruz.
} */
