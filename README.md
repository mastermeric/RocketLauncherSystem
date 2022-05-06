# "ROCKET LAUNCHER SYSTEM"  Server uygulaması.

PROJE HAKKINDA:
- Proje iki modülden oluşmaktadır:  "RealtimePushServer" ve "ConsoleMultiThread". 
-"RealtimePushServer": SignalR web socket impelementasyonunu başlatır. Ve Realtime haberleşme olanağı sağlar.
-"ConsoleMultiThread": Tespit edilen Roket sayısı kadar TCP Client açar ve SignalR üzerinden Canlı TCP datası önyüze push edilir.

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
