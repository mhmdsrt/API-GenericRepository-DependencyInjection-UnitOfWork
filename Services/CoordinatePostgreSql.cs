using MyselfStaj2.Classes;
using MyselfStaj2.Controllers;
using MyselfStaj2.Interfaces;
using Npgsql;
namespace MyselfStaj2.Services
{
    public class CoordinatePostgreSql : ICoordinate
    {
        NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;" +
            "Database=CoordinatesBasarsoft;User Id=postgres; password=1638");


        public Response GetCoordinate()
        {
            var result = new Response();
            connection.Open();
            List<Coordinate> coordinateList = new List<Coordinate>();

            try
            {

                NpgsqlCommand getCoordinates = new NpgsqlCommand("Select * From \"Coordinates\"", connection);
                NpgsqlDataReader dr = getCoordinates.ExecuteReader();
                while (dr.Read())
                {
                    Coordinate coordinate = new Coordinate(); // dr.Read() yaptıkça yani okudukça her okudugu coordinate için aşağıdaki işlemleri yapıyoruz.
                    coordinate.id = Convert.ToInt32(dr[0]);
                    coordinate.Name = Convert.ToString(dr[1]);
                    coordinate.X = Convert.ToDouble(dr[2]);
                    coordinate.Y = Convert.ToDouble(dr[3]);
                    coordinateList.Add(coordinate);
                   

                }
                dr.Close();
                result.Data = coordinateList;
                result.IsSuccess = true;
                result.Message = "coordinates available";
                

                if (coordinateList.Count == 0)
                {
                   
                    result.Message = "Not found Coordinate";
                    
                }
                return result;


            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
                
            }
            finally //  finally yapıs, try içindeki yapı çalışssada yada catch yapısına düşsede her durumda calısacak olan yapıdır
            {
                connection.Close();
            }

        }

        public Response GetCoordinateById(int id)
        {
            var result = new Response();
            

            try
            {
                connection.Open();
                NpgsqlCommand getCoordinates = new NpgsqlCommand("Select * from \"Coordinates\" Where \"id\"=@p1", connection);
                getCoordinates.Parameters.AddWithValue("@p1", id);
                NpgsqlDataReader dr = getCoordinates.ExecuteReader();
                if (dr.Read())
                {
                    Coordinate coordinate = new Coordinate();
                    coordinate.id = Convert.ToInt32(dr[0]);
                    coordinate.Name = Convert.ToString(dr[1]);
                    coordinate.X = Convert.ToDouble(dr[2]);
                    coordinate.Y = Convert.ToDouble(dr[3]);

                    result.Data = coordinate;
                    result.IsSuccess = true;
                    result.Message = "coordinates available";
                    return result;
                }
                else
                {
                    dr.Close();
                    result.Message = "there is no such data";
                    return result;

                }
                

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;

            }
            finally //  finally yapıs, try içindeki yapı çalışssada yada catch yapısına düşsede her durumda calısacak olan yapıdır
            {
                connection.Close();
            }
        }

        public Response UpdateCoordinate(int id, Coordinate c)
        {
            var result = new Response();
            

            try  //  Escape Operating 'lerden \" .... \" ifadesi arasında yazdığımız şeyler string ifadenin içerisinden "" (çift tırnak) kullanbilmemizi sağlıyor.
            {
                connection.Open();
                NpgsqlCommand checkData = new NpgsqlCommand("Select * from \"Coordinates\" Where \"id\"=@p6", connection); // güncellenecek veri varmı kontrol edelim.
                checkData.Parameters.AddWithValue("@p6", id);
                NpgsqlDataReader drCheckData = checkData.ExecuteReader();
                if (drCheckData.Read())
                {
                    drCheckData.Close();
                    NpgsqlCommand updateCoordinate = new NpgsqlCommand("Update \"Coordinates\" Set \"id\"=@p1,\"Name\"=@p2,\"X\"=@p3,\"Y\"=@p4 Where \"id\" =@p5", connection);
                    updateCoordinate.Parameters.AddWithValue("@p1", c.id);
                    updateCoordinate.Parameters.AddWithValue("@p2", c.Name);
                    updateCoordinate.Parameters.AddWithValue("@p3", c.X);
                    updateCoordinate.Parameters.AddWithValue("@p4", c.Y);
                    updateCoordinate.Parameters.AddWithValue("@p5", id);
                    updateCoordinate.ExecuteNonQuery();

                    //güncellenen veri aşağıdaki gibidir.Güncellenen veriyi göstermek için.
                    Coordinate coordinate = new Coordinate();
                    coordinate.id = c.id;
                    coordinate.Name = c.Name;
                    coordinate.X = c.X;
                    coordinate.Y = c.Y;

                    result.Data = coordinate;
                    result.IsSuccess = true;
                    result.Message = "Update is Successful!";
                    return result;


                }

                else
                {
                    drCheckData.Close();
                    result.Message = "There is no such record anyway,Update is Unsuccessful"; // böyle bir kayıt zaten yok, uptade başarısız.
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
            finally //  finally yapıs, try içindeki yapı çalışssada yada catch yapısına düşsede her durumda calısacak olan yapıdır
            {
                connection.Close();
            }

            

        }

        public Response DeleteCoordinate(int id)
        {
            var result = new Response();
            connection.Open();

            try
            {
                NpgsqlCommand checkData = new NpgsqlCommand("Select * From \"Coordinates\" Where \"id\"=@p1", connection); // Silinecek veri varmı önce bunu kontrol edelim.
                checkData.Parameters.AddWithValue("@p1", id);
                NpgsqlDataReader dr = checkData.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    NpgsqlCommand deleteCoordinate = new NpgsqlCommand("Delete From \"Coordinates\" Where \"id\"=@p2", connection);
                    deleteCoordinate.Parameters.AddWithValue("@p2", id);
                    deleteCoordinate.ExecuteNonQuery();

                    result.Message = "Delete is Succcess!";
                    result.IsSuccess = true;
                    return result;

                }

                else
                {
                    dr.Close();
                    result.Message = "No data to delete was found";
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;

            }
            finally //  finally yapıs, try içindeki yapı çalışssada yada catch yapısına düşsede her durumda calısacak olan yapıdır
            {
                connection.Close();
            }
        }

        public Response InsertCoordinate(Coordinate c)
        {
            var result = new Response();
            

            try  // escape operators
            {
                connection.Open();
                // Eklenecek kayıt daha önce eklenmiş mi kontrol edelim.
                NpgsqlCommand checkData = new NpgsqlCommand("Select * From \"Coordinates\" Where \"id\"=@p1", connection);
                checkData.Parameters.AddWithValue("@p1", c.id);
                NpgsqlDataReader dr = checkData.ExecuteReader();
                if (dr.Read())
                {
                    result.Message = "Such a record already exists,unsuccessful!";
                    return result;
                }
                else
                {
                    dr.Close();     //NpgsqlDataReader nesnesi açıldığında, bağlantıyı kilitler ve başka bir komutun aynı bağlantı üzerinde çalışmasını engeller.
                                    //Bu nedenle, NpgsqlDataReader'ı kapatmadan başka bir komut çalıştırmaya çalışırsanız, "A command is already in progress" hatası alırsınız.
                    NpgsqlCommand insertCoordinate = new NpgsqlCommand("Insert into \"Coordinates\" (\"id\",\"Name\",\"X\",\"Y\") VALUES (@p1,@p2,@p3,@p4)", connection);
                    insertCoordinate.Parameters.AddWithValue("@p1", c.id);
                    insertCoordinate.Parameters.AddWithValue("@p2", c.Name);
                    insertCoordinate.Parameters.AddWithValue("@p3", c.X);
                    insertCoordinate.Parameters.AddWithValue("@p4", c.Y);
                    insertCoordinate.ExecuteNonQuery();
                    result.Data = c;
                    result.IsSuccess = true;
                    result.Message = "Insert is successful!";
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;

            }
            finally //  finally yapıs, try içindeki yapı çalışssada yada catch yapısına düşsede her durumda calısacak olan yapıdır
            {
                connection.Close();
            }

        }


    }
}
