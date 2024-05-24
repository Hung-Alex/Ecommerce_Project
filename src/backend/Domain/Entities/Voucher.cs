using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Voucher : BaseEntity, IDatedModification
    {
        private Voucher() : base()
        {
           
        }
       
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int Discountt { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        //mapping ProductVouchersMap
        public IList<ProductVouchers> ProductVouchersMaps { get; set; }
        //mapping Order
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        //mapping categoryvoucher map
        public IList<CategoryVouchers> CategoryVouchersMaps { get; set; }
    }
}
