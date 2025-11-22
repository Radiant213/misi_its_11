using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using Microsoft.Data.SqlClient;
using System.Data;


namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class KategoriController : ControllerBase
    {
        private readonly Koneksi _koneksi;
        public KategoriController(Koneksi koneksi)
        {
            _koneksi = koneksi;
        }

        // Endpoint default
        [HttpGet]
        public IActionResult GetAll()
        {
            var list = new List<object>();
            using (SqlConnection conn = _koneksi.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Kategori", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new

                    {
                        Id = reader["Id"],
                        NamaKategori = reader["NamaKategori"]

                    });
                }
                reader.Close();
            }
            return Ok(list);
        }

        // Endpoint "NamaKategori" ini tuh contoh yang BENAR/AMAN
        // - Menggunakan parameter SQL (@nama) sehingga input user tidak langsung disisipkan ke query.
        // - Parameter diberikan tipe dan ukuran eksplisit untuk menghindari tebakan tipe yang buruk.
        // Keuntungan: mencegah SQL Injection, rencana eksekusi lebih konsisten.
        // Endpoint NamaKategori yang lebih bagus
        [HttpGet("NamaKategori")]
        public IActionResult GetByNamaAddWithValue([FromQuery] string nama)
        {
            if (string.IsNullOrWhiteSpace(nama))
                return BadRequest("Parameter 'nama' wajib diisi.");

            var list = new List<object>();
            using (SqlConnection conn = _koneksi.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT Id, NamaKategori FROM Kategori WHERE NamaKategori = @nama", conn))
                {
                    cmd.Parameters.AddWithValue("@nama", nama ?? (object)DBNull.Value);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new
                            {
                                Id = reader["Id"],
                                NamaKategori = reader["NamaKategori"]
                            });
                        }
                    }
                }
            }
            return Ok(list);
        }

        // Endpoint "NamaKategoriLemah" ini itu contoh yang LEMAH / TIDAK AMAN
        // - Membuat query dengan menggabungkan langsung input user ke string SQL (concatenation).
        // - RENTAN terhadap SQL Injection. Contoh eksploitasi: jika user mengirim "Makanan' OR '1'='1"
        //   maka query menjadi: SELECT ... WHERE NamaKategori = 'Makanan' OR '1'='1' -> mengembalikan semua baris.
        [HttpGet("NamaKategoriLemah")]
        public IActionResult GetByNama_Lemah([FromQuery] string nama)
        {
            if (string.IsNullOrWhiteSpace(nama))
                return BadRequest("Parameter 'nama' wajib diisi.");

            var list = new List<object>();
            using (SqlConnection conn = _koneksi.GetConnection())
            {
                conn.Open();

                // INI CONTOH TIDAK AMAN: memasukkan input langsung ke query
                string query = "SELECT Id, NamaKategori FROM Kategori WHERE NamaKategori = '" + nama + "'";
                using (var cmd = new SqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new
                            {
                                Id = reader["Id"],
                                NamaKategori = reader["NamaKategori"]
                            });
                        }
                    }
                }
            }
            return Ok(list);
        }
    }
}


