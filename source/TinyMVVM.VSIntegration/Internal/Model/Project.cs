﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.TinyMVVM_VSIntegration.Internal.Model
{
    public class Project : Folder
    {
        public virtual string Type { get; set; }

        public Project()
        {
        }
    }
}