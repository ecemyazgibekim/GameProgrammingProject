# GAME PROGRAMMING PROJECT
**Oyun Programlama dersi Final Projesi için hazırladığım 3D match mekaniğine dayalı bir mobil oyun.**

## 🎮 Links:
- **Playable Link:** [Oyna](https://redback06.itch.io/final-project)  
- **Gameplay Video:** [İzle](https://youtube.com/shorts/p3M9KnK6rSM)  

---

## 🚀 PROJE YAPIM AŞAMALARI

### 🔧 TEMEL OYUN MEKANİKLERİNİN OLUŞTURULMASI
- Obje spawn sistemi geliştirildi.  
- Spawn Manager sınıfı ile belirli alanlarda nesne üretimi sağlandı.  
- Objeleri hareket ettirebilmek için bir Drag sistemi geliştirildi.  
- Spawn olacak ve hareket edebilecek objelerin fizik etkileşimleri, boyutları, material gibi özellikleri ayarlandı, uygun kodlar atandı ve her biri prefab haline getirildi.  

### 🔄 NESNE EŞLEŞME MEKANİĞİ
- Aynı türden iki nesnenin eşleşebilmesi için kontrol eklendi.  
- Eşleşme durumunda puan artışı sağlandı ve UI’a yazdırıldı.  

### 🎨 ANİMASYON VE EFEKTLERİN EKLENMESİ
- Eşleşen nesneler için iç içe geçme animasyonu eklendi.  
- Başarılı eşleşme anında Ses (SFX) ve görsel efekt (VFX) entegre edildi.  

### 🏆 SKOR VE OYUN SÜRESİ
- Skor sistemi oluşturuldu.  
- Skor gösterimi için **TextMeshPro** kullanıldı.  

### ⚡ YETKİNLİK (SKILL) SİSTEMLERİ
- **Çift Puan (Double Points):** 5 saniye boyunca puan kazanımları iki katına çıkarıldı.  
- **Yavaşlatma (Slow Motion):** 5 saniye boyunca oyun hızı düşürüldü.  
- **Nesneleri Kaldırma (Lift Objects):** Nesneler havaya zıplatılarak oyuncunun kolay eşleştirme yapması sağlandı.  
  (_Her buton 5 saniye bekleme süresine sahiptir_)  

### 🔄 NESNE YÖNETİMİ VE YENİDEN SPAWN
- Nesneler yok olduğunda otomatik yeniden spawn işlemi sağlandı.  
- Oyun sonsuz döngüde devam edecek şekilde tasarlandı.  

### 🖱️ BUTON VE UI YÖNETİMİ
- Butonların aktif/pasif durumu ve geri sayımı düzenlendi.  
- UI için **TextMeshPro** ve kullanılan asset paketi entegre edildi.  

---

## 🕹️ NASIL OYNANIR?

### 🎯 OYUN AMACI
- Aynı türden iki nesneyi eşleştirerek puan kazanın.  
- Skill’leri kullanarak kendinize avantaj yaratın ve kazandığınız puanları arttırın.  
- Oyunun bu sonsuz modunda en yüksek puanı yapmaya çalışın.  

### 🔍 OYNANIŞ
1. **Nesne Taşıma:** Ekrandaki nesneleri sürükleyip silindirin üstündeki boş alanlara bırakın.  
2. **Eşleştirme:** Aynı türden iki nesneyi eşleştirdiğinizde puan kazanırsınız.  
3. **Yeni Nesne Üretimi:** Tüm nesneler eşleştiğinde otomatik olarak yeni nesneler spawn olur.  

### ⚡ YETKİNLİKLER (SKILL'LER)
- **Çift Puan (Double Points):** 5 saniye boyunca eşleşmelerden 2 kat puan kazanın.  
- **Yavaşlatma (Slow Motion):** 5 saniye boyunca oyun hızı yavaşlar, daha rahat eşleştirme yapın.  
- **Nesneleri Kaldırma (Lift Objects):** Sahnedeki tüm nesneler havaya zıplar ve rastgele düşer.  

### 🏆 İPUÇLARI
- Yetkinlikleri doğru zamanlarda kullanın. Özellikle skor artırma ve eşleştirme fırsatlarını iyi değerlendirin.  
- Nesneleri hızlıca eşleştirin. Zaman dolmadan maksimum puanı toplamaya çalışın.  
_Bol şans! 🎯_
---

## 👩‍💻 GELİŞTİRİCİ
**Ecem Yazgı BEKİM**  
_210601705_  
