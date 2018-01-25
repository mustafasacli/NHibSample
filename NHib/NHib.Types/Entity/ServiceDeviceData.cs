using NHib.Core.Entity;
using NHibernate.Mapping.Attributes;
using System;

namespace NHib.Types.Entity
{
    [Class(Table = "ServiceDeviceData", NameType = typeof(ServiceDeviceData))]
    public class ServiceDeviceData : IEntity
    {
        [Id(Name = "Id")]
        [Generator(1, Class = "increment")]
        public virtual int Id { get; set; }

        [Property(Name = "DeviceCode", Column = "DeviceCode", NotNull = true)]
        public virtual string DeviceCode { get; set; }

        [Property(Name = "DeviceData", Column = "DeviceData", NotNull = true)]
        public virtual string DeviceData { get; set; }

        [Property(Name = "CreationTime", Column = "CreationTime", NotNull = true)]
        public virtual DateTime CreationTime { get; set; } = DateTime.Now;

        [Property(Name = "DeviceAdress", Column = "DeviceAdress", NotNull = true)]
        public virtual string DeviceAdress { get; set; }

        [Property(Name = "ResponseAdress", Column = "ResponseAdress", NotNull = true)]
        public virtual string ResponseAdress { get; set; }

        [Property(Name = "ReadTime", Column = "ReadTime")]
        public virtual DateTime? ReadTime { get; set; }

        [Property(Name = "ReadStatus", Column = "ReadStatus", NotNull = true)]
        public virtual bool ReadStatus { get; set; }
    }
}
