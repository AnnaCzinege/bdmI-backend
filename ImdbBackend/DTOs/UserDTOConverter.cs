using DataAccessLibrary.Models;

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
