using MyselfStaj2.Entity;
using MyselfStaj2.Interfaces;

namespace MyselfStaj2.Services
{
    /*
      
İşlem Yönetimi: UnitOfWork deseni, birden fazla repository'nin bir arada çalıştığı durumlarda tüm işlemleri 
tek bir işlem (transaction) içerisinde yönetmeyi sağlar. Böylece, bir işlem sırasında birden fazla repository
üzerinde değişiklik yapıldığında, bu değişikliklerin tamamı ya birlikte başarılı olur ya da birlikte geri alınır. 
Bu, veri bütünlüğünü sağlar.

Değişiklik Takibi: UnitOfWork, yapılan değişiklikleri takip ederek gerektiğinde veritabanına kaydetmeyi sağlar.
Bu, sadece değişen verilerin kaydedilmesini ve böylece gereksiz veritabanı işlemlerinin önlenmesini sağlar.

Merkezi Yönetim: Tüm repository'lerin aynı UnitOfWork örneği üzerinden çalışması, veri erişim katmanını
merkezi bir yerden yönetmeyi ve kontrol etmeyi kolaylaştırır.

Kod Tekrarının Önlenmesi: Doğru, UnitOfWork, SaveChanges() gibi ortak işlemleri merkezi bir noktada toplar
ve bu işlemlerin tekrar tekrar yazılmasını önler. Ancak bu, sadece faydalardan biridir.
     
     
     */
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EntityContext _context;
        public UnitOfWork(EntityContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(_context);
        }
        /*
         Yani biz burda direkt hangi Repository ile çalışmak istiyorsak _context parametresini GenericRepository classına
         göndericez ve tüm veritabanı işlemlerini aynı _context nesnesi üzerinden gerçekleştiricez.


        
        Burada geriye IGenericRepository'i miras alan bir Repository sınıfı döndürecek bununda hangi repository sınıfı olduğunu "T" belirleyecek
        ve hangi RepositoryClassı'ı için metodu kullanacağımızı ise "GetRepository<T>" ifadesindeki "T" belirliyor.


        GetRepository<T> metodu, IGenericRepository<T> arayüzünü uygulayan bir GenericRepository<T> nesnesi döndürecektir.
        Bu, metodun çağrıldığı sırada belirtilen T türüne bağlı olarak hangi repository sınıfının kullanılacağını belirler.
        Bu yapı, repository'nin belirli bir tür için oluşturulmasını sağlar ve T türü, metodun çağrıldığı yerde belirlenir.
        Yani çalışmak istediğimiz repository'e göre belirleyebilmek için böyle yaptık



        Dönüş Tipinde Generic Kullanımı:IGenericRepository<T> dönüş tipi, metodun geriye döndüreceği nesnenin türünü belirtir.
        Bu, metodun döndüreceği repository'nin hangi türde veriyle çalışacağını belirtir.
        
     
        Metodun Kendisine Generic Kısıtlaması Eklemek:GetRepository<T> metodunun kendisine eklenen T kısıtlaması, 
        metodun çağrıldığı sırada hangi türde repository oluşturulacağını belirlemek içindir.
        Bu, metodu çağıran koda, repository'nin hangi türde veriyle çalışacağını belirtme imkanı sağlar.
         
         */

    }
}
