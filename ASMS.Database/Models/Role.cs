namespace ASMS.Database.Models
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }

        public override void Update(BaseEntity entity)
        {
            if (entity == null) return;
            if (!(entity is Role)) return;

            Role role = (Role)entity;
            RoleName = role.RoleName;
        }
    }
}
