//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace kursach
{
    using System;
    using System.Collections.Generic;
    
    public partial class Movement_goods
    {
        public int ID { get; set; }
        public System.DateTime Date_operation { get; set; }
        public string Type_operation { get; set; }
        public int Product_ID { get; set; }
        public string Quantity { get; set; }
        public int Employee_ID { get; set; }
    
        public virtual Users Users { get; set; }
    }
}
