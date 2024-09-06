using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using stajapi.Dtos;
using stajapi.Entities;
using stajapi.Helpers;
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
        //[Authorize]
        [HttpPost]
        public IActionResult DogumKayitOlustur([FromBody] DogumKayitDto dto)
        {
            try
            {
                if (!string.IsNullOrEmpty(CurrentToken.Token))
                {
                    DateTime dateNow = DateTime.Now;
                    DateTime dateExpire = CurrentToken.tokenExpireDate;
                    if (dateExpire > dateNow)
                    {
                        _dogumKayitService.DogumKaydet(dto);
                        return Ok("Doğum kaydı başarıyla oluşturuldu.");
                    }
                    else
                        return BadRequest("Token süresi dolmuştur.");
                }
                else
                    return BadRequest("Token bulunamadı");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
