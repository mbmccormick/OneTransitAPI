using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OneTransitAPI.Data;
using System.Text;

namespace OneTransitAPI
{
    public partial class Logging : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DatabaseDataContext db = new DatabaseDataContext();

            StringBuilder output = new StringBuilder();

            foreach (Information i in db.Informations.OrderByDescending(z => z.CreatedDate).Take(1000))
            {
                if (i.Message.ToLower().Contains("exception"))
                    output.AppendLine("<br /><span style='color: red;'>");
                else if (i.Message.ToLower().Contains("complete"))
                    output.AppendLine("<br /><span style='color: green;'>");

                output.AppendLine(i.Message.Replace("[BackgroundWorker]", "[" + i.CreatedDate.ToString("MM/dd/yyyy H:mm:ss") + "]") + "<br />");

                if (i.Message.ToLower().Contains("exception"))
                    output.AppendLine("</span><br />");
                else if (i.Message.ToLower().Contains("complete"))
                    output.AppendLine("</span>");
            }

            this.txtInformation.Text = output.ToString();
        }
    }
}