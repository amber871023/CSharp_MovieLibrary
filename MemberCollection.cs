using System;
using static System.Console;
using N11422807;

namespace Application
{
    public class MemberCollection
    {
        private const int MaxMembers = 100;
        private Member[] members;
        private int count;

        public MemberCollection()
        {
            members = new Member[MaxMembers];
            count = 0;
            // AddMember for testing
            AddMember(new Member("yiting", "cheng", "12345", "1234"));
            AddMember(new Member("et", "cheng", "12345", "1234"));
            AddMember(new Member("annie", "cheng", "12345", "1234"));
            AddMember(new Member("tracy", "chiang", "12345", "1234"));
            AddMember(new Member("injay", "cheng", "12345", "1234"));
            AddMember(new Member("arin", "ning", "12345", "1234"));
            AddMember(new Member("cody", "cheng", "12345", "1234"));
            AddMember(new Member("jimin", "park", "12345", "1234"));
            AddMember(new Member("amber", "cheng", "0412345678", "1234"));
        }
        public int Count
        {
            get { return count; }
        }
        public bool AddMember(Member member)
        {
            // Check if the member already exists
            for (int i = 0; i < count; i++)
            {
                if (members[i].FirstName == member.FirstName && members[i].LastName == member.LastName)
                {
                    Write("The member is already exist. ");
                    return false; // The member already exists
                }
            }
            // Check if the collection is full
            if (count >= members.Length)
            {
                Write("The collection is full. ");
                return false; 
            }

            // Add the new member
            members[count] = member;
            count++;
            return true;
        }
        public bool RemoveMember(Member member)
        {
            for (int i = 0; i < count; i++)
            {
                if (members[i].FirstName == member.FirstName && members[i].LastName == member.LastName)
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

        public Member FindMember(string firstName, string lastName)
        {
            for (int i = 0; i < count; i++)
            {
                if (members[i].FirstName == firstName && members[i].LastName == lastName)
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
        public Member GetMemberAtIndex(int index)
        {
            if (index >= 0 && index < count)
            {
                return members[index];
            }
            else
            {
                return null; // Invalid index
            }
        }
    }
}


