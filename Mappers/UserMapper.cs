using Library.DTOs.User;
using Library.Models;

namespace Library.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this User userModel)
        {
            return new UserDto
            {
                Id = userModel.Id,
                UserName = userModel.UserName,
                Email = userModel.Email,
                
            };
        }
    }
}
