using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkingApp.Models.ConnectionModels
{
    public class ConnectionDetailModel
    {
        public int ConnectionID { get; set; }
        public string ConnectionName { get; set; }
        public string Job { get; set; }
        public string Employer { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
    }
}
