using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OneTransitAPI.Data;
using System.Net.Mail;

namespace OneTransitAPI
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DatabaseDataContext db = new DatabaseDataContext();

            Consumer data = new Consumer();
            data.ConsumerKey = Guid.NewGuid();
            data.EmailAddress = this.txtEmailAddress.Text;
            data.CreatedDate = DateTime.UtcNow;

            db.Consumers.InsertOnSubmit(data);

            db.SubmitChanges();

            MailMessage message = new MailMessage();
            message.From = new MailAddress("system@onetransitapi.com");
            message.To.Add(new MailAddress(this.txtEmailAddress.Text));
            message.Subject = "Your OneTransit API Key";
            message.Body = "Thank you for registering for the OneTransit API. We look forward to seeing the unique and innovative applications that developers like you create with our service. Your API key is listed below. Please let us know if you have any problems.\n\n" + data.ConsumerKey.ToString() + "\n\n--\nOneTransit\nwww.onetransitapi.com\n";

            SmtpClient client = new SmtpClient();
            client.Send(message);

            Response.Redirect("~/Default.aspx");
        }
    }
}