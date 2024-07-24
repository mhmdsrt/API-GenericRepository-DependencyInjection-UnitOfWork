using MyselfStaj2.Classes;
using MyselfStaj2.Interfaces;

namespace MyselfStaj2.Services
{
    public class CoordinateService : ICoordinate
    {
        public static readonly List<Coordinate> CoordinateList = new List<Coordinate>();
        public Response GetCoordinate()
        {
            var result = new Response();
            try
            {
                if (CoordinateList.Count == 0)
                {
                    result.Message = "Not found Coordinate";
                    return result;
                }

                result.Data = CoordinateList;
                result.IsSuccess = true;
                result.Message = "coordinates available";

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
                var coordinate = CoordinateList.FirstOrDefault(c => c.id == id);  //FirstOrDefault(koşul) metodu -> koşulu sağlayan ilk öğeyi döndürür.
                                                                                  //Eğer koşulu sağlayan hiç öğe yoksa sahip olduğu veri tipinin default değerini döndürür.
                                                                                  // Find() metodu ise primary key ile çalışır.Birincil parametre olarak verilen birincil anahtara-
                                                                                  // göre sorgulama yapar eğer bulamazsa geriye NULL döner.
                                                                                  
                if (coordinate==null)
                {
                    result.Message = "Not found Coordinate";
                    return result;
                }
                result.Data = coordinate;
                result.IsSuccess = true;
                result.Message= "coordinate available";

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;

            }
            return result;
        }

        public Response InsertCoordinate(Coordinate c)
        {
            var result = new Response();
            int id = c.id; // eklenecek olan nesnenin id numarasını alıp kontrol edicez daha önce eklenmis mi?


            try
            {
                

                if (CoordinateList.Any(c=>c.id==id)) // Geriye bool döner.  Any() parantez içindeki koşulu kontrol eder ,eğer öğe varsa true yoksa false döner.
                {
                    result.Message = "Such a coordinate already exists,Unsuccessful!"; //"böyle bir coordinate zaten var,başarısız"
                    return result;
                }

                Coordinate newCoordinate = new Coordinate();
                newCoordinate.id = c.id;
                newCoordinate.Name = c.Name;
                newCoordinate.X = c.X;
                newCoordinate.Y = c.Y;
                CoordinateList.Add(newCoordinate);
                result.Data = newCoordinate;
                result.IsSuccess = true;
                result.Message = "Adding is Successful!";


            }
            catch (Exception ex)
            {
                result.Message = ex.Message;

            }
            return result;
        }

        public Response DeleteCoordinate(int id)
        {
            var result = new Response();
            var coordinate = CoordinateList.FirstOrDefault(c => c.id == id);
            try
            {
                if (coordinate == null)
                {
                    result.Message = "There is no such record anyway."; // böyle bir kayıt zaten yok.
                    return result;
                }
                CoordinateList.Remove(coordinate);
                result.IsSuccess = true;
                result.Message = "Remove is Successful!";

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;

            }
            return result;
        }

        public Response UpdateCoordinate(int id, Coordinate c)
        {
            var result = new Response();
            var coordinate = CoordinateList.FirstOrDefault(c => c.id == id);
            try 
            {
                // önce güncellenecek olan coordinate var mı bunu kontrol edelim.
                if (coordinate == null)
                {
                    result.Message = "There is no such record anyway,Update is Unsuccessful"; // böyle bir kayıt zaten yok, uptade başarısız.
                    return result;
                }
                coordinate.id = c.id;
                coordinate.X = c.X;
                coordinate.Y = c.Y;
                coordinate.Name = c.Name;
                result.Data = coordinate;
                result.IsSuccess = true;
                result.Message = "Update is Successful!";


            }
            catch (Exception ex)
            {
                result.Message = ex.Message;


            }
            return result;
        }

    }
}
