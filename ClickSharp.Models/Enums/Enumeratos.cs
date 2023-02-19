using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSharp.Models.Enums
{
    public enum ClaimType
    {
        Id,
        Name,
        Email,
        Role
    }
    public enum PageStatus
    {
        IS_ACTIVE,
        IS_ARCHIVED,
        IS_DELETED,
        IS_DRAFT,
        IS_BACKUP
    }
    public enum UserStatus
    {
        DISABLED,
        IS_ACTIVE,
        DELETED,
        PW_RESET,
        BANNED
    }
}

