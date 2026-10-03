using System;

namespace DbrauResultUI.Helpers
{
    public class CustomPrincipal
    {
        public Int64 UserId { get; set; }
        public string Name { get; set; }        
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string CurrentLocation { get; set; }
        public string[] Roles { get; set; }
        public string ProfilePic { get; set; }
        public string ProfilePicDomain { get; set; }

        //for Live Training
        public long MembershipIdLive { get; set; }
        public DateTime MembershipExpiryLive { get; set; }

        //for self-paced
        public long MembershipId { get; set; }
        public DateTime MembershipExpiry { get; set; }

        public int? BranchID { get; set; }
    }
}