
namespace ZCheckIn.Backend
{
    public class RegisterUserRequest
    {
        public RegisterUserRequest()
        {
        }

        public string UUID { get; set; }

        public string FaceImageAsBase64 { get; set; }

        public string Name { get; set; }
    }
}
