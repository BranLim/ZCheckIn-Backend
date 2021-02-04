using System;


namespace check_in_user_registration
{
    public class RegisterUserRequest
    {
        public RegisterUserRequest()
        {
        }

        public string UUID { get; set; }

        public string FaceImageAsBase64 { get; set; }
    }
}
