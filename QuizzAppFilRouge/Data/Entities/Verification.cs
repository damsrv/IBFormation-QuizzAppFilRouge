using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace QuizzAppFilRouge.Data.Entities
{
    [PrimaryKey(nameof(QuestionId), nameof(IdentityUserId), nameof(ResponseId))]
    public class Verification
    {
        // 3 Clés composites
        public int QuestionId { get; set; }
        public string IdentityUserId { get; set; }

        public int ResponseId { get; set; }

        // 3 Foreign Key
        public IdentityUser IdentityUser { get; set; }

        public Question Question { get; set; }

        public Response Response { get; set; }


    }
}
