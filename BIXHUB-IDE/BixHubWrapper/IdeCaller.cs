using IO.Swagger.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BixHubWrapper
{
    public class IdeCaller
    {
        private readonly string _url;
        public string AccessToken { get { return _accessToken; } }
        private string _accessToken;

        public IdeCaller(string url)
        {
            _url = url;
        }

        public IO.Swagger.Client.Configuration Configuration
        {
            get
            {
                {
                    return new IO.Swagger.Client.Configuration()
                    {
                        AccessToken = _accessToken,
                        BasePath = _url + "/OnBoardingService",
                        ApiKey = new Dictionary<string, string>() { { "Authorization", _accessToken } },
                        ApiKeyPrefix = new Dictionary<string, string>() { { "Authorization", "Bearer" } }
                    };
                }
            }
        }

        public async void Login(string clientGuid, string clientId, string clientSecret)
        {
            HttpClient client = new HttpClient();
            var request = new
            {
                client_Id = clientId,
                client_secret = clientSecret,
                grant_Type = "client_credentials"
            };
            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var task = Task.Run(() => client.PostAsync($"{_url}/AuthorizationService/api/{clientGuid}/oauth/Token", content));
            task.Wait();
            var response = task.Result;

            //var response = await client.PostAsync($"{_url}/AuthorizationService/api/{clientGuid}/oauth/Token", content);
            string json = await response.Content.ReadAsStringAsync();
            var accessTokenDto = JsonConvert.DeserializeObject<BixHubWrapper.Model.TokenFromUserResult>(json);
            _accessToken = accessTokenDto.access_token;
        }

        public BixHubWrapper.Model.CreateSessionResponse CreateNewIdentificationSession(string email, string taxCode, string phoneNumber, string returnUrl, string externalID, bool sendMail)
        {
            // declaredPersonalData contiene i dati dell'identificato che l'applicazione terza dichiara
            var declaredPersonalData = new IO.Swagger.Model.DeclaredPersonalData(taxCode, phoneNumber, email);
            
            var attributes = new Dictionary<string, string> { 
            };
            // parameters sono i parametri di funzionamento del servizio quali la lingua utente nonchè la URL di redirect a fine sessione
            var parameters = new Dictionary<string, string> {
                { "returnUrl", returnUrl },
                { "language", "it" }
            };
            // è possibile indicare metadati da salvare lato BIX-IDE per poi favorire il match
            var metadata = new Dictionary<string, string> { 
                { "externalID", externalID } 
            };

            var sessionFlowType = IO.Swagger.Model.SessionFlowTypeDto.Dynamic;
            IO.Swagger.Api.SessionLifeCycleApi sessionLifeCycleApi = new IO.Swagger.Api.SessionLifeCycleApi(Configuration);
            IO.Swagger.Model.CreateSessionRequest body = new IO.Swagger.Model.CreateSessionRequest(parameters, metadata, attributes, declaredPersonalData, sessionFlowType);
            var response = sessionLifeCycleApi.ApiV1SessionLifeCycleCreatePost(body);
            if (sendMail)
                sessionLifeCycleApi.ApiV1SessionLifeCycleSendEmailSessionGuidPost(response.SessionGuid, new SendEmailRequest());

            return new BixHubWrapper.Model.CreateSessionResponse
            {
                SessionGuid = response.SessionGuid,
                SessionUrl = response.SessionUrl
            };
        }

        public void DeleteIdentificationSession(Guid sessionGuid)
        {
            IO.Swagger.Api.SessionLifeCycleApi sessionLifeCycleApi = new IO.Swagger.Api.SessionLifeCycleApi(Configuration);
            sessionLifeCycleApi.ApiV1SessionLifeCycleDeleteSessionGuidDelete(sessionGuid);
        }

        public BixHubWrapper.Model.InfoSessionResponse GetSession(Guid sessionGuid)
        {
            IO.Swagger.Api.SessionLifeCycleApi sessionLifeCycleApi = new IO.Swagger.Api.SessionLifeCycleApi(Configuration);
            IO.Swagger.Model.GetSessionResponse sessionModel = sessionLifeCycleApi.ApiV1SessionLifeCycleGetSessionGuidGet(sessionGuid);
            List<IO.Swagger.Model.GetStatusSessionResponse> statusModelList = sessionLifeCycleApi.ApiV1SessionLifeCycleGetStatusPost(new List<Guid?> { sessionGuid });
            IO.Swagger.Model.GetStatusSessionResponse statusModel = statusModelList.FirstOrDefault();
            var result = InfoSessionResponseTranslate(sessionModel, statusModel);
            return result;

        }

        public List<BixHubWrapper.Model.InfoSessionResponse> GetSessionList()
        {
            IO.Swagger.Api.SessionLifeCycleApi sessionLifeCycleApi = new IO.Swagger.Api.SessionLifeCycleApi(Configuration);
            IO.Swagger.Model.GetAllSessionRequest request = new IO.Swagger.Model.GetAllSessionRequest(0, 100);
            var sessionResponse = sessionLifeCycleApi.ApiV1SessionLifeCycleGetAllPost(request);
            var result = new List<BixHubWrapper.Model.InfoSessionResponse>();
            if (sessionResponse == null)
                return result;
            if (sessionResponse.Sessions == null)
                return result;
            foreach (IO.Swagger.Model.GetSessionResponse session in sessionResponse.Sessions)
                result.Add(InfoSessionResponseTranslate(session, null));
            return result;
        }

        private BixHubWrapper.Model.InfoSessionResponse InfoSessionResponseTranslate(IO.Swagger.Model.GetSessionResponse sessionModel, IO.Swagger.Model.GetStatusSessionResponse statusModel)
        {
            if (sessionModel == null)
                return null;

            BixHubWrapper.Model.InfoSessionResponse result = new BixHubWrapper.Model.InfoSessionResponse();
            if (sessionModel.SessionGuid != null)
                result.SessionGuid = sessionModel.SessionGuid.Value;
            if (sessionModel.CreatedDate != null)
                result.CreatedDate = sessionModel.CreatedDate.Value;
            if (sessionModel.UpdatedDate != null)
                result.UpdatedDate = sessionModel.UpdatedDate.Value;

            if (sessionModel.Metadata != null)
            {
                var f = sessionModel.Metadata.FirstOrDefault(x => x.Key == "externalID");
                if (f.Key != null)
                    result.ExternalID = f.Value;
            }
            if (statusModel != null)
            {
                result.SessionStatus = statusModel.SessionStatus.ToString();
                result.IdentificationStatus = statusModel.IdentificationStatus.ToString();
                result.CompletedDate = statusModel.CompletedDate;
                result.RiskScore = statusModel.RiskScore;
                result.RiskScoreEvaluation = statusModel.RiskScoreEvaluation;             

            }
            return result;
        }

        public BixHubWrapper.Model.AcquiredIDInfoResponse GetIdentificationEvidenceBySessionGuid(Guid sessionGuid)
        {
            IO.Swagger.Api.SessionLifeCycleApi sessionLifeCycleApi = new IO.Swagger.Api.SessionLifeCycleApi(Configuration);
            var allEvidenceResponse = sessionLifeCycleApi.ApiV1SessionLifeCycleGetAcquiredIDInfoSessionGuidPost(sessionGuid, new GetAcquiredIDInfoRequest()
            {
                WithDigitalIdentityReqResp = true,
                WithIDCard = true,
                WithLivenessDetection = true,
                WithPersonalData = true,
                WithSelfie = true
            });

            BixHubWrapper.Model.AcquiredIDInfoResponse responseWrapper = JsonConvert.DeserializeObject<BixHubWrapper.Model.AcquiredIDInfoResponse>(JsonConvert.SerializeObject(allEvidenceResponse));

            return responseWrapper;

        }

        public byte[] GetAuditLogBySessionGuid(Guid sessionGuid)
        {
            IO.Swagger.Api.SessionLifeCycleApi sessionLifeCycleApi = new IO.Swagger.Api.SessionLifeCycleApi(Configuration);
            var response = sessionLifeCycleApi.ApiV1SessionLifeCycleGetAuditLogSessionGuidGet(sessionGuid);
            return response;
        }

    }
}