# AssignmentToRise

ProjectName: **Assignment**

## Contact Model
- Contact
    - UUID
    - name
    - surname
    - company
    - contactInfo
        - phone || email || location
        - value

## Report Model
- UUID
- RequestedAt
- ReportStatus (Preparing, Ready)
- ReportContents

## Services
- [x] Contact  - POST (Create a new contact)
- [x] Contact  - DELETE (Delete a contact)
- [x] Contact  - GET (Display that contact in full detail)
- [x] ContactConnection - PUT (Add another connection option to that contact, ie:Gsm, Email, Location)
- [x] ContactConnection - DELETE (Remove a connection option from that contact)
- [x] Contacts - GET (Get all contacts)

- [x] Report  - POST (Rehberdeki kişilerin bulundukları konuma göre istatistiklerini çıkartan bir rapor talebi)
- [x] Report  - GET (Sistemin oluşturduğu bir raporun detay bilgilerinin getirilmesi)
- [x] Reports - GET (Sistemin oluşturduğu raporların listelenmesi)

## Kafka(Report)
1. Kişi rapor servisine istek atar.
2. Rapor servisi bir producer açıp isteği kuyruğa atar.
3. BackgroundTask olarak halihazırda açık olan consumer bu isteği alır ve işler.
4. Sonuç Reports isimli tabloya eklenir.
5. Kişi sonuçları almak için Reports servisine istek atar.
6. Detay isterse, raporun id'sini Report(GET) servisine iletir.

### Kafka Caveats:
* Producer her seferinde açılıp kapanacak, içime simiyor. 
Başka ne yapabilirim acaba? Kafka Streams gibi bi seçenek vardı galiba ama denemedim ki hiç.
* ConsumerGroup .net kütüphanesinde zorunlu. Bunu cli üzerinden yapmam lazım ama. Kütüphane üzerinden yaparsam 
eğer, app kapandığında consumerGroup'u silebiliyor. (7 günlük süre dolmamasına rağmen) Persistent olmalı. Azap çektirmişti bana bu kısım.

## Todo:
- [ ] PUT & PATCH arasındaki farkı oku.
- [ ] Microservice architecture: https://learn.microsoft.com/en-us/dotnet/architecture/microservices/multi-container-microservice-net-applications/microservice-application-design
- [ ] Add Sonar (Static Code Analysis)

Should have this info:
- Location
- Total count at that location
- Total count of phone numbers at that location

## Expectations
- [x] Should be able to run with docker-compose
- [x] Should have a swagger documentation
- [x] Should have a minimum test coverage of 60%
- [x] Should use a microservice architecture, needs minimum 2 services
- [x] Should use a message broker like Kafka or something.
- [x] Should have a **Postgres** or MongoDB database

## Tests

### Application Tests
- [x] Contact  - POST 
- [x] Contact  - DELETE
- [x] Contact  - GET
- [x] ContactConnection - PUT
- [x] ContactConnection - DELETE
- [x] Contacts - GET

- [x] Report  - POST
- [x] Report  - GET
- [x] Reports - GET

### Controller Tests
- [x] Contact  - POST
- [x] Contact  - DELETE
- [x] Contact  - GET
- [x] ContactConnection - PUT
- [x] ContactConnection - DELETE
- [x] Contacts - GET

- [x] Report  - POST
- [x] Report  - GET
- [x] Reports - GET

### Integration Tests
- [x] Contact  - POST
- [x] Contact  - DELETE
- [x] Contact  - GET
- [x] ContactConnection - PUT
- [x] ContactConnection - DELETE
- [x] Contacts - GET

- [x] Report  - POST
- [x] Report  - GET
- [x] Reports - GET