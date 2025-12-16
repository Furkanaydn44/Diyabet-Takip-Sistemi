# 🏥 Diyabet Takip Sistemi (Diabetes Tracking System)

Bu proje, diyabet hastalarının sağlık verilerini (kan şekeri, diyet, egzersiz, semptom) etkin bir şekilde izlemek, analiz etmek ve hem hastalara hem de hekimlere zamanında klinik karar desteği (insülin önerisi, kritik uyarılar) sağlamak amacıyla geliştirilmiş bir masaüstü uygulamasıdır.

## 🚀 Proje Hakkında

Geleneksel diyabet takibindeki manuel kayıt hatalarını ve veri kopukluğunu önlemeyi amaçlayan bu sistem; hastaların verilerini merkezi bir veritabanında toplar. Geliştirilen **Kural Tabanlı Algoritmalar** sayesinde, hastanın girdiği verilere göre otomatik insülin dozu önerileri sunar ve kritik durumlarda doktoru uyarır.

Proje, **Kocaeli Üniversitesi Bilgisayar Mühendisliği** programlama laboratuvarı kapsamında; ilişkisel veritabanı tasarımı, normalizasyon (3NF) ve nesne yönelimli programlama prensipleri kullanılarak geliştirilmiştir.

## 🛠️ Kullanılan Teknolojiler

* **Dil:** C# (.NET Framework / Core)
* **Veritabanı:** MySQL
* **Kütüphane:** MySql.Data.MySqlClient
* **Mimari:** Katmanlı Mimari / İlişkisel Veritabanı Modeli
* **Güvenlik:** SHA-256 (Şifre Hashleme)

## ⚙️ Özellikler ve Algoritmalar

### 1. 💉 İnsülin Öneri Algoritması
Sistem, hastanın gün içindeki (Sabah, Öğle, Akşam, Gece) kan şekeri ölçümlerinin ortalamasını alır.
* Ortalama değere ve önceden tanımlanmış kurallara göre hastaya en uygun insülin dozunu (ml) hesaplar.
* Zaman aralığı dışında yapılan ölçümleri tespit eder ve ortalamaya dahil etmez.

### 2. 🥗 Diyet ve Egzersiz Öneri Sistemi
Kural tabanlı bir yapı ile çalışır. Hastanın anlık kan şekeri seviyesi ve girdiği belirtiler (örn: Yorgunluk, Polifaji) analiz edilir.
* **Örnek:** Kan şekeri yüksek ve "Kilo Kaybı" belirtisi varsa -> "Az Şekerli Diyet" ve "Yürüyüş" önerilir.

### 3. ⚠️ Akıllı Uyarı Sistemi
* **Kritik Eşik Kontrolü:** Kan şekeri <70 mg/dL veya >200 mg/dL olduğunda "Acil Uyarı" oluşturur.
* **Veri Tutarlılığı:** Günlük ölçüm sayısı yetersizse doktor ekranına bildirim düşer.

### 4. 👤 Kullanıcı Modülleri
* **Doktor Modülü:** Hasta ekleme, tüm hastaların verilerini grafiklerle görüntüleme, geçmiş tedavi takibi.
* **Hasta Modülü:** Günlük veri girişi (Şeker, Diyet, Egzersiz), insülin önerilerini görüntüleme, kişisel gelişim grafikleri.

## 📂 Veritabanı Yapısı

Proje 3. Normal Form (3NF) kurallarına uygun tasarlanmıştır. Ana tablolar şunlardır:
* `Kullanicilar` (Ortak profil verileri)
* `Doktorlar` & `Hastalar` (İlişkisel tablolar)
* `KanSekeriOlcumleri`
* `InsulinOnerileri`
* `UyariKayitlari`
* `DiyetTurleri` / `EgzersizTurleri` / `BelirtiTurleri`

## 💻 Kurulum ve Çalıştırma

Projeyi yerel makinenizde çalıştırmak için aşağıdaki adımları izleyin.

### 1. Veritabanını Oluşturun
Proje klasöründe bulunan veya aşağıda verilen SQL kodlarını MySQL Workbench veya phpMyAdmin üzerinden çalıştırarak veritabanını oluşturun.

* Veritabanı Adı: `Diyabet_Takip`
* Karakter Seti: `utf8mb4`

### 2. Bağlantı Ayarları
C# projesini açın ve veritabanı bağlantı fonksiyonunun olduğu kısımdaki (genellikle `Database` sınıfı veya `App.config` dosyasında) bağlantı cümlesini (connection string) kendi yerel ayarlarınıza göre güncelleyin:

```csharp
Server=localhost; Database=Diyabet_Takip; Uid=root; Pwd=sifreniz;
```
3. Projeyi Başlatın
Visual Studio üzerinden projeyi derleyin ve çalıştırın.

İlk girişte veritabanında kayıtlı bir doktor veya hasta kullanıcı bilgisi ile giriş yapmanız gerekebilir. (SQL dosyasında örnek veri yoksa manuel ekleme yapınız).
