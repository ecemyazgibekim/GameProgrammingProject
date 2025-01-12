# GAME PROGRAMMING PROJECT
**Oyun Programlama dersi Final Projesi için hazırladığım 3D match mekaniğine dayalı bir mobil oyun.**

## 🎮 Links:
- **Playable Link:** [Oyna](https://redback06.itch.io/final-project)  
- **Gameplay Video:** [İzle](https://youtube.com/shorts/p3M9KnK6rSM)  

---

## 🚀 PROJE YAPIM AŞAMALARI <br/><br/>
### 🔧 TEMEL OYUN MEKANİKLERİNİN OLUŞTURULMASI
- Obje spawn sistemi geliştirildi.  
- Spawn Manager sınıfı ile belirli alanlarda nesne üretimi sağlandı.  
- Objeleri hareket ettirebilmek için bir Drag sistemi geliştirildi.  
- Spawn olacak ve hareket edebilecek objelerin fizik etkileşimleri, boyutları, material gibi özellikleri ayarlandı, uygun kodlar atandı ve her biri prefab haline getirildi.  <br/>
### 🔄 NESNE EŞLEŞME MEKANİĞİ
- Aynı türden iki nesnenin eşleşebilmesi için kontrol eklendi.  
- Eşleşme durumunda puan artışı sağlandı ve UI’a yazdırıldı.  <br/>
### 🎨 ANİMASYON VE EFEKTLERİN EKLENMESİ
- Eşleşen nesneler için iç içe geçme animasyonu eklendi.  
- Başarılı eşleşme anında Ses (SFX) ve görsel efekt (VFX) entegre edildi.  <br/>
### 🏆 SKOR VE OYUN SÜRESİ
- Skor sistemi oluşturuldu.  
- Skor gösterimi için **TextMeshPro** kullanıldı.  <br/>
### ⚡ YETKİNLİK (SKILL) SİSTEMLERİ
- **Çift Puan (Double Points):** 5 saniye boyunca puan kazanımları iki katına çıkarıldı.  
- **Yavaşlatma (Slow Motion):** 5 saniye boyunca oyun hızı düşürüldü.  
- **Nesneleri Kaldırma (Lift Objects):** Nesneler havaya zıplatılarak oyuncunun kolay eşleştirme yapması sağlandı.  
  (_Her buton 5 saniye bekleme süresine sahiptir_)  <br/>
### 🔄 NESNE YÖNETİMİ VE YENİDEN SPAWN
- Nesneler yok olduğunda otomatik yeniden spawn işlemi sağlandı.  
- Oyun sonsuz döngüde devam edecek şekilde tasarlandı.  <br/>
### 🖱️ BUTON VE UI YÖNETİMİ
- Butonların aktif/pasif durumu ve geri sayımı düzenlendi.  
- UI için **TextMeshPro** ve kullanılan asset paketi entegre edildi.  <br/>

---

## 🕹️ NASIL OYNANIR? <br/><br/>

### 🎯 OYUN AMACI
- Aynı türden iki nesneyi eşleştirerek puan kazanın.  
- Skill’leri kullanarak kendinize avantaj yaratın ve kazandığınız puanları arttırın.  
- Oyunun bu sonsuz modunda en yüksek puanı yapmaya çalışın.  <br/><br/>
### 🔍 OYNANIŞ
1. **Nesne Taşıma:** Ekrandaki nesneleri sürükleyip silindirin üstündeki boş alanlara bırakın.  
2. **Eşleştirme:** Aynı türden iki nesneyi eşleştirdiğinizde puan kazanırsınız.  
3. **Yeni Nesne Üretimi:** Tüm nesneler eşleştiğinde otomatik olarak yeni nesneler spawn olur.  <br/><br/>
### ⚡ YETKİNLİKLER (SKILL'LER)
- **Çift Puan (Double Points):** 5 saniye boyunca eşleşmelerden 2 kat puan kazanın.  
- **Yavaşlatma (Slow Motion):** 5 saniye boyunca oyun hızı yavaşlar, daha rahat eşleştirme yapın.  
- **Nesneleri Kaldırma (Lift Objects):** Sahnedeki tüm nesneler havaya zıplar ve rastgele düşer.  <br/><br/>
### 🏆 İPUÇLARI
- Yetkinlikleri doğru zamanlarda kullanın. Özellikle skor artırma ve eşleştirme fırsatlarını iyi değerlendirin.  
- Nesneleri hızlıca eşleştirin. Zaman dolmadan maksimum puanı toplamaya çalışın.  
_Bol şans! 🎯_ <br/><br/>
---


## ⚙️ TEKNİK DETAYLAR <br/><br/>

### 🎮 OYNANIŞ
1. 🛠️ **Unity Sürümü:** Unity 2022.3.31f  
2. 🎨 **Assets:** Hyper Visual FX (🗓️ January 20, 2022) - 2D Casual UI HD (🗓️ Feb 23, 2017) - 🎥 Recorder - ✍️ TextMeshPro  
3. 📦 **Build Type:** WebGL  
4. 📱 **Game Type:** 3D Vertical Mobile
   
##  OYUN İÇİ GÖRSELLER
#### Başlangıç Ekranı:
![Ekran görüntüsü 2025-01-12 205941](https://github.com/user-attachments/assets/7454d224-1930-4c2d-a879-e736f425b11c)

#### İki Nesne Başarıyla Eşleştirildiğinde Tek Nesneye Dönüşerek Kaybolması Animasyonu
https://github.com/user-attachments/assets/eed44df2-1d72-47bb-a608-bb0d107fa0d8

#### Nesneleri Eşleştirdiğinde 5 Saniyeliğine 2 Kat Puan Kazanma Skilli
https://github.com/user-attachments/assets/bc47effe-c6f7-496e-8993-6239221aff41

#### Nesneleri Lift Butonuyla Havaya Kaldırma Skilli
https://github.com/user-attachments/assets/72009aea-8ab2-4c50-9e04-d5177f46c30f

#### Nesneleri Slow Skilli ile Yavaşlatma
https://github.com/user-attachments/assets/cf5a65ad-e6b0-4f6e-8729-5c4430a027f5

---

## 👩‍💻 GELİŞTİRİCİ
**Ecem Yazgı BEKİM**  
_210601705_  
