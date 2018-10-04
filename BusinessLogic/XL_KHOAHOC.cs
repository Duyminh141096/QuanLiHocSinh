using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
namespace BusinessLogic
{
    public class XL_KHOAHOC : XL_BANG
    {
        public XL_KHOAHOC() : base("XL_KHOAHOC") { }
        public XL_KHOAHOC(string chuoiSQL) : base("XL_KHOAHOC", chuoiSQL) { }
    }
}
