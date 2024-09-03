using Microsoft.EntityFrameworkCore;
using stajapi.Dtos;
using stajapi.Entities;

namespace stajapi.Helpers
{
    public class Helper
    {
        public static int Cinsiyet_Kontrol(string cinsiyet)
        {
            cinsiyet = cinsiyet.ToLower();
            if (cinsiyet == "erkek")
            {
                return 1;
            }
            else if (cinsiyet == "kadın")
            {
                return 2;
            }
            return 0;

        }

        public static int Sehir_Kontrol(string sehir)
        {
            sehir = sehir.ToLower();
            if (sehir == "istanbul")
            {
                return 1;
            }
            else if (sehir == "ankara")
            {
                return 2;
            }
            else if (sehir == "tokat")
            {
                return 3;
            }
            else if (sehir == "sakarya")
            {
                return 4;
            }
            return 0;
        }
        public static int YasHesaplama(DateTime? v)
        {
            DateTime tarih = v.Value.Date;
            DateTime today = DateTime.Today;
            int yas = today.Year - tarih.Year;
            //Eğer doğum günü bu yıl henüz gelmediyse, yaşı bir azalt.
            if (tarih > today.AddYears(-yas))
            { yas--; }
            return yas;
        }
        public static bool IsEsMi(DogumKayitDto dto)
        {
            string estc_anne = Helper.EsBilgileriGetir(dto.AnneTc).EsTc;
            string estc_baba = Helper.EsBilgileriGetir(dto.BabaTc).EsTc;

            if (estc_anne == dto.BabaTc) return true;
            if (estc_baba == dto.AnneTc) return true;

            return false;
        }

        public static bool IsKisiVarMi(DogumKayitDto dto, DateTime zaman)
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                        .UseNpgsql("ConnectionStrings")
                        .Options;
            using (var _context = new DataContext(options)) { 
                var kisi = _context.Kisi.FirstOrDefault(x => x.Ad == dto.CocukAd);
            var anne = Helper.KisiBilgileriGetir(dto.AnneTc);
            if (kisi != null)
            {
                int aileSiraNo = anne.AileSiraNo;
                var aile = _context.Aile.FirstOrDefault(x => x.AileSiraNo == aileSiraNo);

                if (aile != null)
                {
                    var tarih = _context.OlayGecmisi.FirstOrDefault(x => x.KisiTc == kisi.Tc && x.Zaman == zaman);
                    if (tarih != null)
                    {
                        return true;
                    }
                }
            }
            }

            return false;
        }
        public static KisiBilgileri KisiBilgileriGetir(string tc)
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseNpgsql("ConnectionStrings")
                .Options;

            using (var context = new DataContext(options))
            {
                return context.Kisi
                    .Include(k => k.Aile)
                    .Include(k => k.MedeniHal)
                    .Include(k => k.Durum)
                    .Include(k => k.OlayGecmisi)
                    .Where(k => k.Tc == tc)
                    .Select(k => new KisiBilgileri
                    {
                        Id = k.IdKisi,
                        Ad = k.Ad,
                        Soyad = k.Soyad,
                        AileSiraNo = k.Aile.AileSiraNo.Value,
                        BireySiraNo = k.Aile.BireySiraNo.Value,
                        EsTc = k.Aile.EsTc,
                        cilt_kodu = k.Aile.CiltKodu,
                        AnneTc = k.Aile.AnneTc,
                        BabaTc = k.Aile.BabaTc,
                        MedeniHal_kodu = k.MedeniHalKodu,
                        MedeniHal_aciklamasi = k.MedeniHal.Aciklamasi,
                        Durum_aciklamasi = k.Durum.Aciklamasi,
                        Durum_kodu = k.DurumKodu,
                        cinsiyet_kodu = k.CinsiyetKodu,
                        Tc = tc
                    }).FirstOrDefault();
            }
        }

        public static KisiBilgileri EsBilgileriGetir(string tc)
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseNpgsql("ConnectionStrings")
                .Options;
            using (var context = new DataContext(options))
            {
                return context.Kisi
                        .Include(k => k.Aile)
                        .Where(k => k.Tc == tc)
                        .Select(k => new KisiBilgileri
                        {
                            Id = k.IdKisi,
                            Ad = k.Ad,
                            MedeniHal_kodu = k.MedeniHalKodu,
                            AileSiraNo = k.Aile.AileSiraNo.Value,
                            BireySiraNo = k.Aile.BireySiraNo.Value,
                            EsTc = k.Aile.EsTc
                        }).FirstOrDefault();
            }
        }

        //public static DateTime? Dogumtarihi(string tc)
        //{
        //    var options = new DbContextOptionsBuilder<DataContext>()
        //        .UseNpgsql("ConnectionStrings")
        //        .Options;
        //    using (var context = new DataContext(options))
        //        return context.OlayGecmisi
        //            .Include(k => k.KisiTcNavigation)
        //            .Where(k => k.KisiTc == tc)
        //            .Select(k => k.Zaman)
        //            .FirstOrDefault();
        //}
    }
}
