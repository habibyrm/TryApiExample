using Microsoft.AspNetCore.Mvc;
using stajapi.Dtos;
using stajapi.Entities;
using stajapi.Services;

namespace stajapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogumKayitController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly DogumKayitService _dogumKayitService;
        public DogumKayitController(DataContext context, DogumKayitService dogumKayitService)
        {
            _context = context;
            _dogumKayitService = dogumKayitService;
        }

        /// <summary>
        /// Doğum kaydı oluşturur.
        /// </summary>
        /// <param name="dto">Doğum kaydı için gerekli bilgiler.</param>
        /// <returns>Kayıt durumu.</returns>
        [HttpPost]
        public IActionResult DogumKayitOlustur([FromBody] DogumKayitDto dto)
        {
            try
            {
                _dogumKayitService.DogumKaydet(dto);
                return Ok("Doğum kaydı başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
