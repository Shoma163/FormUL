﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseConnectionLib
{
    public class ClassStudent
    {
        public ClassStudent(int className)
        {
            ClassName = className;
        }

        public int ClassName { get; set; }
    }
}