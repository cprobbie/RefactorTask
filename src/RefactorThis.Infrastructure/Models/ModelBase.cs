﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RefactorThis.Infrastructure.Models
{
    public class ModelBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}