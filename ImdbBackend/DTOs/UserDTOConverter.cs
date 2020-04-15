using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbBackend.DTOs
{
    public class UserDTOConverter : UserDTO
    { 
        public UserDTO ConvertUserObject(User user)
        {
            UserDTO userDTO = new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
            };

            return userDTO;
        }
    }
}
