using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Controls;

namespace ControlCardApp.Classes
{
    internal class ClassHelper
    {
        public static Frame frmObj;

        public static readonly SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionSQL"].ConnectionString);
    }
}
