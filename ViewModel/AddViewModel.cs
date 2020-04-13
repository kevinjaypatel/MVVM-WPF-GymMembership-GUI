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
    public class AddViewModel : ViewModelBase
    {
        // gym member attributes
        private string enteredFName;
        private string enteredLName;
        private string enteredEmail;
        

        public AddViewModel() 
        {     
            SaveCommand = new RelayCommand<IClosable>(SaveMethod); 
            CancelCommand = new RelayCommand<IClosable>(CancelMethod);          
        }

        public RelayCommand<IClosable> SaveCommand { get; private set; }
        public RelayCommand<IClosable> CancelCommand { get; private set; }

        public void SaveMethod(IClosable window) 
        { 
            try 
            { 
                if (window != null) 
                { 
                    var newMember = new MessageMember(enteredFName, enteredLName, enteredEmail, "Add")
                    {
                        FirstName = enteredFName, 
                        LastName = enteredLName,
                        Email = enteredEmail 
                    }; 
                    Messenger.Default.Send(newMember); 
                    window.Close();
                    ResetProperties(); 
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

        public void ResetProperties()
        {
            EnteredFName = "";
            EnteredLName = "";
            EnteredEmail = "";
        }

        public void CancelMethod(IClosable window) 
        { 
            if (window != null) 
            { 
                window.Close(); 
            }
            ResetProperties(); 
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
    }
}
