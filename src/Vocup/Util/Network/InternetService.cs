using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Vocup.Util.Network
{
    public class InternetService
    {
        public void Start()
        {
            HttpWebRequest request = WebRequest.CreateHttp("https://vectordata.de/vocup/v1/");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        }
    }
}
