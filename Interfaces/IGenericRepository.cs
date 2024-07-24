namespace MyselfStaj2.Interfaces
{
    /*
    

    "Generic Repository" mantığı her class için(yani veri tabanındaki her tablo için) entity metotların gövdelerini 
    tekrar tekrar yazmaktan kurtarır.
    
    Yani "generic repository" yapısı entity 'de kullanılacak crud işlemleri için
    her tabloya göre yani her entity'e göre ayrı ayrı uzun crud metotlarını yazmak yerine context sınıfı ile ilişkilendirip
    sadece o metotları çağırarak kod karmaşıklığını ve sayısını azaltmak.

    Generic Repository bize tüm entity'lerin için ortak olan metotları bir yerden yönetebilmeyi sağlıyor yani
    buradaki entity'lerin ifadesi C# daki sınıfları, veri tabanındaki ise tabloları temsil ediyor.

    

    "IEnumerable<T>" C# 'da koleksiyonların sıralı erişimini ve işlenmesini sağlayan bir arayüzdür,
    genellikle döngülerde veya LINQ sorgularında kullanılır
     */


    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll(); //IEnumerable<T> GetAll(); metodu, belirtilen türde (T) bir koleksiyonu döndüren bir yöntem tanımıdır.
                          //Bu yöntem, IEnumerable<T> arayüzünü uygular ve geriye T türünde öğeler içeren bir koleksiyon döndürür.
        T GetById(int id);

        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);

    }

}
