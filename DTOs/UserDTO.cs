
namespace ZCheckIn.Backend.DTOs
{
    public class UserDTO
    {
        public UserDTO()
        {
        }

        public string UUID { get; set; }

        public string FaceImageAsBase64 { get; set; }

        public string Name { get; set; }
    }
}
