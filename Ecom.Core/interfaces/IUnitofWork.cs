﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.interfaces
{
    public interface IUnitofWork
    {
        public ICategoryRepositry CategoryRepositry { get; }
        public IProductRepositry ProductRepositry { get; }
        public IPhotoRepositry PhotoRepositry { get; }
    }
}
