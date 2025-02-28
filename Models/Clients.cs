using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM_SPA_Template.Models
{
    class Client
    {
        public int Id { get; set; }
        public string DNI { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public string Cognoms { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public DateOnly DataAlta { get; set; }
    }
}
