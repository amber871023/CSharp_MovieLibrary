using System;
using static System.Console;
using N11422807;

namespace Application
{
    public class MemberCollection
    {
        private const int MaxMembers = 1000;
        private Member[] members;
        private int count;

        public MemberCollection()
        {
            members = new Member[MaxMembers];
            count = 0;
        }
        public bool AddMember(Member member)
        {
            if (count >= members.Length)
            {
                WriteLine("The collection is full.");
                return false; // The collection is full
            }

            // Check if the member already exists
            for (int i = 0; i < count; i++)
            {
                if (members[i].FirstName == member.FirstName && members[i].LastName == member.LastName)
                {
                    return false; // The member already exists
                }
            }

            // Add the new member
            members[count] = member;
            count++;
            return true;
        }
        public bool RemoveMember(string contactNumber)
        {
            for (int i = 0; i < count; i++)
            {
                if (members[i].ContactNumber == contactNumber)
                {
                    // Shift elements to fill the gap
                    for (int j = i; j < count - 1; j++)
                    {
                        members[j] = members[j + 1];
                    }
                    members[count - 1] = null; // Clear the last element
                    count--;
                    return true; // Member removed successfully
                }
            }
            return false; // Member not found
        }

        public Member FindMember(string contactNumber)
        {
            for (int i = 0; i < count; i++)
            {
                if (members[i].ContactNumber == contactNumber)
                {
                    return members[i];
                }
            }
            return null; // Member not found
        }
        public bool MemberExists(string firstName, string lastName)
        {
            foreach (Member member in members)
            {
                if (member != null && member.FirstName == firstName && member.LastName == lastName)
                {
                    return true;
                }
            }
            return false;
        }

        public string GetMemberPassword(string firstName, string lastName)
        {
            foreach (Member member in members)
            {
                if (member != null && member.FirstName == firstName && member.LastName == lastName)
                {
                    return member.Password;
                }
            }
            return null; // Member not found
        }
    }
}


