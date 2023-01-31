using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace QuizzAppFilRouge.Data.Entities;

public class UserInfo
{
    public int UserInfoId { get; set; }

    [MaxLength(60)]
    public string? FirstName { get; set; }

    [MaxLength(60)]
    public string? LastName { get; set; }
    public IdentityUser IdentityUser { get; set; }



}
