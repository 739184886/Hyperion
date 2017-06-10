﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperion.Core.BL
{
    using Poseidon.Base.Framework;
    using Hyperion.Core.DL;
    using Hyperion.Core.IDAL;

    /// <summary>
    /// 厂家业务类
    /// </summary>
    public class VendorBusiness : AbstractBusiness<Vendor, string>, IBaseBL<Vendor, string>
    {
        #region Constructor
        /// <summary>
        /// 厂家业务类
        /// </summary>
        public VendorBusiness()
        {
            this.baseDal = RepositoryFactory<IVendorRepository>.Instance;
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 按名称查找厂家
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public Vendor FindByName(string name)
        {
            return this.baseDal.FindOneByField("name", name);
        }
        #endregion //Method
    }
}
