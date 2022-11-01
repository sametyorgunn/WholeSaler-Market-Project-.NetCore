using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos
{
    public class UserRegisterDto
    {
		public string UserName { get; set; }
		public string NameSurname { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public string CompanyName { get; set; }
        public bool Status { get; set; }
        public string TaskNo { get; set; }
        public int RoleId { get; set; }
    }
}
