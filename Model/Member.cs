using GalaSoft.MvvmLight;
using System;
using System.ComponentModel;


namespace GymMembers.Model
{
    public class Member : ObservableObject, INotifyPropertyChanged
    {
        private string firstName;
        private string lastName;
        private string email;

        public Member() { }

        public Member(string fName, string lName, string mail) 
        {
            firstName = fName;
            lastName = lName;
            email = mail;
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                if (value.Length > 25) 
                { 
                    throw new ArgumentException("Too long"); 
                }
                if (value.Length == 0) 
                { 
                    throw new NullReferenceException(); 
                }
                lastName = value;
                OnPropertyRaised("LastName");
                OnPropertyRaised("Text");
            }
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                if(value.Length == 0){
                    throw new NullReferenceException(); 
                }
                firstName = value;
                OnPropertyRaised("FirstName");
                OnPropertyRaised("Text");
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (value.Length > 25) 
                { 
                    throw new ArgumentException("Too long"); 
                }
                if (value.Length == 0) 
                { 
                    throw new NullReferenceException(); 
                }
                if (value.IndexOf("@") == -1 || value.IndexOf(".") == -1)
                { 
                    throw new FormatException();
                }
                email = value;
                OnPropertyRaised("Email");
                OnPropertyRaised("Text");
            }
        }

        private string _text; 
        public string Text
        {
            get
            {
                return this.FirstName + " " + this.LastName + ", " + this.Email; 
            } 
            set
            {
                _text = value;
                OnPropertyRaised("Text"); 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}
