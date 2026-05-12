using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDemo.Domain.Entities
{
    public interface IBaseEntity
    {
        DateTime DateCreated { get; set; }
        string CreatedBy { get; set; }
        DateTime? DateEdited { get; set; }
        string EditedBy { get; set; }
        DateTime? DateDeleted { get; set; }
        string DeletedBy { get; set; }
        bool Deleted { get; set; }
    }

    public abstract class BaseEntity : IBaseEntity
    {
        private DateTime _DateCreated;

        public virtual DateTime DateCreated
        {
            get { return _DateCreated < DateTime.Parse("1753-01-01") ? DateTime.Now : _DateCreated; }
            set { _DateCreated = value; }
        }

        public virtual string CreatedBy { get; set; } = string.Empty;

        private DateTime? _DateEdited;

        public virtual DateTime? DateEdited
        {
            get { return _DateEdited < DateTime.Parse("1753-01-01") ? DateTime.Now : _DateEdited; }
            set { _DateEdited = value; }
        }

        public virtual string EditedBy { get; set; } = string.Empty;

        private DateTime? _DateDeleted;

        public virtual DateTime? DateDeleted
        {
            get { return _DateDeleted < DateTime.Parse("1753-01-01") ? DateTime.Now : _DateDeleted; }
            set { _DateDeleted = value; }
        }

        public virtual string DeletedBy { get; set; } = string.Empty;

        public virtual bool Deleted { get; set; } = false;
    }
}
