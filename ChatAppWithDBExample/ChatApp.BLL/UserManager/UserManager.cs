using ChatAppWithDBExample.ChatApp.DAL.UserRepository;
using ChatAppWithDBExample.ChatApp.Model;
using ChatAppWithDBExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatAppWithDBExample.ChatApp.BLL.UserManager
{
    public class UserManager
    {
        UserRepository userRepository = new UserRepository();
        public ChatBoxModel GetChatbox(int toUserId)
        {
            //var re= userRepository.GetChatbox(toUserId);
            //var a = re;
            return userRepository.GetChatbox(toUserId);
        }

        public int DeleteMessage(int messageId)
        {
            return userRepository.DeleteMessage(messageId);
        }
        public bool SendMessage(int toUserId, string message)
        {
            return userRepository.SendMessage(toUserId, message);
        }

        public List<MessageDTO> LazyLoadMssages(int toUserId, int skip)
        {
            return userRepository.LazyLoadMssages(toUserId, skip);
        }

        public bool ExistingUser(User _user)
        {
            return userRepository.ExistingUser(_user);
        }
        
        public int AddUserConnection(Guid ConnectionId)
        {
            return userRepository.AddUserConnection(ConnectionId);
        }
        public int RemoveUserConnection(Guid ConnectionId)
        {
            return userRepository.RemoveUserConnection(ConnectionId);
        }
        public List<UserDTO> GetUsersToChat()
        {
            return userRepository.GetUsersToChat();
        }
        public IList<string> GetUSerConnections(int uSerId)
        {
            return userRepository.GetUSerConnections(uSerId);
        }
    }
}