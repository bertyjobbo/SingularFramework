﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Core.Authentication
{
    public interface IUserFactory
    {
        SingularUser Build(string authenticationType, string logonName);
    }
}