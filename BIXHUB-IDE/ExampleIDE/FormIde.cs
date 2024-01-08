using BixHubWrapper.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Collections.Specialized.BitVector32;

namespace ExampleIDE
{
    public partial class FormIde : Form
    {
        BixHubWrapper.IdeCaller _caller;

        public FormIde()
        {
            InitializeComponent();

            if (System.IO.File.Exists(@"c:\temp\BixHub\BixHub.txt"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SettaggioIniziale));
                using (TextReader reader = new StringReader(System.IO.File.ReadAllText(@"c:\temp\BixHub\BixHub.txt")))
                {
                    var settaggioIniziale = (SettaggioIniziale)new XmlSerializer(typeof(SettaggioIniziale)).Deserialize(reader);
                    _txtUrl.Text = settaggioIniziale.Url;
                    _txtClientGuid.Text = settaggioIniziale.ClientGuid;
                    _txtClientId.Text = settaggioIniziale.ClientId;
                    _txtClientSecret.Text = settaggioIniziale.ClientSecret;
                    _txtMail.Text = settaggioIniziale.Mail;
                    _txtFirstName.Text = settaggioIniziale.FirstName;
                    _txtLastName.Text = settaggioIniziale.LastName;
                    _txtTaxCode.Text = settaggioIniziale.TaxCode;
                    _txtPhoneNumber.Text = settaggioIniziale.PhoneNumber;
                    _txtReturnUrl.Text = settaggioIniziale.ReturnUrl;
                    _txtExternalID.Text = settaggioIniziale.ExternalID;
                }
            }
        }

        private void _btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                _caller = new BixHubWrapper.IdeCaller(_txtUrl.Text);
                
                _caller.Login(_txtClientGuid.Text, _txtClientId.Text, _txtClientSecret.Text);
                AddLogInfo("Login avvenuta con successo: " + _caller.AccessToken);

            }
            catch (Exception ex)
            {
                AddLogError(ex.Message);
            }
        }

        private void _btnCreaSessione_Click(object sender, EventArgs e)
        {
            try
            {
                if (_caller == null)
                    throw new Exception("Fare login");

                CreateSessionResponse createSessionResponse = _caller.CreateNewIdentificationSession(_txtMail.Text, _txtFirstName.Text, _txtLastName.Text, _txtTaxCode.Text, _txtPhoneNumber.Text, _txtReturnUrl.Text, _txtExternalID.Text);
                AddLogInfo("Sessione creata con successo: " + createSessionResponse.SessionUrl);
            }
            catch (Exception ex)
            {
                AddLogError(ex.Message);
            }
        }

        private void _btnSaveSettings_Click(object sender, EventArgs e)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(SettaggioIniziale));
                TextWriter textWriter = new StreamWriter(@"c:\temp\BixHub\BixHub.txt");

                var settaggioIniziale = new SettaggioIniziale
                {
                    Url = _txtUrl.Text,
                    ClientGuid = _txtClientGuid.Text,
                    ClientId = _txtClientId.Text,
                    ClientSecret = _txtClientSecret.Text,
                    Mail = _txtMail.Text,
                    FirstName = _txtFirstName.Text,
                    LastName = _txtLastName.Text,
                    TaxCode = _txtTaxCode.Text,
                    PhoneNumber = _txtPhoneNumber.Text,
                    ReturnUrl = _txtReturnUrl.Text,
                    ExternalID = _txtExternalID.Text,
                };
                xmlSerializer.Serialize(textWriter, settaggioIniziale);
                textWriter.Close();

                AddLogInfo("Salvataggio concluso con successo.");
            }
            catch (Exception ex)
            {
                AddLogError(ex.Message);
            }
        }

        private void AddLog(string log)
        {
            _txtLog.Text += Environment.NewLine + log;
        }

        private void AddLogError(string log)
        {
            log = "ERROR: " + log;
            AddLog(log);
        }

        private void AddLogInfo(string log)
        {
            log = "INFO: " + log;
            AddLog(log);
        }

        private void _btnGetSessionStatus_Click(object sender, EventArgs e)
        {
            try
            {
                if (_caller == null)
                    throw new Exception("Fare login");

                var sessions = _caller.GetSessionList();
                for (int i = 0; i < sessions.Count; i++)
                {

                    AddLogInfo(string.Format("Sessione {0}:", i));
                    AddLogInfo(sessions[i].ToString());
                }

                AddLogInfo("GetSessionList eseguita con successo: ");
            }
            catch (Exception ex)
            {
                AddLogError(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (_caller == null)
                    throw new Exception("Fare login");

                if (string.IsNullOrWhiteSpace(_txtSessionID.Text))
                    throw new Exception("SessionId mancante");

                var sessionId = Guid.Parse(_txtSessionID.Text);
                var session = _caller.GetSession(sessionId);
                AddLogInfo(session.ToString());

                AddLogInfo("GetSession eseguita con successo: ");
            }
            catch (Exception ex)
            {
                AddLogError(ex.ToString());
            }
        }
    }

    [Serializable()]
    public class SettaggioIniziale
    {
        public string Url { get; set; }
        public string ClientGuid { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public string Mail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TaxCode { get; set; }
        public string PhoneNumber { get; set; }
        public string ReturnUrl { get; set; }
        public string ExternalID { get; set; }

    }
}