﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DTOs
{
    public class ValidationError
    {
        public string FieldName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
