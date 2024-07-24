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
### Bu sayede CoordinatesController içerisinde hangisi service ile çalışmak istiyorsak Constructor içerisinde parametre olarak Program.cs de tarafından gönderilen ICoordinate tipinde bir sınıf alıyoruz ve o Service'in metotlarını kullanıyoruz.
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
________________________________________________________________________________________________________________________________________________________
![Ekran Görüntüsü (349)](https://github.com/user-attachments/assets/d9eda6cc-a8d7-4e2a-b67e-1ccb969ad402)
________________________________________________________________________________________________________________________________________________________
## Swagger
![Ekran Görüntüsü (326)](https://github.com/user-attachments/assets/4ba618cd-53b5-448f-8847-0718730fd85b)
![Ekran Görüntüsü (327)](https://github.com/user-attachments/assets/7fa58d85-8232-44da-8750-ef81dc9a96a6)
![Ekran Görüntüsü (328)](https://github.com/user-attachments/assets/690f5166-9b8f-4214-815a-5b0032390f92)
![Ekran Görüntüsü (329)](https://github.com/user-attachments/assets/5b4708f6-6afe-4ebc-a206-d55c95ef98e7)
![Ekran Görüntüsü (330)](https://github.com/user-attachments/assets/0f1f65b5-3da8-4484-8447-40b5e12047d8)
![Ekran Görüntüsü (332)](https://github.com/user-attachments/assets/5b7a0415-8df3-41ae-b322-969031723a3a)
![Ekran Görüntüsü (333)](https://github.com/user-attachments/assets/4bf9fcab-3af8-4955-9be5-69a3a745fa79)
![Ekran Görüntüsü (334)](https://github.com/user-attachments/assets/303ae8c6-5ea7-4343-959b-5759744b4f36)
![Ekran Görüntüsü (335)](https://github.com/user-attachments/assets/2c6be8eb-d6be-4ff8-9b6f-0eb16917dd69)



