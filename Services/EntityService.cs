using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MyselfStaj2.Classes;
using MyselfStaj2.Entity;
using MyselfStaj2.Interfaces;
using System.Collections;
using System.Security.Cryptography;

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


     "IEnumerable<T>" C# 'da koleksiyonların sıralı erişimini ve işlenmesini sağlayan bir arayüzdür,
    genellikle döngülerde veya LINQ sorgularında kullanılır.

     */



    /*
 EntityService yazma amacımız controller içerisinde biz her zaman services'ler ile çalışıyoruz.Burada EntityService içerisinde Kullanılacak Repository'nun
 metotlarını çağıracaz-kullanıcaz ama geriye Response döndürcez. Veri tabanı işlemlerini ve Repository Class'ların içerisinde yapıyoruz.Yani günün sonunda
 EntityController içerisinde biz oluşturduğumuz Service'leri kullanırken örneğin EntityService'ni kullanırken EntityService'in metotlarını çağıracaz ve
 bu EntityService metotları Repository'lerin metotlarını kullnarak veri tabanı işlemlerini yapıp o işlemlere göre geriye bir Response döndürcek.
     
     */




    public class EntityService<T> : IEntityService<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<T> genericRepository;

        public EntityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            genericRepository = _unitOfWork.GetRepository<T>();
        }

        public Response GetAllCoordinate()
        {
            var result = new Response();
            try
            {
                var log = genericRepository.GetAll();
                if (log.Any())          
                                        //Any() metodu, herhangi bir koşul parametresi olmadan kullanıldığında, koleksiyonun içinde herhangi bir öğe olup olmadığını kontrol eder.
                                        //Eğer koleksiyonda en az bir öğe varsa true, yoksa false döner.
                {
                    result.Data = log;
                    result.Message = "Get All Coordinate is Succcess!";
                    result.IsSuccess = true;

                }
                else
                {
                    result.Message = "Not found";
                    return result;
                   
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;

            }
            return result;
        }

        public Response GetCoordinateById(int id)
        {
            var result = new Response();
            try
            {
                var log = genericRepository.GetById(id);

                if (log == null)
                {
                    result.Message = "Not found";
                    return result;
                }
                else
                {
                    result.Data = log;
                    result.IsSuccess = true;
                    result.Message = "Get coordinate is Success!";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;

            }
            return result;
        }

        public Response InsertCoordinate(int id, T entity)
        {
            var result = new Response();
            try
            {

                var log = genericRepository.GetById(id);



                if (log != null) // entity referans tipinde oldugu için default  değeri NULL olur.
                {
                    result.Message = "such a record already exists!";
                    return result;
                }
                else
                {
                    genericRepository.Insert(entity);
                    _unitOfWork.Save();
                    result.Data = entity;
                    result.IsSuccess = true;
                    result.Message = "Insert is successful!";
                }

            }
            catch (Exception ex)
            {

                result.Message = ex.Message;
            }
            return result;
        }
        /*
         Temel Farklar
        Contains(), belirli bir değeri arar; Any(), belirli bir koşulu kontrol eder.
        Contains() doğrudan değeri kontrol eder; Any() ise lambda ifadesi ile koşul kontrolü yapar.
        Any(), herhangi bir koşul belirtilmediğinde, koleksiyonun boş olup olmadığını kontrol etmek için de kullanılabilir.
                 
         */
        public Response UpdateCoordinate(int id, T entity)
        {
            var result = new Response();
            try
            {
                //var log = genericRepository.GetById(id);            // BU KISIM HATA VERDİĞİ İÇİN COMMENT'E ALDIK ASENKRON CALISMA LAZIM GETBYID İLE UPDATE BİRBİRİNİ EZMEYE CALISIYOR!!!!
                ////GetById metodu, genellikle bir veri deposunda belirtilen id değerine sahip öğeyi bulup döndüren bir metottur.
                ////Bu tür metotlar genellikle veri erişim katmanlarında veya repository pattern'de kullanılır. 
                //if (log == null)
                //{
                //    result.Message = "No record found to update!";
                //    return result;
                //}

                genericRepository.Update(entity);
                _unitOfWork.Save();
                result.Data = entity;
                result.IsSuccess = true;
                result.Message = "Update is successful!";




            }
            catch (Exception ex)
            {
                result.Message = ex.Message;


            }

            return result;
        }

        //FirstOrDefault(koşul) metodu -> koşulu sağlayan ilk öğeyi döndürür.
        //Eğer koşulu sağlayan hiç öğe yoksa sahip olduğu veri tipinin default değerini döndürür.
        // Find() metodu ise primary key ile çalışır.Birincil parametre olarak verilen birincil anahtara-
        // göre sorgulama yapar eğer bulamazsa geriye NULL döner.
        public Response DeleteCoordinate(int id)
        {
            var result = new Response();

            try
            {
               var log= genericRepository.GetById(id);
                if (log == null)
                {
                    result.Message = "No record found to delete";
                    return result;
                }
                else
                {

                    genericRepository.Delete(log);
                    _unitOfWork.Save();
                    result.IsSuccess = true;
                    result.Message = "Delete is Successful!";
                }

            }
            catch (Exception ex)
            {

                result.Message = ex.Message;
            }
            return result;
        }

    }
}
