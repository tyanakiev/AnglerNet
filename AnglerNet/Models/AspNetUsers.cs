using System;
using System.Collections.Generic;

namespace AnglerNet.Models
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
            AspNetUserTokens = new HashSet<AspNetUserTokens>();
            ConversationUserIdOneNavigation = new HashSet<Conversation>();
            ConversationUserIdTwoNavigation = new HashSet<Conversation>();
            MessageUserIdRecieveNavigation = new HashSet<Message>();
            MessageUserIdSendNavigation = new HashSet<Message>();
            Photo = new HashSet<Photo>();
            Profile = new HashSet<Profile>();
            RelationshipFriend = new HashSet<Relationship>();
            RelationshipUser = new HashSet<Relationship>();
            Wall = new HashSet<Wall>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string User { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        public ICollection<AspNetUserTokens> AspNetUserTokens { get; set; }
        public ICollection<Conversation> ConversationUserIdOneNavigation { get; set; }
        public ICollection<Conversation> ConversationUserIdTwoNavigation { get; set; }
        public ICollection<Message> MessageUserIdRecieveNavigation { get; set; }
        public ICollection<Message> MessageUserIdSendNavigation { get; set; }
        public ICollection<Photo> Photo { get; set; }
        public ICollection<Profile> Profile { get; set; }
        public ICollection<Relationship> RelationshipFriend { get; set; }
        public ICollection<Relationship> RelationshipUser { get; set; }
        public ICollection<Wall> Wall { get; set; }
    }
}
