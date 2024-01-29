using IO.Swagger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace BixHubWrapper.Model
{
    public class InfoSessionResponse
    {
        public Guid SessionGuid { get; set; }
        public string ExternalID { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string FlowType { get; set; }
        public string IdentificationStatus { get; set; }
        public string SessionStatus { get; set; }
        public float? RiskScore { get; set; }
        public string RiskScoreEvaluation { get; set; }

        public override string ToString()
        {
            var result = string.Empty;
            result += string.Format("  SessionGuid: {0}", SessionGuid);
            result += Environment.NewLine;
            result += string.Format("  ExternalID: {0}", ExternalID);
            result += Environment.NewLine;
            result += string.Format("  CreatedDate: {0}", CreatedDate);
            result += Environment.NewLine;
            result += string.Format("  UpdatedDate: {0}", UpdatedDate);
            result += Environment.NewLine;
            result += string.Format("  CompletedDate: {0}", CompletedDate);
            result += Environment.NewLine;
            result += string.Format("  FlowType: {0}", FlowType);
            result += Environment.NewLine;
            result += string.Format("  IdentificationStatus: {0}", IdentificationStatus);
            result += Environment.NewLine;
            result += string.Format("  SessionStatus: {0}", SessionStatus);
            result += Environment.NewLine;
            result += string.Format("  RiskScore: {0}", RiskScore == null ? "null" : RiskScore.ToString());
            result += Environment.NewLine;
            result += string.Format("  RiskScoreEvaluation: {0}", RiskScoreEvaluation);
            return result;
        }
    }
}