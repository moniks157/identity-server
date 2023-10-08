using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Shelfie.Identity.DataAccess.Data;

public class AspNetIdentityDbContext : IdentityDbContext
{
    public AspNetIdentityDbContext(DbContextOptions<AspNetIdentityDbContext> options) : base(options)
    {
    }
}
