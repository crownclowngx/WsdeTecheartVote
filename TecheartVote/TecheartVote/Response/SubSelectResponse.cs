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

            return AnalysisSubSelect(decr);
        }
        public static SubSelect AnalysisSubSelect(byte[] text)
        {
            SubSelect subdate = new SubSelect();
            byte[] addresss = new byte[4];
            addresss[0] = text[0];
            addresss[1] = text[1];
            addresss[2] = text[2];
            addresss[3] = text[3];
            var k=SubVoteDisplayAction.AnalysisSubAddress(addresss);
            var k2=SubVoteDisplayAction.AnalysisSubDisplaySign(text[5]);
            byte[] selectes = new byte[8];
            selectes[0] = text[6];
            selectes[1] = text[7];
            selectes[2] = text[8];
            selectes[3] = text[9];
            selectes[4] = text[10];
            selectes[5] = text[11];
            selectes[6] = text[12];
            selectes[7] = text[13];
            var k3=SubVoteDisplayAction.AnalysisDisplayData(k2,selectes);

            subdate.address = k;
            subdate.selectDate = k3;
            subdate.number = text[4];
            return subdate;
        }
    }
}
