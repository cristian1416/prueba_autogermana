using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autogermana.Application.DTO
{
    public class BaseResponseDTO
    {
        public int status { get; set; }
        public bool result { get; set; }
        public string? errors { get; set; }
        public string time { get; set; } = "0 ms";
    }
}
