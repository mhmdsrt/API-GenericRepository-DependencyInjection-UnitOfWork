using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyselfStaj2.Classes;
using MyselfStaj2.Interfaces;
using MyselfStaj2.Services;

namespace MyselfStaj2.Controllers
{
    // [Route("api/[controller]/[Action]")] -> ifadesi https://localhost:7172/api/Coordinate/[Action] url'sini temsil ediyor "/[Action]" ise aşağıdaki oluşturduğumuz metotların ismi oluyor.
    // Yani bu web adresine gittiğin zaman , "/[Action]" ifadesine uygun metot çalışıyor.
    // örnek url : https://localhost:7172/api/Coordinate/GetAllCoordinates
    // [Route("api/[controller]/[Action]")] ifadesindeki [controller] bölümü bizim "public class CoordinateController : ControllerBase" ifadesindeki "Coordinate" controller ismimizi temsil ediyor.

    [Route("api/[controller]/[Action]")]   // [controller] şuan sayfadaki oluşturdugumuz controller'in ismnini yani kendisini alıyor.
                                           // [Action] ise aşağıdaki kullandıgımız metotların ismi.
    [ApiController]
    public class CoodinatesController : ControllerBase
    {
        public readonly ICoordinate _coordinateService;
        /*
         "readonly" değişken tanımlandığı anda ya da constructor ile değeri verilebilir ve asla değiştirilemez 
         
         Bunları Hangi Service Sınıfının metotlarını kullanacağımızı belirlemek için yapıyoruz. Şimdi gelelim bunu nasıl yapıyoruz açıklayalım.
         Burada Controller ile kullanmak istediğimiz Service Sınıfı için bir İnterface tipinden bir değişken oluşturuyoruz,
         Oluşturduğumuz bu değişken aslında İnterface veritipinde bir sınıf oluyor. Bu değişken, Yani ICoordinate arayüzünü implement eden bir sınıfın örneğini tutar.
         Çünkü veri tipi İnterface ise bu sadece interface'i miras alan bir class olabilir.
         Dependency Injection sayesinde hemen aşağıdaki Constructor ile parametre olarak interface veri tipinde bir class alıyoruz.
         Constructor da Parametre olarak aldığımız interface tipindeki coordinateService'i ise Program.cs bulunan 
        "builder.Services.AddScoped<ICoordinate, CoordinateService>();" ile göndermiş oluyoruz.
         Daha sonrasında İnterface tipinde aldığımız bu classı yukarıda oluşturduğumuz "_coordinateService" değişkenine atıyoruz.
         Ve o class'ın yani _coordinateService'in metotlarını aşağıda kullanıyoruz eğer başka service sınıfı oluşturup onun metotlarını kullanmak isteseydik bu yapı sayesinde
         parametre olarak gönderdiğimiz service sınıfının ismini Program.cs bulunan "builder.Services.AddScoped<ICoordinate, CoordinateService>();" içindeki 
        "CoordinateService" 'i değiştirmemiz yeterli olucaktı.
         
         Sonuç olarak bu yapı sayesinde HTTP isteklerini nasıl gerçekleşeceği belirleyebilmek için kullandığımız metotların hangi Service Sınıfının metotlarını
        olacağını belirleyebiliyoruz
         
         */

        public CoodinatesController(ICoordinate coordinateService)
        {
            _coordinateService = coordinateService;
        }


        [HttpGet]  // Verileri listelemek için kullanılan attribute

        public Response GetCoordinateList()
        {
            return _coordinateService.GetCoordinate();
        }

        [HttpGet]

        public Response GetCoordinateById(int id)
        {
            return _coordinateService.GetCoordinateById(id);
        }

        [HttpPost] // Veri eklemek için kullanılan attribute

        public Response Insert(int id, Coordinate c)
        {
            return _coordinateService.InsertCoordinate(c);
        }

        [HttpPut] // Veri Güncellemek için kullanılır.

        public Response Update(int id, Coordinate c)
        {
            return _coordinateService.UpdateCoordinate(id, c);
        }

        [HttpDelete] // Veri Silme için kullanılır.

        public Response Delete(int id)
        {
            return _coordinateService.DeleteCoordinate(id);
        }





    }
}
