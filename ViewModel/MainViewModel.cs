using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GymMembers.Model;
using GymMembers.View;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace GymMembers.ViewModel
{
   
    public class MainViewModel : ViewModelBase
    {
        private readonly ObservableCollection<Member> members;
        private Member selectedMember;
        private MemberDB database;

        public MainViewModel()
        {
            members = new ObservableCollection<Member>();
            database = new MemberDB(members); 
            members = database.GetMemberships();
            AddCommand = new RelayCommand(AddMethod);
            ExitCommand = new RelayCommand<IClosable>(ExitMethod);
            ChangeCommand = new RelayCommand(ChangeMethod); 

            Messenger.Default.Register<MessageMember>(this, ReceiveMember); 
            Messenger.Default.Register<NotificationMessage>(this, ReceiveMessage);
        }

        public ICommand AddCommand { get; private set; }
        public RelayCommand<IClosable> ExitCommand { get; private set; }
        public ICommand ChangeCommand { get; private set; }

        public Member SelectedMember 
        { 
            get 
            { 
                return selectedMember; 
            } 
            set 
            { 
                selectedMember = value; 
                RaisePropertyChanged("SelectedMember"); 
            } 
        }

        public void AddMethod() 
        { 
            AddWindow add = new AddWindow(); 
            add.Show(); 
        }

        public void ExitMethod(IClosable window)
        {
            if (window != null)
            {
                window.Close();
            }
        }

        public void ChangeMethod() 
        { 
            if (SelectedMember != null) 
            { 
                ChangeWindow change = new ChangeWindow(); 
                change.Show(); 
                Messenger.Default.Send(this.SelectedMember); 
            } else
            {
                MessageBox.Show("SelectedMember is null"); 
            } 

        }

        public void ReceiveMember(MessageMember m) 
        { 
            if (m.Message == "Update") 
            { 
                for(int i = 0; i < this.MemberList.Count; i++){
                    if(m.FirstName.Equals(this.MemberList[i].FirstName) || m.LastName.Equals(this.MemberList[i].LastName) || m.Email.Equals(this.MemberList[i].Email)) {
                        this.MemberList[i].FirstName = m.FirstName; 
                        this.MemberList[i].LastName = m.LastName; 
                        this.MemberList[i].Email = m.Email;
                    }
                }
                database.SaveMemberships();

            } else if (m.Message == "Add") 
            {
                members.Add(m);
                database.SaveMemberships();
            } 
        }

        public void ReceiveMessage(NotificationMessage msg)
        { 
            if (msg.Notification.Equals("Delete"))
            {
                members.Remove(this.selectedMember);
                database.SaveMemberships(); 
            }
        }

        public ObservableCollection<Member> MemberList 
        { 
            get 
            { 
                return members; 
            } 
        }
    }
}