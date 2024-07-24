using Microsoft.EntityFrameworkCore;
using MyselfStaj2.Classes;

namespace MyselfStaj2.Services
{
    /*
     ORM (Object-Relational Mapping) -> İlişkili Veri Tabanı ile Nesneye Yönelik Program arasındaki dönüşümü sağlayan teknolojidir.
     */
    public class EntityContext : DbContext
    {
        protected readonly IConfiguration configuration;

        public EntityContext(IConfiguration conf)
        {
            configuration = conf;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // connect to postgresql with connection string from appsettings.json
            // appsettings.json dostasındaki "MyConnection" anahtarına karşılık gelen bağlantı dizesini alır.
            // ORM kullanabilmemiz için hangi veri tabanı sağlayıcısın kullanılacağını ve bağlantı dizesinin nereden alacağını belirliyoruz.
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("MyConnection"));
        }

        public DbSet<Coordinate> Coordinates { get; set; }
        // Buradaki <Coordinate> ifadesi  projede yer alan Coordinate sınıfıdır.
        // Ve bu <Coordinate> sınıfı veri tabanında "Coordinates" tablosu olarak temsil edilir.
        // <Coordinate> sınıfı tablo olarak, <Coordinate> sınıfının propertyleri ise sütun olarak veri tabanına yansır.
        // Yani Nesneye yönelik Programlama ve Sql arasında dönüşüm sağlanır.
    }
}
