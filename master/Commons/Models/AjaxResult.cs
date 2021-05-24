﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GPPSU.Commons.Models
{
    public class AjaxResult : BaseResult
    {
        public string ValueSuccess { get { return VALUE_SUCCESS; } }
        public string ValueError { get { return VALUE_ERROR; } }
    }
}