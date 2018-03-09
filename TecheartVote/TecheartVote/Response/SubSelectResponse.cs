using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecheartVote.Verification;

namespace TecheartVote.Response
{
    public class SubSelectResponse
    {
        public static SubSelect GetSubDate(byte[] date, HandshakeResponse handresponse)
        {
            SubSelect subdate = new SubSelect();
            if (!VerificationTools.HashCheck(date.ToList()))
            {
                return null;
            }
            var decr = Cryptogram.Decrypt(date, handresponse);

            return AnalysisSubSelect(date);
        }
        public static SubSelect AnalysisSubSelect(byte[] text)
        {
            SubSelect subdate = new SubSelect();
            return subdate;
        }
    }
}
