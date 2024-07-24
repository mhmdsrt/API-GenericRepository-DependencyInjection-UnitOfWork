# Web API-GenericRepository-DependencyInjection-UnitOfWork
## Başarsoft şirketindeki yaz stajımda harita projemizin Back-end tarafında öğrendiğim ve uyguladığım konular:

### -> Web Api(with Swagger) 
### -> Dependency Injection 
### -> Generic Repository Design Pattern
### -> UnitOfWork Design Pattern 
### -> PostgreSQL
________________________________________________________________________________________________________________________________________________________
## Kullandığım 3 adet Service var:
### 1) ICoordinate interface'ni implement eden CoordinateService sınıfı: Burada List<> üzerinden işlem yapıyoruz database kullanmadan.
![Ekran Görüntüsü (340)](https://github.com/user-attachments/assets/5c263510-7e8a-4f53-83b5-65b321f27c88)
### 2) ICoordinate interface'ni implement eden CoordinatePostgreSql Servisi : Postgresql veritabanı ile ADO.NET kullanarak Response döndürdüğüm Service.
![Ekran Görüntüsü (341)](https://github.com/user-attachments/assets/84a34c36-687a-40e9-ba9a-fc767f15a592)
### 3) IEntityService interface'ni implement eden ORM teknolojisi olarak Entity-Framework(CodeFirst Yaklaşımı) kullandığım EntityService Sınıfım:
![Ekran Görüntüsü (342)](https://github.com/user-attachments/assets/a1602902-496b-4fcf-886e-a1a5b2b5624e)
________________________________________________________________________________________________________________________________________________________
### Dependency Injection yapısı için özet olarak  eden Controller içerisinde ICoordinate interface'ni implement eden hangi Service ile çalışmak istiyorsak Program.cs 'de o servisi vericez 
### örnek: builder.Services.AddScoped<ICoordinate, CoordinatePostgreSql>(); veya  builder.Services.AddScoped<ICoordinate, CoordinateService>();
### Bu sayede CoordinatesController içerisinde hangisi service ile çalışmak istiyorsak Constructor içerisinde parametre olarak Program.cs de tarafında kullanmak istediğimiz service'i göndericez.
![Ekran Görüntüsü (346)](https://github.com/user-attachments/assets/a57fd576-4ffc-4be5-bc20-37b743e9a784)
________________________________________________________________________________________________________________________________________________________
### Program.cs'De Dependency Injection yapısı için yazdığım yorumlar:

![Ekran Görüntüsü (347)](https://github.com/user-attachments/assets/e9ebc088-8061-4b80-83eb-4b545e255d48)
________________________________________________________________________________________________________________________________________________________
### Coordinate Class'ı:
![Ekran Görüntüsü (345)](https://github.com/user-attachments/assets/dfb11e99-788c-45e3-a364-a67475f98e48)
________________________________________________________________________________________________________________________________________________________
### Geriye Response döndürmek için kullandığım Response Class'ı:
![Ekran Görüntüsü (344)](https://github.com/user-attachments/assets/2cb0bf41-37ed-44db-a8c8-d769b4099c7c)
________________________________________________________________________________________________________________________________________________________
### Oluşturulabilecek tüm Repository'ler için "Generic Repository" oluşturulma sebebi her Respository için veritabanı işlemlerini (CRUD) ayrı ayrı Repositoryler içerisinde metot oluşturmak yerine bir defa GenericRepository Class'ında yazma ve "T" yerine gelecek Repository'e(Class'a) göre o Repositorynin(Classın) bu metotlarına kullanabilmesini sağlama.
![Ekran Görüntüsü (348)](https://github.com/user-attachments/assets/23d6e1d1-8830-47f9-b504-e1ad01c5a906)

