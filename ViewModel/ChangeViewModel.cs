using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GymMembers.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace GymMembers.ViewModel
{
    public class ChangeViewModel : ViewModelBase
    {
        private string enteredFName;
        private string enteredLName;
        private string enteredEmail;

        public ChangeViewModel() 
        { 
            UpdateCommand = new RelayCommand<IClosable>(UpdateMethod);
            DeleteCommand = new RelayCommand<IClosable>(DeleteMethod);
            Messenger.Default.Register<Member>(this, GetSelected);
        }

        public ICommand UpdateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public void GetSelected(Member m)
        {
            EnteredFName = m.FirstName;
            EnteredLName = m.LastName;
            EnteredEmail = m.Email;
        }

        public string EnteredFName
        {
            get
            {
                return enteredFName;
            }
            set
            {
                enteredFName = value;
                RaisePropertyChanged("EnteredFName");
            }
        }

        public string EnteredLName
        {
            get
            {
                return enteredLName;
            }
            set
            {
                enteredLName = value;
                RaisePropertyChanged("EnteredLName");
            }
        }

        public string EnteredEmail
        {
            get
            {
                return enteredEmail;
            }
            set
            {
                enteredEmail = value;
                RaisePropertyChanged("EnteredEmail");
            }
        }

        public void UpdateMethod(IClosable window) 
        {
            try
            {
                if (window != null)
                {
                    var updatedMember = new MessageMember(enteredFName, enteredLName, enteredEmail, "Update")
                    {
                        FirstName = enteredFName,
                        LastName = enteredLName,
                        Email = enteredEmail
                    };
                    Messenger.Default.Send(updatedMember);
                    window.Close();
                } 
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Fields must be under 25 characters.", "Entry Error");
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Fields cannot be empty.", "Entry Error");
            }
            catch (FormatException)
            {
                MessageBox.Show("Must be a valid e-mail address.", "Entry Error");
            }
        }

        public void DeleteMethod(IClosable window) 
        { 
            if (window != null) 
            { 
                Messenger.Default.Send(new NotificationMessage("Delete")); 
                window.Close();
            } 
        }
    }
}
