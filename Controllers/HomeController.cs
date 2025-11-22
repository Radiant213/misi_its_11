using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    //home
    [ApiController]
    [Route("api/[controller]")]
    public class JsontestController : Controller
    {
        [HttpGet("{noRumah}")]
        public string noRumah(int noRumah)
        {
            return "Nomor rumah kamu " + noRumah + "!";
        }


        [HttpGet("apa")]
        public string Apa()
        {
            return "Apa kabar?";
        }

        [HttpGet("user")]
        public object User()
        {
            return new
            {
                Id = 1,
                Nama = "Iwak",
                Username = "Idk",
                Role = new String[] {"Admin", "User", "Mode"}
            };
        }

        // 1. Ringkasan Produk + Statistik Dasar
        [HttpGet("produk-summary")]
        public object ProdukSummary()
        {
            return new
            {
                JumlahProduk = 25,
                RataRataHarga = 15400,
                ProdukTertinggi = new
                {
                    Id = 8,
                    Nama = "Kabel HDMI",
                    Harga = 75000
                },
                ProdukTerendah = new
                {
                    Id = 1,
                    Nama = "Penghapus",
                    Harga = 1500
                }
            };
        }


        // 2. Produk Per Kategori (Grouped Response)
        [HttpGet("produk-grouped")]
        public object ProdukGrouped()
        {
            return new
            {
                Elektronik = new[]
                {
                    new
                    {
                        Id = 1,
                        Nama = "Keyboard"
                    },
                    new
                    {
                        Id = 2,
                        Nama = "Mouse"
                    }
                },

                ATK = new[]
                {
                    new
                    {
                        Id = 3,
                        Nama = "Buku"
                    },
                    new
                    {
                        Id = 4,
                        Nama = "Pulpen"
                    }
                }
            };
        }


        // 3. Response Summary Product
        [HttpGet("summary")]
        public object Summary()
        {
            return new
            {
                TotalProduk = 12,
                ProdukTermahal = new
                {
                    Id = 4,
                    Nama = "Laptop",
                    Harga = 8000000
                },
                ProdukTermurah = new
                {
                    Id = 1,
                    Nama = "Pensil",
                    Harga = 2000
                },
                DaftarProduk = new[]
                {
                    new
                    {
                        Id = 1,
                        Nama = "Pensil",
                    },
                    new
                    {
                        Id = 2,
                        Nama = "Buku"
                    }
                }
            };
        }



    }
}
