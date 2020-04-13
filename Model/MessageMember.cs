namespace GymMembers.Model
{
    public class MessageMember : Member
    {
        private string _message; 
        public MessageMember(string fName, string lName, string mail, string message) : base(fName, lName, mail) 
        { 
            Message = message; 
        }
        public string Message
        {
            get
            {
                return _message; 
            } 
            set
            {
                _message = value; 
            }
        }

    }
}
