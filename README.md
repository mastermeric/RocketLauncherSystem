# "ROCKET LAUNCHER SYSTEM"  Server uygulaması.

PROJE HAKKINDA:
- Proje iki modülden oluşmaktadır:  "RealtimePushServer" ve "ConsoleMultiThread". 
- RealtimePushServer: SignalR web socket impelementasyonunu başlatır. Ve Realtime haberleşme olanağı sağlar.
  Browser üzerinde birden fazla açılabilir, bu sayede veriler farklı Browseralrdan cablı izlenebilir.
  Proje http://localhost:44990  adresinden çalışır.
- ConsoleMultiThread: Tespit edilen Roket sayısı kadar TCP Client açar ve SignalR üzerinden Canlı TCP datası önyüze push edilir.

# NOTLAR - Known Issues:
- API rety lar için Microservis yakalaşımı Retry patetrn + Circuit Breaker kullanılaibilir.
- Refactoring => Best practice olarak Json datadaki Int/Double alanlar string e çevrildi. Type safety düşünülebilir.
- UI arayüzde görselleştirme yarım kaldı. 
- Persistent data yapısı kurumadı. (UI refresh olunca veri tazelenemiyor.)
- Rocket gönderme operasyonu henüz tamamlanamadı.
- Unit test ilave edilemedi.

# PROJEYİ ÇALIŞTIRMA ve TEST etme :
-"RocketLauncherServer.sln"  çift tıklanarak VS2022 de proje açılır.
- Sln dosyası üzerinde Sağ Click -> Rebuild Solution  yapılır.  
- Run/Baştan butonu ile sıralı olarak projeler ayağa kalkar.(Sırayla "RealtimePushServer" --> "ConsoleMultiThread")


ÖRNEK EKRAN GÖRÜNTÜSÜ :
Sol tarafta  "ConsoleMultiThread"  konsol uygulaması API den aldığı Roket Telemetry verilerini dinler.
Sağ tarafta görünen önyüz üzerinde ilgili veriler görüntülenir.

![demo2](https://user-images.githubusercontent.com/49819371/167137431-a980cb3f-d152-49ef-bc3d-e0681c4b93bf.jpg)
