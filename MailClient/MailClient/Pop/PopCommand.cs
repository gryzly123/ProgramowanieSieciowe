using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient
{
    public abstract class PopCommand
    {
        protected const string EOL = "\r\n";
        protected const string OK = "+OK";
        protected const string ERROR = "-ERR";
        internal  const string MultilineTerminator = "\r\n.\r\n";
        internal  const string Whitespace = " ";

        protected PopService ParentService;
        internal void SetPopService(PopService InService) { ParentService = InService; }

        internal abstract string BuildVerb();
        internal abstract bool ParseResponse(string Response);
        internal abstract int VerbsLeft();
        internal abstract bool IsMultiline();
        internal virtual bool ExpectsResponse() { return true; }
    }
}
