using Microsoft.EntityFrameworkCore;
using stajapi.Entities;

namespace stajapi.Helpers
{
    public class OlayKayit
    {
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
