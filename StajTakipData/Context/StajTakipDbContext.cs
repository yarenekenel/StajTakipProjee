using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StajTakip.Core.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StajTakip.Data.Context
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
}
