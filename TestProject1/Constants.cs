using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace TestProject1
{
    public static class Constants
    {
        public static readonly Dictionary<string, string> TestCase1 = new Dictionary<string, string>
        {


                { "firstName", "London" },
                { "lastName", "Chicago" },                
                { "userEmail", "test@gmail.com" },
                { "gender", "Female" },
                { "userNumber", "0987697650" },
                { "dateOfBirthInput", "09 October,2000" },
                { "subjectsInput", "English" },
                { "hobbies", "Music" },
                { "uploadPicture", "uploadPic.jfif" },
                { "currentAddress", "Lorem Ipsum" },
                { "state", "NCR" },
                { "city", "Delhi" },
         };

        public static readonly Dictionary<string, string> TestCase2 = new Dictionary<string, string>
        {


                { "firstName", "London" },
                { "lastName", "Chicago" },
                { "userEmail", "test@gmail.com" },
                { "gender", "Female" },
                { "userNumber", "" },
                { "dateOfBirthInput", "09 Oct,2000" },
                { "subjectsInput", "English" },
                { "hobbies", "Music" },
                { "uploadPicture", "uploadPic.jfif" },
                { "currentAddress", "Lorem Ipsum" },
                { "state", "NCR" },
                { "city", "Delhi" },
         };

    }
}
