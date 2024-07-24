using Microsoft.EntityFrameworkCore;
using MyselfStaj2.Controllers;
using MyselfStaj2.Entity;
using MyselfStaj2.Interfaces;

namespace MyselfStaj2.Services
{

    /*
     ORM (Object-Relational Mapping) -> İlişkili Veri Tabanı ile Nesneye Yönelik Program arasındaki dönüşümü sağlayan teknolojidir.
     */



    /*


   "Generic Repository" mantığı her class için(yani veri tabanındaki her tablo için) entity metotların gövdelerini 
   tekrar tekrar yazmaktan kurtarır.

   Yani "generic repository" yapısı entity 'de kullanılacak crud işlemleri için
   her tabloya göre yani her entity'e göre ayrı ayrı uzun crud metotlarını yazmak yerine context sınıfı ile ilişkilendirip
   sadece o metotları çağırarak kod karmaşıklığını ve sayısını azaltmak.

   Generic Repository bize tüm entity'lerin için ortak olan metotları bir yerden yönetebilmeyi sağlıyor yani
   buradaki entity'lerin ifadesi C# daki sınıfları, veri tabanındaki ise tabloları temsil ediyor.

    _context.Coordinate.Add() = _context.Set<Coordinate>.Add()


    */


    /* 
     Aşkağıdaki Constructor içerisinde EntityContext sınıfımızdan oluşturdugumuz context nesnesi ile hangi sınıf(tablo) ile
     entity işlemlerini yapmak istiyorsak "context.Set<T>()" ifadesindeki "T" yerine o sınıfın 
     yani database'deki tablonun ismini veriyoruz ve böylece "_dbset" değişkeni ile "T" yerine verdiğimiz sınıf üzerinden veri tabanu
     işlemlerini gerçekleştirebiliyoruz.

     Bu sayede kullanmak istediğimiz sınıfa göre(Repository'e göre) T belirleniyor ve o class üzerinden veri tabanı işlemleri yapabiliyoruz. 
     Ve aşağıdaki yazdığımız metotları her ClassRepository'i için tek tek yazmak yerine burada bir kere yazıyoruz.

     "IEnumerable<T>" C# 'da koleksiyonların sıralı erişimini ve işlenmesini sağlayan bir arayüzdür,
    genellikle döngülerde veya LINQ sorgularında kullanılır.

    "readonly" değişken tanımlandığı anda ya da constructor ile değeri verilebilir ve asla değiştirilemez.
                                         
      */

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        
        public readonly DbSet<T> _dbset;
        

        public GenericRepository(EntityContext context)
        {
            _dbset = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbset.ToList();

            
        }
        public T GetById(int id)
        {
            
            return _dbset.Find(id);
        }

        public void Insert(T entity)
        {
            _dbset.Add(entity);
        }

        public void Update(T entity)
        {
            _dbset.Update(entity);
        }
        public void Delete(T entity)
        {
            _dbset.Remove(entity);
        }
    }
}
