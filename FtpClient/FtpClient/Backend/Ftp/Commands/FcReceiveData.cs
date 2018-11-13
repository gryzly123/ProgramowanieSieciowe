namespace FtpClient
{
    public class FcReceiveData : FtpCommand
    {
        bool DataReceived = false;

        public FcReceiveData(ref byte[] DataOut) { }

        internal override string BuildVerb() { return EOL; }
        internal override bool OmmitVerb() { return true; }
        internal override int VerbsLeft() { return DataReceived ? 0 : 1; }
        internal override bool IsMultiline() { return true; }
        internal override bool ExpectsResponse() { return true; }

        internal override bool ParseResponse(string Response)
        {
            System.Windows.Forms.MessageBox.Show(Response);
            DataReceived = true;
            return false;
        }
    }
}
