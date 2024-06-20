﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared
{
    public class Result<T>
    {
        public T Data { get; set; }
        public Result(T data)
        {
            Data = data;
        }
    }
}
