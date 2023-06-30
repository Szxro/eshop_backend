using Eshop_Domain.Common;

namespace Eshop_Domain.Entities.UserEntities;

public class User : AuditableEntity
{
    public User()
    {
        UserShippingInfos = new HashSet<UserShippingInfo>();
        RefreshTokenUsers = new HashSet<RefreshTokenUser>();
        UserSalts = new HashSet<UserSalt>();
        UserCart = new HashSet<UserCart>();
        UserOrders = new HashSet<UserOrders>();
        UserUserRoles = new HashSet<UserUserRoles>();
    }
    public string UserName { get; set; } = null!;

    public string NormalizedUsername { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string NormalizedEmail { get; set; } = null!;

    public DateTime LockOutEnd { get; set; }

    public bool LockOutEnable { get; set; }

    public int AccessFailedCount { get; set; }

    //1:N
    public ICollection<UserOrders> UserOrders { get; set; }

    //1:N
    public ICollection<UserUserRoles> UserUserRoles { get; set; }

    //1:N
    public ICollection<UserShippingInfo> UserShippingInfos { get; set; }

    // N:N
    public ICollection<RefreshTokenUser> RefreshTokenUsers { get; set; }

    // 1:N
    public ICollection<UserSalt> UserSalts { get; set; }

    //1:N
    public ICollection<UserCart> UserCart { get; set; }
}