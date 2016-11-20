using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DesafioMarvel.Models
{
    public class Keys
    {
        [DisplayName("Public Key")]
        [Required]
        public string PublicKey { get; set; }
        [DisplayName("Private Key")]
        [Required]
        public string PrivateKey { get; set; }
    }
}