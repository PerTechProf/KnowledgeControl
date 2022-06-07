using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace KnowledgeControl
{
    public class Options
    {
        public const string AuthCookie = "authToken";
        public const string IsEmployerCookie = "isEmployer";

        public static SymmetricSecurityKey SecurityKey =>
            new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes("knowledge_control_security"));
    }
}