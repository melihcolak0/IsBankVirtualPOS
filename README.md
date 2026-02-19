\# 💳 IsBank Virtual POS – 3D Secure Payment Simulation



Bu proje, \*\*Türkiye İş Bankası Sanal POS entegrasyon mantığını simüle eden\*\*, modern mimari ile geliştirilmiş uçtan uca bir ödeme sistemidir.



Proje amacı:



\* Gerçek banka ödeme akışını anlamak

\* 3D Secure mantığını modellemek

\* Payment → Attempt → OTP → Callback sürecini uygulamak

\* Clean Architecture + CQRS + MediatR yapısını gerçek senaryoda kullanmak



---



\## 🚀 Proje Özellikleri



✅ Sipariş oluşturma

✅ Ödeme oluşturma

✅ Non-3D ödeme akışı

✅ 3D Secure ödeme akışı

✅ OTP (Email doğrulama) sistemi

✅ SMTP ile tek kullanımlık doğrulama kodu

✅ Payment Attempt takibi

✅ Refund işlemi

✅ JWT Authentication

✅ CORS yönetimi

✅ Clean Architecture



---



\## 🏗️ Mimari



Proje \*\*Clean Architecture\*\* prensiplerine göre geliştirilmiştir.



```

WebUI (MVC)

&nbsp;  ↓

API Layer

&nbsp;  ↓

Application (CQRS / MediatR)

&nbsp;  ↓

Domain

&nbsp;  ↓

Persistence (EF Core)

```



---



\## 🔄 Ödeme Akışı



\### 1️⃣ Tutar Girişi



Kullanıcı ödeme sayfasında tutar girer veya hazır fiyatlardan birini seçer.



\### 2️⃣ Order Oluşturma



```

Order → Payment oluşturulur

```



\### 3️⃣ Kart Bilgisi Girişi



Kullanıcı kart bilgilerini girer.



\### 4️⃣ 3D Secure Başlatma



Sistem:



\* PaymentAttempt oluşturur

\* OTP üretir

\* Kullanıcının email adresine gönderir



\### 5️⃣ OTP Doğrulama



Kullanıcı mailine gelen kodu girer.



✅ Doğruysa → Banka formu submit edilir

❌ Yanlışsa → İşlem durdurulur



\### 6️⃣ Banka Callback



Simüle edilen banka sonucu API'ye döner.



\### 7️⃣ Payment Sonucu



Payment status güncellenir ve sonuç ekranı gösterilir.



---



\## 🧪 Test Kart Bilgileri



Demo ödeme için aşağıdaki bilgiler kullanılabilir:



```

Kart Sahibi: Test User

Kart No:     4444 4444 4444 4444

Ay:          12

Yıl:         30

CVV:         123

```



---



\## 📧 OTP Doğrulama



3D ödeme sırasında:



\* 6 haneli OTP üretilir

\* SMTP üzerinden kullanıcı emailine gönderilir

\* Kod 5 dakika geçerlidir



---



\## ⚙️ Kurulum



\### 1️⃣ Repository clone



```bash

git clone https://github.com/USERNAME/IsBankVirtualPOS.git

```



---



\### 2️⃣ Veritabanı Oluşturma



```bash

Update-Database

```



veya



```bash

dotnet ef database update

```



---



\### 3️⃣ appsettings.json Oluşturma



Güvenlik nedeniyle gerçek config dosyaları repoya eklenmemiştir.



Aşağıdaki örneği kullanarak kendi ayarlarınızı oluşturun:



```json

{

&nbsp; "ConnectionStrings": {

&nbsp;   "DefaultConnection": "Server=.;Database=IsBankVirtualPOS;Trusted\_Connection=True;"

&nbsp; },

&nbsp; "Jwt": {

&nbsp;   "Key": "YOUR\_SECRET\_KEY"

&nbsp; },

&nbsp; "MailSettings": {

&nbsp;   "Host": "smtp.gmail.com",

&nbsp;   "Port": 587,

&nbsp;   "Email": "example@gmail.com",

&nbsp;   "Password": "APP\_PASSWORD"

&nbsp; }

}

```



---



\### 4️⃣ Projeyi Çalıştırma



Önce API:



```

IsBankVirtualPOS.API

```



Sonra:



```

IsBankVirtualPOS.WebUI

```



---



\## 🔐 CORS Ayarı



WebUI ve API farklı portlarda çalıştığı için API projesinde CORS aktif edilmelidir.



```

https://localhost:7149 → WebUI

https://localhost:7290 → API

```



---



\## 💰 Refund



Başarılı ödeme sonrası sonuç ekranından refund işlemi yapılabilir.



---



\## 🧱 Kullanılan Teknolojiler



\* ASP.NET Core

\* Entity Framework Core

\* MediatR

\* Clean Architecture

\* MailKit (SMTP)

\* JWT Authentication

\* MVC

\* JavaScript Fetch API



---



\## 📸 Ekran Görüntüleri



> Aşağıya proje ekran görüntüleri eklenebilir.



\### Checkout



<!-- screenshot -->



\### Card Payment



<!-- screenshot -->



\### OTP Verification



<!-- screenshot -->



\### Payment Result



<!-- screenshot -->



---



\## 👨‍💻 Amaç



Bu proje gerçek banka entegrasyonuna hazırlık amacıyla geliştirilmiş bir \*\*öğrenme ve mimari demonstrasyon\*\* projesidir.



Gerçek finansal işlem içermez.



---



\## ⭐ Katkı



Pull request'ler ve geliştirme önerileri memnuniyetle karşılanır.



