using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RModel.DTO.Result
{
    public class FormResponse
    {
        public int ResponseCode { get; set; }

        public string? ResponseMessage { get; set; }

        public string? ResponseMessageCode { get; set; }

        public object? ResponseId { get; set; }
    }

}
