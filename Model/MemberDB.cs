using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace GymMembers.Model
{
    public class MemberDB : ObservableObject
    {
        private ObservableCollection<Member> members;
        private const string filepath = @"..\..\members.txt";
        private string pattern = "(^\\w+)(\\s+)(\\w+)(\\W+\\s)(.+)";
        private Regex r;

        public MemberDB(ObservableCollection<Member> m)
        {
            members = m;
            
            this.r = new Regex(pattern, RegexOptions.IgnoreCase); 
        }

        public ObservableCollection<Member> GetMemberships()
        {
            try
            {
                StreamReader input = new StreamReader(new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Read));
                string line = String.Empty;

                while ((line = input.ReadLine()) != null)
                {
                    // instantiate a new Member and store it into members collection
                    AddMember(line); 
                }
                input.Close();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("File not found.", "Entry Error");
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid e-mail format");
            }
            return members;
        }

        public void AddMember(string memberData)
        {
            Match m = r.Match(memberData);
            Group g1 = m.Groups[1];
            Group g2 = m.Groups[3];
            Group g3 = m.Groups[5];
            Member mem = new Member
            {
                FirstName = g1.ToString(),
                LastName = g2.ToString(),
                Email = g3.ToString()
            };
            members.Add(mem);
        }

        public void SaveMemberships()
        {
            try
            {
                StreamWriter output = new StreamWriter(new FileStream(filepath, FileMode.Create, FileAccess.Write));
                foreach (Member gymMember in members)
                {
                    output.WriteLine(gymMember.Text); // writing members as strings to the text file
                }
                output.Close();

            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("File not found.", "Entry Error"); 
            }
        }
    }
}
