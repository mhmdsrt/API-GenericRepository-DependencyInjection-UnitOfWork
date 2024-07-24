# API-GenericRepository-DependencyInjection-UnitOfWork
## Başarsoft şirketindeki yaz stajımda harita projemizin Back-end tarafında öğrendiğim ve uyguladığım konular:

### -> Web Api(with Swagger) 
### -> Dependency Injection 
### -> Generic Repository Design Pattern
### -> UnitOfWork Design Pattern 
________________________________________________________________________________________________________________________________________________________
## Kullandığım 3 adet Service var:
### 1) ICoordinate interface'ni implement eden CoordinateService sınıfı: Burada List<> üzerinden işlem yapıyoruz database kullanmadan.
![Ekran Görüntüsü (340)](https://github.com/user-attachments/assets/5c263510-7e8a-4f53-83b5-65b321f27c88)
### 2) ICoordinate interface'ni implement eden CoordinatePostgreSql Servisi : Postgresql veritabanı ile ADO.NET kullanarak Response döndürdüğüm Service.
![Ekran Görüntüsü (341)](https://github.com/user-attachments/assets/84a34c36-687a-40e9-ba9a-fc767f15a592)
### 3) IEntityService interface'ni implement eden ORM teknolojisi olarak Entity-Framework(CodeFirst Yaklaşımı) kullandığım EntityService Sınıfım:
![Ekran Görüntüsü (342)](https://github.com/user-attachments/assets/a1602902-496b-4fcf-886e-a1a5b2b5624e)
