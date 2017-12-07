using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkingApp.Models.ConnectionModels
{
    public class ConnectionCreateModel
    {
        [Required]
        public string ConnectionName { get; set; }

        [Required]
        public string Job { get; set; }

        [Required]
        public string Employer { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Notes { get; set; }

    }
}
