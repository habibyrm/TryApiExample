using Microsoft.EntityFrameworkCore;
using stajapi.Dtos;
using stajapi.Entities;
using stajapi.Helpers;

namespace stajapi.Services
{
    public class DogumKayitService
    {
        private readonly DataContext _context;

        public DogumKayitService(DataContext context)
        {
            _context = context;
        }
        public void DogumKaydet(DogumKayitDto dto)
        {
            var zaman = DateTime.Now;
            if (!dto.AnneTc.All(char.IsDigit))
                throw new Exception("T.C. verisi sadece rakamlardan oluşmalıdır. ");
            if (!dto.BabaTc.All(char.IsDigit))
                throw new Exception("T.C. verisi sadece rakamlardan oluşmalıdır. ");
            var anne = Helper.KisiBilgileriGetir(dto.AnneTc);
            if (anne == null)
            {
                throw new Exception("Anneye ait kayıt bulunamadı. ");
            }
            var baba = Helper.KisiBilgileriGetir(dto.BabaTc);
            if (baba == null)
            {
                throw new Exception("Babaya ait aile kayıt bulunamadı.");
            }
            if (baba.cinsiyet_kodu != 1)
                throw new Exception("Babanın cinsiyeti erkek olamalıdır. ");
            if (anne.cinsiyet_kodu != 2)
                throw new Exception("Annenin cinsiyeti kadın olmalıdır. ");
            var yas = Helper.YasHesaplama(dto.DogumTarihi);
            if (dto == null)
            {
                throw new Exception("Veri null olarak gelmiştir.");
            }
            if (yas >= 7)
            {
                throw new Exception("Doğan çocuğun yaşı 7'den küçük olmalıdır!");
            }

            if (!Helper.IsEsMi(dto))
            {
                throw new Exception("Bu kişiler evli değildir!");
            }

            if (Helper.IsKisiVarMi(dto, zaman))
            {
                throw new Exception("Bu kişiye ait kayıt vardır!");
            }
            int cinsiyet = Helper.Cinsiyet_Kontrol(dto.Cinsiyet);
            int sehir = Helper.Sehir_Kontrol(dto.Sehir);
            try
            {
                int? maxBireySiraNo = _context.Aile
                    .Where(a => a.AileSiraNo == anne.AileSiraNo)
                    .Max(a => (int?)a.BireySiraNo);

                int yeniBireySiraNo = (maxBireySiraNo ?? 0) + 1;

                var aile = new Aile
                {
                    AileSiraNo = anne.AileSiraNo,
                    BireySiraNo = yeniBireySiraNo,
                    AnneTc = dto.AnneTc,
                    BabaTc = dto.BabaTc,
                    CiltKodu = anne.cilt_kodu
                };

                _context.Aile.Add(aile);
                _context.SaveChanges();

                var yeniksi = _context.Aile.FirstOrDefault(x => x.AileSiraNo == aile.AileSiraNo && x.BireySiraNo == yeniBireySiraNo);
                int ailekodu = yeniksi?.IdAile ?? 0;

                var kisi = new Kisi
                {
                    Tc = Helpers.TC.GenerateRandomTC(),
                    Ad = dto.CocukAd,
                    Soyad = dto.CocukSoyad,
                    DurumKodu = 1,
                    MedeniHalKodu = 1,
                    CinsiyetKodu = cinsiyet,
                    DogumYeriKodu = sehir,
                    AileKodu = ailekodu
                };

                _context.Kisi.Add(kisi);
                _context.SaveChanges();
                OlayKaydi(kisi.Tc, zaman, 1);
                OlayKaydi(dto.AnneTc, dto.BabaTc, zaman, 1);
            }
            catch (Exception ex)
            {
                throw new Exception($"Veritabanına veri eklenirken hata oluştu: {ex.Message}");
            }
        }
        public void OlayKaydi(string tc, DateTime zaman, int olaykodu)
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseNpgsql("ConnectionStrings")
                .Options;
            using (var context = new DataContext(options))
            {
                var olay = new OlayGecmisi
                {
                    KisiTc = tc,
                    OlayKodu = olaykodu,
                    KullaniciId = 1,
                    Zaman = zaman
                };

                context.OlayGecmisi.Add(olay);
                context.SaveChanges();
            }
        }
        public void OlayKaydi(string tc, string es_tc, DateTime zaman, int olaykodu)
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                        .UseNpgsql("ConnectionStrings")
                        .Options;
            using (var context = new DataContext(options))
            {
                var olay = new OlayGecmisi
                {
                    KisiTc = tc,
                    EsTc = es_tc,
                    OlayKodu = olaykodu,
                    KullaniciId = 1,
                    Zaman = zaman
                };

                context.OlayGecmisi.Add(olay);
                context.SaveChanges();
            }
        }
    }
}

