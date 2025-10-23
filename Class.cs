using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDBProject
{
    
        public class Kullanici
        {
            public string TcKimlikNo { get; set; }
            public string SifreHash { get; set; }
            public string Ad { get; set; }
            public string Soyad { get; set; }
            public string Eposta { get; set; }
            public DateTime DogumTarihi { get; set; }
            public string Cinsiyet { get; set; } // ENUM (Erkek, Kadın)
            public string KullaniciTipi { get; set; } // ENUM (Doktor, Hasta)
            public byte[] ProfilResim { get; set; } // LONGBLOB
        }

        public class Doktorlar
        {
            public string doktor_id { get; set; }

        }

        public class Hastalar
        {
            public string hasta_id { get; set; }
            public string doktor_id { get; set; }

        }

        public class DiyetTurleri
        {
            public int diyet_tur_id { get; set; }
            public string Ad { get; set; }
            public string aciklama { get; set; }
        }

        public class EgzersizTurleri
        {
            public int egzersiz_tur_id { get; set; }
            public string egzersiz_adi { get; set; }
            public string aciklama { get; set; }

        }

        public class BelirtiTurleri
        {
            public int belirti_tur_id { get; set; }
            public string belirti_adi { get; set; }
            public string aciklama { get; set; }

        }

        public class DoktorDiyetOnerileri
        {
            public int oneri_id { get; set; }
            public string doktor_id { get; set; }
            public string hasta_id { get; set; }
            public int diyet_tur_id { get; set; }
            public DateTime oneri_tarihi { get; set; }
            public string ek_notlar { get; set; }

        }
        public class DoktorEgzersizOnerileri
        {
            public int oneri_id { get; set; }
            public string doktor_id { get; set; }
            public string hasta_id { get; set; }
            public int egzersiz_tur_id { get; set; }
            public DateTime oneri_tarihi { get; set; }
            public string ek_notlar { get; set; }

        }

        public class HastaDiyetKayitlari
        {
            public int kayit_id { get; set; }
            public string hasta_id { get; set; }
            public int doktor_diyet_oneri_id { get; set; }
            public DateTime kayit_tarihi { get; set; }
            public string uygulama_durumu { get; set; }

        }

        public class HastaEgzersizKayitlari
        {
            public int kayit_id { get; set; }
            public string hasta_id { get; set; }
            public int doktor_egzersiz_oneri_id { get; set; }
            public DateTime kayit_tarihi { get; set; }
            public string uygulama_durumu { get; set; }

        }

        public class HastaBelirtiKayitlari
        {
            public int kayit_id { get; set; }
            public string hasta_id { get; set; }
            public int belirti_tur_id { get; set; }
            public DateTime kayit_tarihi { get; set; }

        }

        public class KanSekeriOlcumleri
        {
            public int olcum_id { get; set; }
            public string hasta_id { get; set; }
            public int olcum_degeri_mg_dl { get; set; }
            public DateTime olcum_tarihi { get; set; }
            public string olcum_tipi { get; set; }
            public bool ortalama_dahil_mi { get; set; }
            public string notlar { get; set; }
        }

        public class InsulinOnerileri
        {
            public int insulin_oneri_id { get; set; }
            public string hasta_id { get; set; }
            public string olcum_tipi_referans { get; set; }
            public double ortalama_kan_sekeri_mg_dl { get; set; }
            public double onerilen_doz_ml { get; set; }
            public DateTime oneri_tarihi { get; set; }
            public string hesaplama_notu { get; set; }

        }

        public class UyariKayitlari
        {
            public int uyari_id { get; set; }
            public string doktor_id { get; set; }
            public string hasta_id { get; set; }
            public int ilgili_olcum_id { get; set; }
            public string uyari_tipi { get; set; }
            public string mesaj { get; set; }
            public DateTime olusturulma_tarihi { get; set; }
        }


        public class Oturum
        {
            public string GirisYapanTc {  get; set; }
            public string KullaniciTipi { get; set; }

        }

        public class İlkKanSekeri
        {
            public int kan_id { get; set; }
            public string hasta_id { get; set; }
            public int ilk_kan_sekeri { get; set; }
            public DateTime olusturulma_tarihi { get; set; }

        }

        public class OlcumSaatAraligi
        {
            public string Tip { get; set; }
            public int BaslangicSaat { get; set; }
            public int BitisSaat { get; set; }
        }

}
