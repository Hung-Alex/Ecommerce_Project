﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses.Payments
{
    public record PaymentsResultDTO
    {
        public string PaymentUrl { get; set; }
    }
}
