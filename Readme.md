# 🏋️ FitHub — Spor Salonu Yönetim Sistemi

Kapsamlı bir **Spor Salonu Yönetim ve Randevu Sistemi**. ASP.NET Core MVC mimarisi kullanılarak, Entity Framework Core ile veritabanı yönetimi ve Identity kütüphanesi ile kullanıcı yetkilendirmesi sağlanmıştır.

## 🚀 Özellikler

### 👤 Üye Paneli
- **Kayıt & Giriş:** Güvenli üyelik sistemi.
- **Randevu Alma:** Eğitmenlerin müsaitlik durumuna göre dinamik randevu oluşturma.
- **Randevu Geçmişi:** Bekleyen, onaylanan ve geçmiş randevuları görüntüleme ve iptal etme.
- **Akıllı Asistan (AI):** Google Gemini AI ile kişiselleştirilmiş egzersiz/diyet programı oluşturma.
- **Eğitmen & Hizmet İnceleme:** Detaylı eğitmen profilleri ve hizmet açıklamaları.

### 🛠 Yönetici (Admin) Paneli
- **Dashboard:** Genel istatistikler ve hızlı erişim.
- **Salon & Hizmet Yönetimi:** Salon bilgileri ve hizmet kategorilerinin CRUD işlemleri.
- **Eğitmen Yönetimi:** Eğitmen ekleme, fotoğraf yükleme ve uzmanlık alanı atama.
- **Çalışma Saatleri:** Eğitmenler için haftalık çalışma programı ve çakışma kontrolü.
- **Randevu Onayı:** Üyelerden gelen randevu taleplerini onaylama veya reddetme.

### 🔌 Teknik Özellikler
- **Mimari:** ASP.NET Core MVC (.NET 9.0)
- **Veritabanı:** MS SQL Server (Entity Framework Core Code-First)
- **Yetkilendirme:** ASP.NET Core Identity (Role-Based: Admin, Member)
- **API:** RESTful API endpoints (Swagger UI ile dokümante edilmiş)
- **AI Entegrasyonu:** Google Gemini API (opsiyonel — API key olmadan mock modda çalışır)
- **Güvenlik:** CSRF koruması, dosya yükleme validasyonu, XSS sanitizasyonu, hesap kilitleme
- **Localization:** Türkçe (tr-TR) kültür desteği

---

## ⚙️ Kurulum ve Çalıştırma

### Gereksinimler
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- SQL Server (LocalDB veya Full) — Visual Studio ile birlikte gelir

### 1. Projeyi Klonlayın
```bash
git clone https://github.com/KULLANICI_ADI/FitHub.git
cd FitHub
```

### 2. Veritabanını Oluşturun
```bash
cd FitnessApp.Web
dotnet ef database update
```
> Bu komut veritabanını otomatik oluşturur ve başlangıç verilerini (admin kullanıcısı, eğitmenler, hizmetler vb.) ekler.

### 3. (Opsiyonel) Gemini AI API Anahtarı Tanımlama

Yapay Zeka Asistanı özelliğinin **gerçek AI** ile çalışması için Google Gemini API anahtarı gereklidir.  
API anahtarı olmadan uygulama **mock (demo) modda** çalışır, hata vermez.

API anahtarınız varsa aşağıdaki komutu çalıştırın:

```bash
cd FitnessApp.Web
dotnet user-secrets set "Gemini:ApiKey" "BURAYA_API_ANAHTARINIZI_YAZIN"
```

> 💡 **User Secrets nedir?** API anahtarınız bilgisayarınızda güvenli bir yerde saklanır, Git'e **asla** commit edilmez.  
> API anahtarı almak için: [Google AI Studio](https://aistudio.google.com/apikey)

### 4. Projeyi Başlatın
```bash
dotnet run
```
Tarayıcınızda gösterilen adrese gidin (genellikle `https://localhost:PORT`).

> 📘 Swagger API dokümantasyonu: `/swagger` adresinde mevcuttur (sadece Development modunda).

---

## 🔑 Giriş Bilgileri

| Rol | Email | Şifre |
|-----|-------|-------|
| **Admin** | `admin@saufitness.com` | `Admin1234` |
| **Üye** | Kayıt sayfasından yeni hesap oluşturabilirsiniz | — |

---

## 📚 API Endpoint'leri

| Metot | Endpoint | Açıklama | Yetki |
|-------|----------|----------|-------|
| `GET` | `/api/Trainers` | Tüm eğitmen listesi | Auth gerekli |
| `GET` | `/api/Trainers/filter?date=YYYY-MM-DD` | Tarihe göre müsait eğitmenler | Auth gerekli |
| `GET` | `/api/Appointments/my-history` | Üyenin randevu geçmişi | Auth gerekli |

---

## 🤖 Akıllı Asistan (AI)

Üyeler, **Hesabım → Yapay Zeka Asistanı** menüsünden yaş, kilo, boy ve hedeflerini girerek kişiselleştirilmiş beslenme ve antrenman planı alabilirler.

- **Gemini API anahtarı tanımlıysa:** Google Gemini AI ile gerçek zamanlı plan oluşturulur.
- **API anahtarı yoksa:** Uygulama otomatik olarak demo/mock modda çalışır, hazır şablonlardan plan üretir.

---

## 📁 Proje Yapısı

```
FitnessApp.Web/
├── Areas/Admin/          # Admin paneli (Controller + View)
├── Controllers/          # Ana controller'lar
│   └── Api/              # RESTful API controller'ları
├── Data/                 # Entity model'ler, DbContext, Seeder
├── Migrations/           # EF Core migration dosyaları
├── Services/             # İş mantığı servisleri (AI, Randevu)
├── ViewModels/           # Form ve görünüm modelleri
├── Views/                # Razor View dosyaları
└── wwwroot/              # Statik dosyalar (CSS, JS, images)
```