using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleIDE
{
    public partial class FormIde : Form
    {
        public FormIde()
        {
            InitializeComponent();
            if (System.IO.File.Exists(@"c:\temp\BixHub\BixHub_Url.txt"))
                _txtUrl.Text = System.IO.File.ReadAllText(@"c:\temp\BixHub\BixHub_Url.txt");
            if (System.IO.File.Exists(@"c:\temp\BixHub\BixHub_Username.txt"))
                _txtUsername.Text = System.IO.File.ReadAllText(@"c:\temp\BixHub\BixHub_Username.txt");
            if (System.IO.File.Exists(@"c:\temp\BixHub\BixHub_Password.txt"))
                _txtPassword.Text = System.IO.File.ReadAllText(@"c:\temp\BixHub\BixHub_Password.txt");
        }

        private void _btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                BixHubWrapper.IdeCaller caller = new BixHubWrapper.IdeCaller(_txtUrl.Text);
                caller.Login(_txtUsername.Text , _txtPassword.Text);    


                //Abletech.WebApi.Client.Arxivar.Model.AuthenticationTokenRequestDTO arxLogonRequest = new Abletech.WebApi.Client.Arxivar.Model.AuthenticationTokenRequestDTO(_txtUsername.Text, _txtPassword.Text, _txtClientId.Text, _txtClientSecret.Text);
                //var authApi = new Abletech.WebApi.Client.Arxivar.Api.AuthenticationApi(_txtUrl.Text);
                //var resultToken = authApi.AuthenticationGetToken(arxLogonRequest);
                //_authToken = resultToken.AccessToken;

                //_refreshToken = resultToken.RefreshToken;
                //_txtLog.Text = "AuthToken: " + _authToken;
                //tabControl1.Enabled = true;

                //_txtLog.Text += Environment.NewLine;

                //var userApi = new Abletech.WebApi.Client.Arxivar.Api.UsersApi(Configuration);
                //var userInfo = userApi.UsersGetUserInfo();
                //_txtLog.Text += Environment.NewLine + "UserDescription: " + userInfo.CompleteDescription;
                //_txtLog.Text += Environment.NewLine + "UserName: " + userInfo.CompleteName;
                //_txtLog.Text += Environment.NewLine + "UserId: " + userInfo.User;
                //_txtLog.Text += Environment.NewLine;

                //Abletech.WebApi.Client.Arxivar.Api.LicenseApi licenseApi = new Abletech.WebApi.Client.Arxivar.Api.LicenseApi(Configuration);
                //var licenseIsLoaded = licenseApi.LicenseLicenseIsLoaded();
                //_txtLog.Text += Environment.NewLine + "License Is Loaded" + licenseIsLoaded;

                //Abletech.WebApi.Client.Arxivar.Model.ServerLicense loadedLicense = licenseApi.LicenseGetLoadedlicense();
                //_txtLog.Text += Environment.NewLine + "License Issuer: " + loadedLicense.Issuer;
                //_txtLog.Text += Environment.NewLine + "License Type: " + loadedLicense.Purpose;
                //_txtLog.Text += Environment.NewLine + "ActivationCode: " + loadedLicense.ActivationCode;
                //_txtLog.Text += Environment.NewLine + "Note: " + loadedLicense.Note;
            }
            catch (Exception ex)
            {
                _txtLog.Text = ex.Message;
            }
        }
    }
}