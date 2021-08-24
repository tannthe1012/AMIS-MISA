using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Entity;

namespace MISA.ApplicationCore.Entities
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Required:Attribute
    {

    }
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckDuplicate : Attribute
    {

    }
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : Attribute
    {

    }
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLength:Attribute
    {
        #region property
        public int Value { get; set; }
        #endregion
        #region constructor
        public MaxLength(int length)
        {
            Value = length;  
        }
        #endregion
    }
    public class BaseEntity
    {
        #region Property
        public EntityState EntityState { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        #endregion
    }
}
