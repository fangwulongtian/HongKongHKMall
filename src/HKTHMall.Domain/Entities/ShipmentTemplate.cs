//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace HKTHMall.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShipmentTemplate
    {
        public int ShipmentTemplateId { get; set; }
        public long THAreaID { get; set; }
        public Nullable<decimal> Price1 { get; set; }
        public Nullable<decimal> Price2 { get; set; }
        public Nullable<decimal> Price3 { get; set; }
        public Nullable<decimal> Price4 { get; set; }
        public Nullable<decimal> Price5 { get; set; }
        public string CityIds { get; set; }
    }
}
