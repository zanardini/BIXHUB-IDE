using BixHubWrapper.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

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

                CreateSessionResponse createSessionResponse = _caller.CreateNewIdentificationSession(_txtMail.Text, _txtTaxCode.Text, _txtPhoneNumber.Text, _txtReturnUrl.Text, _txtExternalID.Text, _cbSendMail.Checked);
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

                LoadData(sessions);

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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (_caller == null)
                    throw new Exception("Fare login");

                var sessionId = Guid.Parse(_txtSessionID.Text);

                var session = _caller.GetSession(sessionId);
                if (session.SessionStatus != "Completed")
                {
                    AddLogInfo("Sessione di identificazione non ancora completata");
                    return;
                }

                using (var folderBrowserDialog1 = new FolderBrowserDialog())
                {


                    DialogResult result = folderBrowserDialog1.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        string folderName = folderBrowserDialog1.SelectedPath;

                        var acquiredIDInfoResponse = _caller.GetIdentificationEvidenceBySessionGuid(sessionId);
                        string fullname = Directory.CreateDirectory(Path.Combine(folderName, sessionId.ToString())).FullName;

                        // Se AI
                        if (acquiredIDInfoResponse.IDCard.FrontFileData != null)
                        {
                            File.WriteAllBytes(Path.Combine(fullname, "IDCard_Front.jpeg"), acquiredIDInfoResponse.IDCard.FrontFileData);
                        }
                        if (acquiredIDInfoResponse.IDCard.RearFileData != null)
                        {
                            File.WriteAllBytes(Path.Combine(fullname, "IDCard_Rear.jpeg"), acquiredIDInfoResponse.IDCard.RearFileData);
                        }
                        if (acquiredIDInfoResponse.Selfie.FileData != null)
                        {
                            File.WriteAllBytes(Path.Combine(fullname, "Selfie.jpeg"), acquiredIDInfoResponse.Selfie.FileData);
                        }
                        if (acquiredIDInfoResponse.LivenessDetection.FileData != null)
                        {
                            File.WriteAllBytes(Path.Combine(fullname, "Liveness.mp4"), acquiredIDInfoResponse.LivenessDetection.FileData);
                        }

                        // Se SPID/CIE
                        if (acquiredIDInfoResponse.DigitalIdentity != null)
                        {
                            File.WriteAllText(Path.Combine(fullname, "Request.xml"), acquiredIDInfoResponse.DigitalIdentity.Request);
                        }
                        if (acquiredIDInfoResponse.DigitalIdentity != null)
                        {
                            File.WriteAllText(Path.Combine(fullname, "Response.xml"), acquiredIDInfoResponse.DigitalIdentity.Response);
                        }

                        var auditLog = _caller.GetAuditLogBySessionGuid(sessionId);
                        File.WriteAllBytes(Path.Combine(fullname, "auditLog.pdf"), auditLog);

                        AddLogInfo("Download evidenze identificazione eseguito sul path: " + fullname);
                        System.Diagnostics.Process.Start("explorer.exe", fullname);
                    }
                }
            }
            catch (Exception ex)
            {
                AddLogError(ex.Message);
            }
        }

        private void LoadData(List<BixHubWrapper.Model.InfoSessionResponse> sessions)
        {
            sessionData.DataSource = null;
            sessionData.Columns.Clear();
            sessionData.DataSource = sessions;
            sessionData.Columns.Add(new DataGridViewButtonColumn() { Text = "Apri sessione", UseColumnTextForButtonValue = true });
        }

        private void sessionData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                var value = senderGrid.Rows[e.RowIndex].Cells["SessionGuid"].Value.ToString();
                var url = "https://idedemo.bixhub.it/OnBoarding.FE/start/" + value; //DEMO
                System.Diagnostics.Process.Start(url);
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
        public string TaxCode { get; set; }
        public string PhoneNumber { get; set; }
        public string ReturnUrl { get; set; }
        public string ExternalID { get; set; }

    }
}