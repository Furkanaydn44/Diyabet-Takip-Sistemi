using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net;
using System.Net.Mail;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PatientDBProject
{
    public class VeriTabaniIslemleri
    {


        public string connectionString= "Server=localhost;Database=Diyabet_Takip;Uid=root;Pwd=Furkan@1059;";
        public void KullaniciEkle(Kullanici yeniKullanici)
        {
            //string connectionString = "Server=localhost;Database=Diyabet_Takip;Uid=root;Pwd=0000;";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO Kullanicilar 
            (tc_kimlik_no, sifre_hash, ad, soyad, eposta, dogum_tarihi, cinsiyet, kullanici_tipi, profil_resim) 
            VALUES 
            (@tc, @sifre, @ad, @soyad, @eposta, @dogum, @cinsiyet, @tip, @resim)";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@tc", yeniKullanici.TcKimlikNo);
                    cmd.Parameters.AddWithValue("@sifre", yeniKullanici.SifreHash);
                    cmd.Parameters.AddWithValue("@ad", yeniKullanici.Ad);
                    cmd.Parameters.AddWithValue("@soyad", yeniKullanici.Soyad);
                    cmd.Parameters.AddWithValue("@eposta", yeniKullanici.Eposta);
                    cmd.Parameters.AddWithValue("@dogum", yeniKullanici.DogumTarihi);
                    cmd.Parameters.AddWithValue("@cinsiyet", yeniKullanici.Cinsiyet);
                    cmd.Parameters.AddWithValue("@tip", yeniKullanici.KullaniciTipi);
                    cmd.Parameters.AddWithValue("@resim", yeniKullanici.ProfilResim);

                    cmd.ExecuteNonQuery();
                }
                if (yeniKullanici.KullaniciTipi == "Doktor")
                {
                    string insertDoktor = @"INSERT INTO Doktorlar
                (doktor_id)
                VALUES
                (@did)";
                    using (var cmd = new MySqlCommand(insertDoktor, connection))
                    {
                        cmd.Parameters.AddWithValue("@did", yeniKullanici.TcKimlikNo);

                        cmd.ExecuteNonQuery();
                    }
                }
                else if (yeniKullanici.KullaniciTipi == "Hasta")
                {
                    string insertHasta = @"INSERT INTO Hastalar
                (hasta_id)
                VALUES
                (@hid)";
                    using (var cmd = new MySqlCommand(insertHasta, connection))
                    {
                        cmd.Parameters.AddWithValue("@hid", yeniKullanici.TcKimlikNo);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        

        public bool GirisYap(string tcKimlikNo, string girilenSifre, string gtip)
        {
            // string connectionString = "Server=localhost;Database=Diyabet_Takip;Uid=root;Pwd=Furkan@1059;";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT sifre_hash FROM Kullanicilar WHERE tc_kimlik_no = @tc AND kullanici_tipi = @tip";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@tc", tcKimlikNo);
                    cmd.Parameters.AddWithValue("@tip", gtip); 

                    var sonuc = cmd.ExecuteScalar();

                    if (sonuc != null)
                    {
                        string veritabanindakiHash = sonuc.ToString();
                        return veritabanindakiHash == girilenSifre;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public void KayitYap(string hasta_id, double kanSekeri, List<string> belirtiler)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string insertKan = "INSERT INTO İlkKanSekeri (hasta_id,ilk_kan_sekeri,olusturulma_tarihi) VALUES (@hastaId, @kan, @tarih)";
                using (var cmd = new MySqlCommand(insertKan, connection))
                {
                    cmd.Parameters.AddWithValue("@hastaId", hasta_id);
                    cmd.Parameters.AddWithValue("@kan", kanSekeri);
                    cmd.Parameters.AddWithValue("@tarih", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

                int belirti = 0;
                foreach (string belirtiAd in belirtiler)
                {
                    belirti = BelirtiCevirim(belirtiAd);

                    string insertBelirti = "INSERT INTO HastaBelirtiKayitlari (hasta_id,belirti_tur_id,kayit_tarihi) VALUES (@hastaId, @belirtiAd, @tarih)";
                    using (var cmd = new MySqlCommand(insertBelirti, connection))
                    {
                        cmd.Parameters.AddWithValue("@hastaId", hasta_id);
                        cmd.Parameters.AddWithValue("@belirtiAd", belirti);
                        cmd.Parameters.AddWithValue("@tarih", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
                connection.Close();
            }
        }

        public void KullaniciGetir(string tcKimlikNo,ref string isim,ref string soyisim,ref string epos,ref DateTime dogum,ref string cins)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query0 = "SELECT ad FROM kullanicilar WHERE tc_kimlik_no = @tc";
                using (var cmd0 = new MySqlCommand(query0, connection))
                {
                    cmd0.Parameters.AddWithValue("@tc", tcKimlikNo);

                    var sonuc0 = cmd0.ExecuteScalar();
                    if (sonuc0 != null)
                    {
                        isim = sonuc0.ToString();

                    }
                    else
                    {
                        isim = "NULL";
                    }

                }
                string query1 = "SELECT soyad FROM kullanicilar WHERE tc_kimlik_no = @tc";
                using (var cmd1 = new MySqlCommand(query1, connection))
                {
                    cmd1.Parameters.AddWithValue("@tc", tcKimlikNo);

                    var sonuc1 = cmd1.ExecuteScalar();
                    if (sonuc1 != null)
                    {
                        soyisim = sonuc1.ToString();

                    }
                    else
                    {
                        soyisim = "NULL";
                    }

                }
                string query2 = "SELECT eposta FROM kullanicilar WHERE tc_kimlik_no = @tc";
                using (var cmd2 = new MySqlCommand(query2, connection))
                {
                    cmd2.Parameters.AddWithValue("@tc", tcKimlikNo);

                    var sonuc2 = cmd2.ExecuteScalar();
                    if (sonuc2 != null)
                    {
                        epos = sonuc2.ToString();

                    }
                    else
                    {
                       epos = "NULL";
                    }

                }
                string query3 = "SELECT dogum_tarihi FROM kullanicilar WHERE tc_kimlik_no = @tc";
                using (var cmd3 = new MySqlCommand(query3, connection))
                {
                    cmd3.Parameters.AddWithValue("@tc", tcKimlikNo);

                    var sonuc3 = cmd3.ExecuteScalar();
                    if (sonuc3 != null)
                    {
                        dogum =DateTime.Parse(sonuc3.ToString());

                    }
                    else
                    {
                        dogum = DateTime.Parse(null);
                    }

                }
                string query4 = "SELECT cinsiyet FROM kullanicilar WHERE tc_kimlik_no = @tc";
                using (var cmd4 = new MySqlCommand(query4, connection))
                {
                    cmd4.Parameters.AddWithValue("@tc", tcKimlikNo);

                    var sonuc4 = cmd4.ExecuteScalar();
                    if (sonuc4 != null)
                    {
                        cins = sonuc4.ToString();

                    }
                    else
                    {
                        cins = "NULL";
                    }

                }
            }   
        }
        public byte[] Resimgetir(ref string tcKimlikNo )
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT profil_resim FROM kullanicilar WHERE tc_kimlik_no = @tc";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@tc", tcKimlikNo);

                    var sonuc = cmd.ExecuteScalar();
                    if (sonuc != null && sonuc != DBNull.Value)
                    {
                        return (byte[])sonuc;
                        
                    }
                    else
                    {
                        return null;
                    }

                }
            }
        }
        public bool TCKontrol(ref string tcKimlikNo)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM kullanicilar WHERE tc_kimlik_no = @tc";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@tc", tcKimlikNo);

                    var sonuc = cmd.ExecuteScalar();
                    if (sonuc != null)
                    {
                        return true;

                    }
                    else
                    {
                        return false;
                    }

                }
            }
        }

        public void VerileriYukle(
        List<DiyetTurleri> tumDiyetler,
        List<EgzersizTurleri> tumEgzersizler,
        List<BelirtiTurleri> tumBelirtiler)
        {

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();


                var diyetCmd = new MySqlCommand("SELECT * FROM DiyetTurleri", connection);
                using (var reader = diyetCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tumDiyetler.Add(new DiyetTurleri
                        {
                            diyet_tur_id = Convert.ToInt32(reader["diyet_tur_id"]),
                            Ad = reader["Ad"].ToString()
                        });
                    }
                }


                var belirtiCmd = new MySqlCommand("SELECT * FROM BelirtiTurleri", connection);
                using (var reader = belirtiCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tumBelirtiler.Add(new BelirtiTurleri
                        {
                            belirti_tur_id = Convert.ToInt32(reader["belirti_tur_id"]),
                            belirti_adi = reader["belirti_adi"].ToString()
                        });
                    }
                }


                var egzersizCmd = new MySqlCommand("SELECT * FROM EgzersizTurleri", connection);
                using (var reader = egzersizCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tumEgzersizler.Add(new EgzersizTurleri
                        {
                            egzersiz_tur_id = Convert.ToInt32(reader["egzersiz_tur_id"]),
                            egzersiz_adi = reader["egzersiz_adi"].ToString()
                        });
                    }
                }
            }
        }

        public int BelirtiCevirim(string belirti)
        {
            int belirti_id = 0;

            if (belirti == "Poliüri")
                belirti_id = 1;
            if (belirti == "Polifaji")
                belirti_id = 2;
            if (belirti == "Polidipsi")
                belirti_id = 3;
            if (belirti == "Noropati")
                belirti_id = 4;
            if (belirti == "Kilo kaybı")
                belirti_id = 5;
            if (belirti == "Yorgunluk")
                belirti_id = 6;
            if (belirti == "Yaraların Yavaş İyileşmesi")
                belirti_id = 7;
            if (belirti == "Bulanık görme")
                belirti_id = 8;
            return belirti_id;
        }

        int EgzersizCevirim(string egzersiz)
        {
            int egzersiz_id = 0;

            if (egzersiz == "Yürüyüş")
                egzersiz_id = 1;
            if (egzersiz == "Bisiklet")
                egzersiz_id = 2;
            if (egzersiz == "Klinik Egzersiz")
                egzersiz_id = 3;
            return egzersiz_id;

        }

        int DiyetCevirim(string diyet)
        {
            int diyet_id = 0;

            if (diyet == "Az Şekerli Diyet")
                diyet_id = 1;
            if (diyet == "Şekersiz Diyet")
                diyet_id = 2;
            if (diyet == "Dengeli Beslenme")
                diyet_id = 3;
            return diyet_id;

        }

        public void OneriYap(string doktor_id, string hasta_id, double kanSekeri, List<string> girilenBelirtiler, List<DiyetTurleri> tumDiyetler, List<EgzersizTurleri> tumEgzersizler,ref bool error)
        {
            var diyet = "";
            var egzersiz = "";


            if (kanSekeri < 70 && girilenBelirtiler.Contains("Nöropati") && girilenBelirtiler.Contains("Polifaji") && girilenBelirtiler.Contains("Yorgunluk"))
            {
                diyet = tumDiyetler.FirstOrDefault(d => d.Ad.Contains("Dengeli Beslenme"))?.Ad;
                error = false;

                egzersiz = tumEgzersizler.FirstOrDefault(e => e.egzersiz_adi.Contains(null))?.egzersiz_adi;

            }
            else if (kanSekeri > 70 && kanSekeri < 110 && girilenBelirtiler.Contains("Yorgunluk") && girilenBelirtiler.Contains("Kilo Kaybı"))
            {
                diyet = tumDiyetler.FirstOrDefault(d => d.Ad.Contains("Az Şekerli Diyet"))?.Ad;


                egzersiz = tumEgzersizler.FirstOrDefault(e => e.egzersiz_adi.Contains("Yürüyüş"))?.egzersiz_adi;
                error = false;

            }
            else if (kanSekeri > 70 && kanSekeri < 110 && girilenBelirtiler.Contains("Polifaji") && girilenBelirtiler.Contains("Polidipsi"))
            {
                diyet = tumDiyetler.FirstOrDefault(d => d.Ad.Contains("Dengeli Beslenme"))?.Ad;


                egzersiz = tumEgzersizler.FirstOrDefault(e => e.egzersiz_adi.Contains("Yürüyüş"))?.egzersiz_adi;
                error = false;

            }
            else if (kanSekeri > 110 && kanSekeri < 180 && girilenBelirtiler.Contains("Yorgunluk") && girilenBelirtiler.Contains("Nöropati") && girilenBelirtiler.Contains("Bulanık Görme"))
            {
                diyet = tumDiyetler.FirstOrDefault(d => d.Ad.Contains("Az Şekerli Diyet"))?.Ad;


                egzersiz = tumEgzersizler.FirstOrDefault(e => e.egzersiz_adi.Contains("Yürüyüş"))?.egzersiz_adi;
                error = false;

            }
            else if (kanSekeri > 110 && kanSekeri < 180 && girilenBelirtiler.Contains("Bulanık Görme") && girilenBelirtiler.Contains("Nöropati"))
            {
                diyet = tumDiyetler.FirstOrDefault(d => d.Ad.Contains("Az Şekerli Diyet"))?.Ad;


                egzersiz = tumEgzersizler.FirstOrDefault(e => e.egzersiz_adi.Contains("Klinik Egzersiz"))?.egzersiz_adi;
                error = false;

            }
            else if (kanSekeri > 110 && kanSekeri < 180 && girilenBelirtiler.Contains("Poliüri") && girilenBelirtiler.Contains("Polidipsi"))
            {
                diyet = tumDiyetler.FirstOrDefault(d => d.Ad.Contains("Şekersiz Diyet"))?.Ad;

                egzersiz = tumEgzersizler.FirstOrDefault(e => e.egzersiz_adi.Contains("Klinik Egzersiz"))?.egzersiz_adi;
                error = false;

            }
            else if (kanSekeri >= 180 && girilenBelirtiler.Contains("Yaraların Yavaş İyileşmesi") && girilenBelirtiler.Contains("Polifaji") && girilenBelirtiler.Contains("Polidipsi"))
            {
                diyet = tumDiyetler.FirstOrDefault(d => d.Ad.Contains("Şekersiz Diyet"))?.Ad;


                egzersiz = tumEgzersizler.FirstOrDefault(e => e.egzersiz_adi.Contains("Klinik Egzersiz"))?.egzersiz_adi;
                error = false;

            }
            else if (kanSekeri >= 180 && girilenBelirtiler.Contains("Yaraların Yavaş İyileşmesi") && girilenBelirtiler.Contains("Kilo Kaybı"))
            {
                diyet = tumDiyetler.FirstOrDefault(d => d.Ad.Contains("Şekersiz Diyet"))?.Ad;


                egzersiz = tumEgzersizler.FirstOrDefault(e => e.egzersiz_adi.Contains("Yürüyüş"))?.egzersiz_adi;
                error = false;

            }
            else 
            {
                MessageBox.Show("Veritabanına kayıt yapılamadı.\nHasta koşulları sağlamıyor.","Kayıt yapılamadı",MessageBoxButtons.OK,MessageBoxIcon.Error);
                error = true;
            }
                

            int diyet_id = DiyetCevirim(diyet);
            int egzersiz_id = EgzersizCevirim(egzersiz);
            
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();


                    if (!string.IsNullOrEmpty(diyet))
                    {
                        try
                        {

                            string insertDiyet = "INSERT INTO DoktorDiyetOnerileri (doktor_id,hasta_id,diyet_tur_id,oneri_tarihi) VALUES (@doktorId,@hastaId, @diyetAd, @tarih)";
                            using (var cmd0 = new MySqlCommand(insertDiyet, connection))
                            {
                                cmd0.Parameters.AddWithValue("@doktorId", doktor_id);
                                cmd0.Parameters.AddWithValue("@hastaId", hasta_id);
                                cmd0.Parameters.AddWithValue("@diyetAd", diyet_id);
                                cmd0.Parameters.AddWithValue("@tarih", DateTime.Now);
                                cmd0.ExecuteNonQuery();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("1");
                        }
                        try 
                        { 
                            string insertDiyet2 = "INSERT INTO HastaDiyetKayitlari (hasta_id,doktor_diyet_oneri_id,kayit_tarihi) VALUES (@hastaId , @diyetAd, @tarih)";
                            using (var cmd1 = new MySqlCommand(insertDiyet2, connection))
                            {
                                cmd1.Parameters.AddWithValue("@hastaId", hasta_id);
                                cmd1.Parameters.AddWithValue("@diyetAd", diyet_id);
                                cmd1.Parameters.AddWithValue("@tarih", DateTime.Now);
                                cmd1.ExecuteNonQuery();
                            }
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("2");
                        }
                    }


                    if (!string.IsNullOrEmpty(egzersiz))
                    {

                        try
                        {
                            string insertEgzersiz = "INSERT INTO DoktorEgzersizOnerileri (doktor_id,hasta_id,egzersiz_tur_id,oneri_tarihi) VALUES (@doktorId,@hastaId, @egzersizAd, @tarih)";
                            using (var cmd2 = new MySqlCommand(insertEgzersiz, connection))
                            {
                                cmd2.Parameters.AddWithValue("@doktorId", doktor_id);
                                cmd2.Parameters.AddWithValue("@hastaId", hasta_id);
                                cmd2.Parameters.AddWithValue("@egzersizAd", egzersiz_id);
                                cmd2.Parameters.AddWithValue("@tarih", DateTime.Now);
                                cmd2.ExecuteNonQuery();
                            }
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("3");
                        }

                        try
                        {
                            string insertEgzersiz2 = "INSERT INTO HastaEgzersizKayitlari(hasta_id,doktor_egzersiz_oneri_id,kayit_tarihi) VALUES (@hastaId, @egzersizAd, @tarih)";
                            using (var cmd = new MySqlCommand(insertEgzersiz2, connection))
                            {
                                cmd.Parameters.AddWithValue("@hastaId", hasta_id);
                                cmd.Parameters.AddWithValue("@egzersizAd", egzersiz_id);
                                cmd.Parameters.AddWithValue("@tarih", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("4");
                        }
                    }
                    connection.Close();
                }
        }
            
        

        public bool HastaTCKontrol( string doktortc, string hastatc)
        {


            using(var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM kullanicilar JOIN hastalar ON tc_kimlik_no = hasta_id WHERE tc_kimlik_no = @tc AND doktor_id=@doktorId";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@tc", hastatc);
                    cmd.Parameters.AddWithValue("@doktorId", doktortc);

                    var sonuc = cmd.ExecuteScalar();
                    if (sonuc != null)
                    {
                        return true;

                    }
                    else
                    {
                        return false;
                    }

                }
            }
        }

        public void DoktorTCKontrol(ref string doktortc, string hastatc)
        {


            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT doktor_id FROM kullanicilar JOIN hastalar ON tc_kimlik_no = hasta_id WHERE tc_kimlik_no = @tc";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@tc", hastatc);


                    var sonuc = cmd.ExecuteScalar();
                    if (sonuc != null)
                    {
                        doktortc = sonuc.ToString();

                    }
                    else
                    {
                        
                    }

                }
            }
        }

        public bool HastaDiyetKontrol(string hastatc)
        {


            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM HastaDiyetKayitlari WHERE hasta_id = @tc ";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@tc", hastatc);

                    var sonuc = cmd.ExecuteScalar();
                    if (sonuc != null)
                    {
                        return true;

                    }
                    else
                    {
                        return false;
                    }

                }
            }
        }

        public void Uygulandimi(string hastaId, bool diyet, bool egzersiz)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                if (diyet)
                {
                    string updateDiyet = @"
             UPDATE HastaDiyetKayitlari 
             SET uygulama_durumu = @durum 
             WHERE hasta_id = @hastaId AND uygulama_durumu IS NULL 
             ORDER BY kayit_id DESC 
             LIMIT 1";

                    using (var cmd = new MySqlCommand(updateDiyet, connection))
                    {
                        cmd.Parameters.AddWithValue("@durum", "uygulandı");
                        cmd.Parameters.AddWithValue("@hastaId", hastaId);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    string updateDiyet = @"
             UPDATE HastaDiyetKayitlari 
             SET uygulama_durumu = @durum 
             WHERE hasta_id = @hastaId AND uygulama_durumu IS NULL 
             ORDER BY kayit_id DESC 
             LIMIT 1";

                    using (var cmd = new MySqlCommand(updateDiyet, connection))
                    {
                        cmd.Parameters.AddWithValue("@durum", "uygulanmadı");
                        cmd.Parameters.AddWithValue("@hastaId", hastaId);
                        cmd.ExecuteNonQuery();
                    }
                }

                if (egzersiz)
                {
                    string updateEgzersiz = @"
             UPDATE HastaEgzersizKayitlari 
             SET uygulama_durumu = @durum 
             WHERE hasta_id = @hastaId AND uygulama_durumu IS NULL 
             ORDER BY kayit_id DESC 
             LIMIT 1";

                    using (var cmd = new MySqlCommand(updateEgzersiz, connection))
                    {
                        cmd.Parameters.AddWithValue("@durum", "uygulandı");
                        cmd.Parameters.AddWithValue("@hastaId", hastaId);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    string updateEgzersiz = @"
             UPDATE HastaEgzersizKayitlari 
             SET uygulama_durumu = @durum 
             WHERE hasta_id = @hastaId AND uygulama_durumu IS NULL 
             ORDER BY kayit_id DESC 
             LIMIT 1";

                    using (var cmd = new MySqlCommand(updateEgzersiz, connection))
                    {
                        cmd.Parameters.AddWithValue("@durum", "uygulanmadı");
                        cmd.Parameters.AddWithValue("@hastaId", hastaId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void HastaEgzersizUygulamaYuzdesi(string hastaId,ref int yuzde_e)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"
         SELECT 
             COUNT(*) AS toplam_kayit,
             SUM(CASE WHEN uygulama_durumu = 'uygulandı' THEN 1 ELSE 0 END) AS uygulanan
         FROM HastaEgzersizKayitlari
         WHERE hasta_id = @hastaId;
     ";

                using (var cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@hastaId", hastaId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            double yuzde = 0;
                            int toplam = Convert.ToInt32(reader["toplam_kayit"]);
                            int a = reader.IsDBNull(reader.GetOrdinal("uygulanan")) ? 0 : reader.GetInt32(reader.GetOrdinal("uygulanan"));
                            if (a != null)
                            {
                                int uygulanan = a;
                                yuzde = toplam > 0 ? (uygulanan * 100.0 / toplam) : 0;
                            }
                            yuzde_e = Convert.ToInt32((double)yuzde);
                        }
                        else
                        {
                            yuzde_e = 0;
                        }
                    }
                }

                connection.Close();
            }
        }

        public void HastaDiyetUygulamaYuzdesi(string hastaId,ref int yuzde_d)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"
         SELECT 
             COUNT(*) AS toplam_kayit,
             SUM(CASE WHEN uygulama_durumu = 'uygulandı' THEN 1 ELSE 0 END) AS uygulanan
         FROM HastaDiyetKayitlari
         WHERE hasta_id = @hastaId;
     ";

                using (var cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@hastaId", hastaId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            double yuzde = 0;
                            int toplam = Convert.ToInt32(reader["toplam_kayit"]);
                            int a = reader.IsDBNull(reader.GetOrdinal("uygulanan")) ? 0 : reader.GetInt32(reader.GetOrdinal("uygulanan"));
                            if (a != null)
                            { 
                                int uygulanan = a;
                                yuzde = toplam > 0 ? (uygulanan * 100.0 / toplam) : 0;
                            }
                            
                            yuzde_d = Convert.ToInt32((double)yuzde);
                        }
                        else
                        {
                            yuzde_d = 0;
                        }
                    }
                }

                connection.Close();
            }
        }

        public void HastaKayitlariniGoster(string hastaId)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                Console.WriteLine("---- Diyet Kayıtları ----");
                string queryDiyet = @"
        SELECT d.diyet_tur_id, hdk.kayit_tarihi, hdk.uygulama_durumu
        FROM HastaDiyetKayitlari hdk
        JOIN DoktorDiyetOnerileri d ON hdk.doktor_diyet_oneri_id = d.oneri_id
        WHERE hdk.hasta_id = @hastaId
        ORDER BY hdk.kayit_tarihi DESC";

                using (var cmd = new MySqlCommand(queryDiyet, connection))
                {
                    cmd.Parameters.AddWithValue("@hastaId", hastaId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Diyet: {reader["diyet_tur_id"]} | Tarih: {reader["kayit_tarihi"]} | Uygulama: {reader["uygulama_durumu"]}");
                        }
                    }
                }

                Console.WriteLine();

                Console.WriteLine("---- Egzersiz Kayıtları ----");
                string queryEgzersiz = @"
        SELECT e.egzersiz_tur_id, hek.kayit_tarihi, hek.uygulama_durumu
        FROM HastaEgzersizKayitlari hek
        JOIN DoktorEgzersizOnerileri e ON hek.doktor_egzersiz_oneri_id = e.oneri_id
        WHERE hek.hasta_id = @hastaId
        ORDER BY hek.kayit_tarihi DESC";

                using (var cmd = new MySqlCommand(queryEgzersiz, connection))
                {
                    cmd.Parameters.AddWithValue("@hastaId", hastaId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Egzersiz: {reader["egzersiz_tur_id"]} | Tarih: {reader["kayit_tarihi"]} | Uygulama: {reader["uygulama_durumu"]}");
                        }
                    }
                }

                Console.WriteLine();

                Console.WriteLine("---- Belirti Kayıtları ----");
                string queryBelirti = @"
        SELECT b.belirti_adi, hbk.kayit_tarihi
        FROM HastaBelirtiKayitlari hbk
        JOIN BelirtiTurleri b ON hbk.belirti_tur_id = b.belirti_adi
        WHERE hbk.hasta_id = @hastaId
        ORDER BY hbk.kayit_tarihi DESC";

                using (var cmd = new MySqlCommand(queryBelirti, connection))
                {
                    cmd.Parameters.AddWithValue("@hastaId", hastaId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Belirti: {reader["belirti_adi"]} | Tarih: {reader["kayit_tarihi"]}");
                        }
                    }
                }
            }
        }

        public List<String> HastalariGetir(string drtc)
        {

            List<String> liste = new List<String>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Hastalar WHERE doktor_id = @tc";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@tc", drtc);
                    using (var reader = cmd.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {
                            string tcno = reader.GetString("hasta_id");

                            liste.Add(tcno);
                        }
                    }
                }
            }

            return liste;
        }


        public List<Kullanici> KullanicilariGetir()
        {
            //string connectionString = "Server=localhost;Database=Diyabet_Takip;Uid=root;Pwd=0000;";
            List<Kullanici> liste = new List<Kullanici>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Kullanicilar";

                using (var cmd = new MySqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Kullanici k = new Kullanici
                        {
                            TcKimlikNo = reader.GetString("tc_kimlik_no"),
                            SifreHash = reader.GetString("sifre_hash"),
                            Ad = reader.GetString("ad"),
                            Soyad = reader.GetString("soyad"),
                            Eposta = reader.GetString("eposta"),
                            DogumTarihi = reader.GetDateTime("dogum_tarihi"),
                            Cinsiyet = reader.GetString("cinsiyet"),
                            KullaniciTipi = reader.GetString("kullanici_tipi"),
                            ProfilResim = reader["profil_resim"] as byte[]
                        };
                        liste.Add(k);
                    }
                }
            }

            return liste;
        }

        public void HastaKaydet(Kullanici hasta, Kullanici doktor)
        {

            string otomatikSifre = SifreUret();
            hasta.SifreHash = otomatikSifre;


            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO Kullanicilar 
    (tc_kimlik_no, sifre_hash, ad, soyad, eposta, dogum_tarihi, cinsiyet, kullanici_tipi, profil_resim) 
    VALUES (@tc, @sifre, @ad, @soyad, @eposta, @dogum, @cinsiyet, 'Hasta', @profil_resim)";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@tc", hasta.TcKimlikNo);
                    cmd.Parameters.AddWithValue("@sifre", hasta.SifreHash);
                    cmd.Parameters.AddWithValue("@ad", hasta.Ad);
                    cmd.Parameters.AddWithValue("@soyad", hasta.Soyad);
                    cmd.Parameters.AddWithValue("@eposta", hasta.Eposta);
                    cmd.Parameters.AddWithValue("@dogum", hasta.DogumTarihi);
                    cmd.Parameters.AddWithValue("@cinsiyet", hasta.Cinsiyet);
                    cmd.Parameters.AddWithValue("@profil_resim", hasta.ProfilResim);

                    cmd.ExecuteNonQuery();
                }

                string insertHasta = "INSERT INTO Hastalar (hasta_id, doktor_id) VALUES (@hid, @did)";
                using (var cmd = new MySqlCommand(insertHasta, connection))
                {
                    cmd.Parameters.AddWithValue("@hid", hasta.TcKimlikNo);
                    cmd.Parameters.AddWithValue("@did", doktor.TcKimlikNo);

                    cmd.ExecuteNonQuery();
                }
            }

        }
        public string SifreUret(int uzunluk = 8)
        {
            const string karakterler = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789";
            Random rnd = new Random();
            return new string(Enumerable.Repeat(karakterler, uzunluk)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        public byte[] ResimDonusum(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"Hata: Resim dosyası bulunamadı: {imagePath}");
                return null;
            }
            try
            {
                return File.ReadAllBytes(imagePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Resim dosyası okunurken hata oluştu: {ex.Message}");
                return null;
            }
        }
        public Bitmap ByteArrayToBitmap(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return new Bitmap(ms);
            }
        }
        public byte[] ResimToByteArray(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Png); 
                return ms.ToArray();
            }
        }

        public void GuncelDiyetOnerisiGoster(string hastaId , List<diyet_dat> diyet)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"SELECT dt.Ad, dd.ek_notlar, dd.oneri_tarihi , dd.oneri_id
                   FROM DoktorDiyetOnerileri dd
                   JOIN DiyetTurleri dt ON dd.diyet_tur_id = dt.diyet_tur_id
                   WHERE dd.hasta_id = @hasta_id
                   ORDER BY dd.oneri_tarihi DESC";

                using (var cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@hasta_id", hastaId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.IsDBNull((reader.GetOrdinal("ek_notlar"))))
                            {
                                diyet.Add(new diyet_dat
                                {
                                    diyet_tarih_ = reader.GetDateTime("oneri_tarihi"),
                                    diyet_isim_ = reader.GetString("Ad"),
                                    diyet_eknot_ = "NULL",
                                    diyet_id = reader.GetInt32("oneri_id")

                                });
                            }
                            else
                            {
                                diyet.Add(new diyet_dat
                                {
                                    diyet_tarih_ = reader.GetDateTime("oneri_tarihi"),
                                    diyet_isim_ = reader.GetString("Ad"),
                                    diyet_eknot_ = reader.GetString("ek_notlar"),
                                    diyet_id = reader.GetInt32("oneri_id")
                                });
                            }
                        }
                    }
                }
                connection.Close();
            }
        }


        public void GuncelEgzersizOnerisiGoster(string hastaId,List<egzersiz_dat> dat)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"SELECT et.egzersiz_adi, de.ek_notlar, de.oneri_tarihi , de.oneri_id
                    FROM DoktorEgzersizOnerileri de
                    JOIN EgzersizTurleri et ON de.egzersiz_tur_id = et.egzersiz_tur_id
                    WHERE de.hasta_id = @hasta_id
                    ORDER BY de.oneri_tarihi DESC";

                using (var cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@hasta_id", hastaId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.IsDBNull((reader.GetOrdinal("ek_notlar"))))
                            {
                                dat.Add(new egzersiz_dat
                                {
                                    oneri_tarih_ = reader.GetDateTime("oneri_tarihi"),
                                    egzersiz_ad_ = reader.GetString("egzersiz_adi"),
                                    oneri_eknot_ = "NULL",
                                    egzersiz_id = reader.GetInt32("oneri_id")

                                });
                            }
                            else
                            {
                                dat.Add(new egzersiz_dat
                                {
                                    oneri_tarih_ = reader.GetDateTime("oneri_tarihi"),
                                    egzersiz_ad_ = reader.GetString("Ad"),
                                    oneri_eknot_ = reader.GetString("ek_notlar"),
                                    egzersiz_id = reader.GetInt32("oneri_id")
                                });
                            }
                        }
                    }
                }
                connection.Close();
            }
        }

        public void GuncelKanSekeriOlcumleriGoster(string hastaId,List<kan_hepsi_Dat> dat)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"SELECT olcum_degeri_mg_dl, olcum_tarihi, olcum_tipi, notlar , olcum_id
                    FROM KanSekeriOlcumleri
                    WHERE hasta_id = @hastaId
                    ORDER BY olcum_tarihi DESC";

                using (var cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@hastaId", hastaId);

                    using (var reader = cmd.ExecuteReader())
                    {
      
                        while (reader.Read())
                        {
                            if (reader.IsDBNull((reader.GetOrdinal("notlar"))))
                            {
                                dat.Add(new kan_hepsi_Dat
                                {
                                    kh_tarih = reader.GetDateTime("olcum_tarihi"),
                                    kh_tip = reader.GetString("olcum_tipi"),
                                    kh_not = "NULL",
                                    kh_id = reader.GetInt32("olcum_id"),
                                    kh_olcum = reader.GetInt32("olcum_degeri_mg_dl")

                                });
                            }
                            else
                            {
                                dat.Add(new kan_hepsi_Dat
                                {
                                    kh_tarih = reader.GetDateTime("olcum_tarihi"),
                                    kh_tip = reader.GetString("olcum_tipi"),
                                    kh_not = reader.GetString("notlar"),
                                    kh_id = reader.GetInt32("olcum_id"),
                                    kh_olcum = reader.GetInt32("olcum_degeri_mg_dl")
                                });
                            }
                        }
                    }
                }
            }
        }

        public void TarihKanSekeriOlcumleriGoster(string hastaId, List<kan_hepsi_Dat> dat,DateTime tarih)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"SELECT olcum_degeri_mg_dl, olcum_tarihi, olcum_tipi, notlar , olcum_id
                             FROM KanSekeriOlcumleri
                             WHERE hasta_id = @hastaId AND DATE(olcum_tarihi) = @olcumtarih
                             ORDER BY olcum_tarihi DESC";

                using (var cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@hastaId", hastaId);
                    cmd.Parameters.AddWithValue("@olcumtarih",tarih);
                    using (var reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            if (reader.IsDBNull((reader.GetOrdinal("notlar"))))
                            {
                                dat.Add(new kan_hepsi_Dat
                                {
                                    kh_tarih = reader.GetDateTime("olcum_tarihi"),
                                    kh_tip = reader.GetString("olcum_tipi"),
                                    kh_not = "NULL",
                                    kh_id = reader.GetInt32("olcum_id"),
                                    kh_olcum = reader.GetInt32("olcum_degeri_mg_dl")

                                });
                            }
                            else
                            {
                                dat.Add(new kan_hepsi_Dat
                                {
                                    kh_tarih = reader.GetDateTime("olcum_tarihi"),
                                    kh_tip = reader.GetString("olcum_tipi"),
                                    kh_not = reader.GetString("notlar"),
                                    kh_id = reader.GetInt32("olcum_id"),
                                    kh_olcum = reader.GetInt32("olcum_degeri_mg_dl")
                                });
                            }
                        }
                    }
                }
            }
        }

        public void GuncelBelirtilerGoster(string hastaId,List<belirti_dat> be)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();


                string tarihSorgu = @"SELECT MAX(kayit_tarihi)
                           FROM HastaBelirtiKayitlari
                           WHERE hasta_id = @hastaId";

                DateTime? guncelTarih = null;
                using (var tarihCmd = new MySqlCommand(tarihSorgu, connection))
                {
                    tarihCmd.Parameters.AddWithValue("@hastaId", hastaId);
                    var result = tarihCmd.ExecuteScalar();
                    if (result != DBNull.Value)
                        guncelTarih = Convert.ToDateTime(result);
                }

                if (guncelTarih == null)
                {
                    return;
                }


                string sql = @"SELECT bt.belirti_adi, hb.kayit_tarihi ,hb.kayit_id
                    FROM HastaBelirtiKayitlari hb
                    JOIN BelirtiTurleri bt ON hb.belirti_tur_id = bt.belirti_tur_id
                    WHERE hb.hasta_id = @hastaId AND hb.kayit_tarihi = @guncelTarih";

                using (var cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@hastaId", hastaId);
                    cmd.Parameters.AddWithValue("@guncelTarih", guncelTarih);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            be.Add(new belirti_dat
                            {
                                belirti_kayit = reader.GetDateTime("kayit_tarihi"),
                                belirti_isim = reader.GetString("belirti_adi"),
                                belirti_id = reader.GetInt32("kayit_id")

                            });
                        }
                    }
                }
            }
        }

        public void GecmisBelirtilerGoster(string hastaId,List<belirti_dat> be )
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"SELECT bt.belirti_adi, hb.kayit_tarihi , hb.kayit_id
                   FROM HastaBelirtiKayitlari hb
                   JOIN BelirtiTurleri bt ON hb.belirti_tur_id = bt.belirti_tur_id
                   WHERE hb.hasta_id = @hastaId
                   ORDER BY hb.kayit_tarihi ASC";

                using (var cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@hastaId", hastaId);
                    using (var reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            be.Add(new belirti_dat
                            {
                                belirti_kayit = reader.GetDateTime("kayit_tarihi"),
                                belirti_isim = reader.GetString("belirti_adi"),
                                belirti_id = reader.GetInt32("kayit_id")

                            });
                        }
                    }
                }
            }
        }

        public void IlkKanSekeriGoster(string hastaId)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"SELECT ilk_kan_sekeri, olusturulma_tarihi
                    FROM İlkKanSekeri
                    WHERE hasta_id = @hastaId
                    ORDER BY olusturulma_tarihi ASC
                    LIMIT 1";

                using (var cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@hastaId", hastaId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("İlk Kan Şekeri:");
                        if (reader.Read())
                        {
                            Console.WriteLine($"Tarih: {reader["olusturulma_tarihi"]}, Değer: {reader["ilk_kan_sekeri"]} mg/dL");
                        }
                        else
                        {
                            Console.WriteLine("İlk kan şekeri verisi bulunamadı.");
                        }
                    }
                }
            }
        }

        public void GuncelInsulinOnerisiGoster(string hastaId,List<insulin_dat> ins)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"SELECT olcum_tipi_referans, ortalama_kan_sekeri_mg_dl, onerilen_doz_ml, hesaplama_notu, oneri_tarihi , insulin_oneri_id
                   FROM InsulinOnerileri
                   WHERE hasta_id = @hastaId
                   ORDER BY oneri_tarihi DESC";

                using (var cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@hastaId", hastaId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("Güncel İnsülin Önerisi:");
                        if (reader.Read())
                        {
                            while (reader.Read())
                            {
                                ins.Add(new insulin_dat
                                {
                                    insulin_time = reader.GetDateTime("oneri_tarihi"),
                                    insulin_not = reader.GetString("hesaplama_notu"),
                                    insulin_id = reader.GetInt32("insulin_oneri_id"),
                                    insulin_doz = reader.GetDouble("onerilen_doz_ml"),
                                    insulin_ref = reader.GetString("olcum_tipi_referans")

                                });
                            }
                        }
                        else
                        {
                            Console.WriteLine("Güncel insülin önerisi bulunamadı.");
                        }
                    }
                }
            }
        }

        public void GecmisInsulinOnerileriGoster(string hastaId)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"SELECT olcum_tipi_referans, ortalama_kan_sekeri_mg_dl, onerilen_doz_ml, hesaplama_notu, oneri_tarihi
                   FROM InsulinOnerileri
                   WHERE hasta_id = @hastaId
                   ORDER BY oneri_tarihi DESC";

                using (var cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@hastaId", hastaId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("Geçmiş İnsülin Önerileri:");
                        while (reader.Read())
                        {
                            Console.WriteLine($"Tarih: {reader["oneri_tarihi"]}, Tip: {reader["olcum_tipi_referans"]}, Ortalama Şeker: {reader["ortalama_kan_sekeri_mg_dl"]}, Doz: {reader["onerilen_doz_ml"]} ml, Not: {reader["hesaplama_notu"]}");
                        }
                    }
                }
            }
        }

        private int KanSekeriOlcumuEkle(KanSekeriOlcumleri olcum)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO KanSekeriOlcumleri
                         (hasta_id, olcum_degeri_mg_dl, olcum_tarihi, olcum_tipi, ortalama_dahil_mi, notlar)
                         VALUES (@hasta_id, @deger, @tarih, @tip, @dahil_mi, @notlar)";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@hasta_id", olcum.hasta_id);
                    cmd.Parameters.AddWithValue("@deger", olcum.olcum_degeri_mg_dl);
                    cmd.Parameters.AddWithValue("@tarih", olcum.olcum_tarihi);
                    cmd.Parameters.AddWithValue("@tip", olcum.olcum_tipi);
                    cmd.Parameters.AddWithValue("@dahil_mi", olcum.ortalama_dahil_mi);
                    cmd.Parameters.AddWithValue("@notlar", olcum.notlar ?? (object)DBNull.Value);
                    cmd.ExecuteNonQuery();
                    return (int)cmd.LastInsertedId;
                }
            }
        }

        public (InsulinOnerileri insulinOnerisi, List<string> olusanUyariMesajlari) HesaplaVeOneriYap(
    string hastaId, string doktorId, int olcumDegeri, string olcumTipi, DateTime olcumZamani)
        {
            List<string> olusanUyariMesajlari = new List<string>();
            InsulinOnerileri insulinOnerisi = null;
            int eklenenOlcumId = -1;

            OlcumSaatAraligi ilgiliAralik = olcumSaatAraliklari.FirstOrDefault(a => a.Tip == olcumTipi);
            bool ortalamayaDahilMi = true;
            string notlar = null;

            if (ilgiliAralik != null && olcumTipi != "Diğer")
            {
                if (olcumZamani.Hour < ilgiliAralik.BaslangicSaat || olcumZamani.Hour >= ilgiliAralik.BitisSaat)
                {
                    ortalamayaDahilMi = false;
                    notlar = "Ölçüm belirtilen saat aralığı dışında yapıldı. Ortalama hesaba katılmadı.";
                    olusanUyariMesajlari.Add("Ölçüm eksik! İstenilen saat aralığında ölçüm yapılması gerekiyor.");
                    MessageBox.Show("Ölçüm eksik! İstenilen saat aralığında ölçüm yapılması gerekiyor.");
                    KaydetUyari(new UyariKayitlari
                    {
                        doktor_id = doktorId,
                        hasta_id = hastaId,
                        ilgili_olcum_id = -1,
                        uyari_tipi = "Saat Dışı Ölçüm",
                        mesaj = $"Hasta {hastaId} için '{olcumTipi}' ölçümü belirtilen saat aralığı dışında yapıldı.",
                        olusturulma_tarihi = DateTime.Now
                    });
                }
            }

            try
            {
                eklenenOlcumId = KanSekeriOlcumuEkle(new KanSekeriOlcumleri
                {
                    hasta_id = hastaId,
                    olcum_degeri_mg_dl = olcumDegeri,
                    olcum_tarihi = olcumZamani,
                    olcum_tipi = olcumTipi,
                    ortalama_dahil_mi = ortalamayaDahilMi,
                    notlar = notlar
                });
            }
            catch (Exception ex)
            {
               MessageBox.Show($"Kan şekeri ölçümü kaydedilirken hata oluştu: {ex.Message}");
                olusanUyariMesajlari.Add("Kan şekeri ölçümü kaydedilirken bir hata oluştu.");
                return (null, olusanUyariMesajlari);
            }


            List<KanSekeriOlcumleri> gunlukOlcumler = GetGunlukOlcumler(hastaId, olcumZamani.Date);

            List<KanSekeriOlcumleri> ortalamaHesaplamaIcinOlcumler = new List<KanSekeriOlcumleri>();

            switch (olcumTipi)
            {
                case "Sabah":
                    ortalamaHesaplamaIcinOlcumler = gunlukOlcumler
                        .Where(m => m.olcum_tipi == "Sabah" && m.ortalama_dahil_mi)
                        .ToList();
                    break;
                case "Öğle":
                    ortalamaHesaplamaIcinOlcumler = gunlukOlcumler
                        .Where(m => (m.olcum_tipi == "Sabah" || m.olcum_tipi == "Öğle") && m.ortalama_dahil_mi)
                        .ToList();
                    break;
                case "İkindi":
                    ortalamaHesaplamaIcinOlcumler = gunlukOlcumler
                        .Where(m => (m.olcum_tipi == "Sabah" || m.olcum_tipi == "Öğle" || m.olcum_tipi == "İkindi") && m.ortalama_dahil_mi)
                        .ToList();
                    break;
                case "Akşam":
                    ortalamaHesaplamaIcinOlcumler = gunlukOlcumler
                        .Where(m => (m.olcum_tipi == "Sabah" || m.olcum_tipi == "Öğle" || m.olcum_tipi == "İkindi" || m.olcum_tipi == "Akşam") && m.ortalama_dahil_mi)
                        .ToList();
                    break;
                case "Gece":
                    ortalamaHesaplamaIcinOlcumler = gunlukOlcumler
                        .Where(m => (m.olcum_tipi == "Sabah" || m.olcum_tipi == "Öğle" || m.olcum_tipi == "İkindi" || m.olcum_tipi == "Akşam" || m.olcum_tipi == "Gece") && m.ortalama_dahil_mi)
                        .ToList();
                    break;
                default:
                    ortalamaHesaplamaIcinOlcumler = gunlukOlcumler
                        .Where(m => m.olcum_id == eklenenOlcumId && m.ortalama_dahil_mi)
                        .ToList();
                    break;
            }

            double ortalamaKanSekeri = 0;
            if (ortalamaHesaplamaIcinOlcumler.Any())
            {
                ortalamaKanSekeri = ortalamaHesaplamaIcinOlcumler.Average(m => m.olcum_degeri_mg_dl);
            }


            double onerilenDoz = 0.0;
            string durum = "Bilinmiyor";
            var kural = insulinRules.FirstOrDefault(r => ortalamaKanSekeri >= r.min && ortalamaKanSekeri <= r.max);
            if (kural.Item4 != null)
            {
                onerilenDoz = kural.dose;
                durum = kural.status;
            }

            insulinOnerisi = new InsulinOnerileri
            {
                hasta_id = hastaId,
                olcum_tipi_referans = olcumTipi,
                ortalama_kan_sekeri_mg_dl = ortalamaKanSekeri,
                onerilen_doz_ml = onerilenDoz,
                oneri_tarihi = DateTime.Now,
                hesaplama_notu = $"Ortalama {ortalamaHesaplamaIcinOlcumler.Count} ölçüm üzerinden hesaplandı."
            };
            KaydetInsulinOnerisi(insulinOnerisi);



            if (gunlukOlcumler.Count(m => m.ortalama_dahil_mi) < 3 && olcumTipi == "Gece")
            {
                olusanUyariMesajlari.Add("Yetersiz veri! Ortalama hesaplaması güvenilir değildir.");
                MessageBox.Show("Yetersiz veri! Ortalama hesaplaması güvenilir değildir.");
                KaydetUyari(new UyariKayitlari
                {
                    doktor_id = doktorId,
                    hasta_id = hastaId,
                    ilgili_olcum_id = eklenenOlcumId,
                    uyari_tipi = "Ölçüm Yetersiz",
                    mesaj = $"Hasta {hastaId} için günlük ölçüm sayısı yetersiz ({gunlukOlcumler.Count(m => m.ortalama_dahil_mi)}).",
                    olusturulma_tarihi = DateTime.Now
                });
            }


            if (olcumDegeri < 70 || olcumDegeri > 200)
            {
                string uyariMesaji = olcumDegeri < 70 ?
                                     $"Kan şekeri değeri çok düşük: {olcumDegeri} mg/dL." :
                                     $"Kan şekeri değeri çok yüksek: {olcumDegeri} mg/dL.";
                olusanUyariMesajlari.Add($"Acil Uyarı: {uyariMesaji} Doktora bildirildi.");
                MessageBox.Show($"Acil Uyarı: {uyariMesaji} Doktora bildirildi.");
                KaydetUyari(new UyariKayitlari
                {
                    doktor_id = doktorId,
                    hasta_id = hastaId,
                    ilgili_olcum_id = eklenenOlcumId,
                    uyari_tipi = "Kritik Eşik",
                    mesaj = $"Hasta {hastaId} için kritik kan şekeri değeri: {olcumDegeri} mg/dL ({uyariMesaji})",
                    olusturulma_tarihi = DateTime.Now
                });
            }



            return (insulinOnerisi, olusanUyariMesajlari);
        }

        public List<KanSekeriOlcumleri> GetGunlukOlcumler(string hastaId, DateTime tarih)
        {
            List<KanSekeriOlcumleri> olcumler = new List<KanSekeriOlcumleri>();
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT * FROM KanSekeriOlcumleri
                          WHERE hasta_id = @hasta_id AND DATE(olcum_tarihi) = DATE(@tarih)
                          ORDER BY olcum_tarihi ASC";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@hasta_id", hastaId);
                    cmd.Parameters.AddWithValue("@tarih", tarih.Date);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            olcumler.Add(new KanSekeriOlcumleri
                            {
                                olcum_id = reader.GetInt32("olcum_id"),
                                hasta_id = reader.GetString("hasta_id"),
                                olcum_degeri_mg_dl = reader.GetInt32("olcum_degeri_mg_dl"),
                                olcum_tarihi = reader.GetDateTime("olcum_tarihi"),
                                olcum_tipi = reader.GetString("olcum_tipi"),
                                ortalama_dahil_mi = reader.GetBoolean("ortalama_dahil_mi"),
                                notlar = reader["notlar"] == DBNull.Value ? null : reader.GetString("notlar")
                            });
                        }
                    }
                }
            }
            return olcumler;
        }

        private void KaydetInsulinOnerisi(InsulinOnerileri oneri)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO InsulinOnerileri
                          (hasta_id, olcum_tipi_referans, ortalama_kan_sekeri_mg_dl, onerilen_doz_ml, oneri_tarihi, hesaplama_notu)
                          VALUES (@hasta_id, @tip_referans, @ortalama_deger, @onerilen_doz, @oneri_tarihi, @notu)";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@hasta_id", oneri.hasta_id);
                    cmd.Parameters.AddWithValue("@tip_referans", oneri.olcum_tipi_referans);
                    cmd.Parameters.AddWithValue("@ortalama_deger", oneri.ortalama_kan_sekeri_mg_dl);
                    cmd.Parameters.AddWithValue("@onerilen_doz", oneri.onerilen_doz_ml);
                    cmd.Parameters.AddWithValue("@oneri_tarihi", oneri.oneri_tarihi);
                    cmd.Parameters.AddWithValue("@notu", oneri.hesaplama_notu ?? (object)DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void KaydetUyari(UyariKayitlari uyari)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO UyariKayitlari
                         (doktor_id, hasta_id, ilgili_olcum_id, uyari_tipi, mesaj, olusturulma_tarihi)
                         VALUES (@doktor_id, @hasta_id, @olcum_id, @tip, @mesaj, @tarih)";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@doktor_id", uyari.doktor_id);
                    cmd.Parameters.AddWithValue("@hasta_id", uyari.hasta_id);
                    cmd.Parameters.AddWithValue("@olcum_id", uyari.ilgili_olcum_id);
                    cmd.Parameters.AddWithValue("@tip", uyari.uyari_tipi);
                    cmd.Parameters.AddWithValue("@mesaj", uyari.mesaj);
                    cmd.Parameters.AddWithValue("@tarih", uyari.olusturulma_tarihi);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private List<OlcumSaatAraligi> olcumSaatAraliklari = new List<OlcumSaatAraligi>
        {
            new OlcumSaatAraligi { Tip = "Sabah", BaslangicSaat = 7, BitisSaat = 8 },
            new OlcumSaatAraligi { Tip = "Öğle", BaslangicSaat = 12, BitisSaat = 13 },
            new OlcumSaatAraligi { Tip = "İkindi", BaslangicSaat = 15, BitisSaat = 16 },
            new OlcumSaatAraligi { Tip = "Akşam", BaslangicSaat = 18, BitisSaat = 19 },
            new OlcumSaatAraligi { Tip = "Gece", BaslangicSaat = 22, BitisSaat = 23 },
            new OlcumSaatAraligi { Tip = "Diğer", BaslangicSaat = 0, BitisSaat = 24 }
        };

                private List<(int min, int max, double dose, string status)> insulinRules = new List<(int, int, double, string)>
        {
            (int.MinValue, 69, 0.0, "Hipoglisemi"),
            (70, 110, 0.0, "Normal"),
            (111, 150, 1.0, "Orta Yüksek"),
            (151, 200, 2.0, "Yüksek"),
            (201, int.MaxValue, 3.0, "Çok Yüksek")
        };

        public void MailGonder(string aliciMail, string hastaAdi, string bilgiler)
        {
            try
            {
                MailMessage mesaj = new MailMessage();
                mesaj.From = new MailAddress(aliciMail);
                mesaj.To.Add(aliciMail);
                mesaj.Subject = "Hasta Bilgileri";
                mesaj.Body = $"Merhaba {hastaAdi},\n\n{bilgiler}";

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(aliciMail, "cttz svuw ukwz vwvy");
                smtp.EnableSsl = true;
                smtp.Send(mesaj);

                Console.WriteLine("Mail gönderildi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Mail gönderilirken hata oluştu: " + ex.Message);
            }
        }

        public void HastaTarihInsulin(string hastaId,DateTime tarih,StringBuilder strin)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"SELECT onerilen_doz_ml, oneri_tarihi
                             FROM InsulinOnerileri
                             WHERE hasta_id = @hastaId AND DATE(oneri_tarihi) = @oneri_tarih
                             ORDER BY oneri_tarihi DESC";

                using (var cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@hastaId", hastaId);
                    cmd.Parameters.AddWithValue("oneri_tarih",tarih);
                    using (var reader = cmd.ExecuteReader())
                    {
                        strin.AppendLine("İnsülin Önerileri:");
                        while (reader.Read())
                        {
                            strin.AppendLine($"Tarih: {reader["oneri_tarihi"]}, Doz: {reader["onerilen_doz_ml"]} ml");
                        }
                    }
                }
            }
        }

        public void GunlukKanSekeriRaporuGoster(string hastaId, DateTime tarih,StringBuilder strin,List<KanSekeriOlcumleri> gelen)
        {
            strin.AppendLine($"\n--- {tarih.ToShortDateString()} Tarihli Kan Şekeri Raporu (Hasta: {hastaId}) ---");

            List<KanSekeriOlcumleri> gunlukOlcumler = GetGunlukOlcumler(hastaId, tarih);
            

            if (!gunlukOlcumler.Any())
            {
                strin.AppendLine("Bu tarihe ait kan şekeri ölçümü bulunamadı.");
                return;
            }

            strin.AppendLine("Ölçümler:");
            foreach (var olcum in gunlukOlcumler)
            {
                string dahilDurumu = olcum.ortalama_dahil_mi ? "" : " (Ortalamaya Dahil Değil)";
                strin.AppendLine($"- Tarih: {olcum.olcum_tarihi:dd.MM.yyyy HH:mm:ss}, Değer: {olcum.olcum_degeri_mg_dl} mg/dL, Tip: {olcum.olcum_tipi}{dahilDurumu}");
                if (!string.IsNullOrEmpty(olcum.notlar))
                {
                    strin.AppendLine($"  Not: {olcum.notlar}");
                }
            }

            

            List<KanSekeriOlcumleri> ortalamaHesaplamaIcinOlcumler = gunlukOlcumler
                                                                        .Where(o => o.ortalama_dahil_mi)
                                                                        .ToList();

            if (ortalamaHesaplamaIcinOlcumler.Any())
            {
                double ortalama = ortalamaHesaplamaIcinOlcumler.Average(o => o.olcum_degeri_mg_dl);
                strin.AppendLine($"\nGünlük Ortalama Kan Şekeri (dahil edilen ölçümler): {ortalama:F2} mg/dL");


                if (ortalamaHesaplamaIcinOlcumler.Count < 3)
                {
                    strin.AppendLine("Uyarı: Yetersiz veri! Ortalama hesaplaması güvenilir değildir (3'ten az ölçüm).");
                }
            }
            else
            {
                strin.AppendLine("\nOrtalama hesaplamaya dahil edilecek ölçüm bulunamadı.");
            }
            strin.AppendLine("-----------------------------------------------------");

            gelen.AddRange(gunlukOlcumler);
        }

        public void GuncelUyarilarGoster(string doktorID,List<uyarikayit> kay)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"SELECT hasta_id,uyari_tipi, mesaj, olusturulma_tarihi , uyari_id
                   FROM UyariKayitlari
                   WHERE doktor_id = @doktor_id
                   ORDER BY olusturulma_tarihi DESC";

                using (var cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@doktor_id", doktorID);
                    using (var reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            kay.Add(new uyarikayit
                            {
                                uyari_time = reader.GetDateTime("olusturulma_tarihi"),
                                uyari_tip = reader.GetString("uyari_tipi"),
                                uyari_mesaj = reader.GetString("mesaj"),
                                uyari_hasta = reader.GetString("hasta_id"),
                                uyari_id = reader.GetInt32("uyari_id")
                            });                       
                        }
                    }
                }
            }
        }

        public void UygulamaDurumlariniGoster(string hastaId,StringBuilder st)
        {
            st.AppendLine($"\n--- Hasta {hastaId} İçin Tüm Uygulama Durumları ---");

            st.AppendLine("\n--- Diyet Uygulama Kayıtları ---");
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string queryDiyet = @"
             SELECT hdk.uygulama_durumu, hdk.kayit_tarihi, dt.Ad AS diyet_adi
             FROM HastaDiyetKayitlari hdk
             JOIN DoktorDiyetOnerileri ddo ON hdk.doktor_diyet_oneri_id = ddo.oneri_id
             JOIN DiyetTurleri dt ON ddo.diyet_tur_id = dt.diyet_tur_id
             WHERE hdk.hasta_id = @hastaId
             ORDER BY hdk.kayit_tarihi DESC";

                using (var cmd = new MySqlCommand(queryDiyet, connection))
                {
                    cmd.Parameters.AddWithValue("@hastaId", hastaId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        bool foundDiyet = false;
                        while (reader.Read())
                        {
                            foundDiyet = true;

                            string uygulamaDurumu = reader["uygulama_durumu"] == DBNull.Value ? "Belirtilmemiş" : reader.GetString("uygulama_durumu");
                            DateTime kayitTarihi = reader.GetDateTime("kayit_tarihi");
                            string diyetAdi = reader["diyet_adi"] == DBNull.Value ? "Bilinmiyor" : reader.GetString("diyet_adi");
                            st.AppendLine($"- Diyet: {diyetAdi}, Durum: {uygulamaDurumu}, Tarih: {kayitTarihi:dd.MM.yyyy HH:mm:ss}");
                        }
                        if (!foundDiyet)
                        {
                            st.AppendLine("Diyet kaydı bulunamadı.");
                        }
                    }
                }
            }

            st.AppendLine("\n--- Egzersiz Uygulama Kayıtları ---");
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string queryEgzersiz = @"
             SELECT hek.uygulama_durumu, hek.kayit_tarihi, et.egzersiz_adi
             FROM HastaEgzersizKayitlari hek
             JOIN DoktorEgzersizOnerileri deo ON hek.doktor_egzersiz_oneri_id = deo.oneri_id
             JOIN EgzersizTurleri et ON deo.egzersiz_tur_id = et.egzersiz_tur_id
             WHERE hek.hasta_id = @hastaId
             ORDER BY hek.kayit_tarihi DESC";

                using (var cmd = new MySqlCommand(queryEgzersiz, connection))
                {
                    cmd.Parameters.AddWithValue("@hastaId", hastaId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        bool foundEgzersiz = false;
                        while (reader.Read())
                        {
                            foundEgzersiz = true;
                            string uygulamaDurumu = reader["uygulama_durumu"] == DBNull.Value ? "Belirtilmemiş" : reader.GetString("uygulama_durumu");
                            DateTime kayitTarihi = reader.GetDateTime("kayit_tarihi");
                            string egzersizAdi = reader["egzersiz_adi"] == DBNull.Value ? "Bilinmiyor" : reader.GetString("egzersiz_adi");
                            st.AppendLine($"- Egzersiz: {egzersizAdi}, Durum: {uygulamaDurumu}, Tarih: {kayitTarihi:dd.MM.yyyy HH:mm:ss}");
                        }
                        if (!foundEgzersiz)
                        {
                            st.AppendLine("Egzersiz kaydı bulunamadı.");
                        }
                    }
                }
            }
            st.AppendLine("------------------------------------------");
        }

    }


}
