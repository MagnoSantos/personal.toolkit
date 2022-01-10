using System;
using System.ComponentModel.DataAnnotations;

namespace Personal.Commom
{
    public class Entity
    {
        public Entity()
        {
            CreateAt = DateTime.UtcNow;
            CreateBy = CreatorHelper.GetCreator;
        }

        public DateTime? CreateAt { get; set; }
        public string CreateBy { get; set; }

        [Key]
        public Guid Id { get; } = Guid.NewGuid();
    }
}