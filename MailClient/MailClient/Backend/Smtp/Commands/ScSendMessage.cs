using System;

namespace MailClient
{
    public class ScSendMessage : SmtpCommand
    {
        enum State
        {
            NotStarted,
            SentInit,
            AcceptedInit,
            SentRecipient,
            AcceptedRecipient,
            SentDataStart,
            AcceptedDataStart,
            SentData,
            AcceptedData,
            Failed
        }

        private State SendState;
        private MailMessage Message;
        public NetEventRetVal OnMessageSent;

        public ScSendMessage(MailMessage Message)
        {
            this.Message = Message;
            if (Message == null) throw new Exception("message_was_nullptr");
            if (!Message.IsOutMessage) throw new Exception("message_is_not_a_sendable");
        }

        internal override string BuildVerb()
        {
            switch (SendState)
            {
                case State.NotStarted:
                    SendState = State.SentInit;
                    return 
                        string.Format(
                            "MAIL FROM <{0}>{1}",
                             ParentService.GetConfig().UserLogin,
                             EOL);

                case State.AcceptedInit:
                    SendState = State.SentRecipient;
                    return
                        string.Format(
                            "RCPT TO <{0}>{1}",
                             string.Join(",", Message.Recipients),
                             EOL);

                case State.AcceptedRecipient:
                    SendState = State.SentDataStart;
                    return "DATA" + EOL;

                case State.AcceptedDataStart:
                    SendState = State.SentData;
                        return string.Format(
                            "From:<{5}>{3}To:<{0}>{3}Subject:{1}{3}{3}{2}{4}",
                            string.Join(", ", Message.Recipients),
                            Message.Subject,
                            Message.Message,
                            EOL,
                            PopCommand.MultilineTerminator,
                            ParentService.GetConfig().UserLogin);

                default:
                    throw new Exception("no_verbs_left");
            }
        }
        internal override int VerbsLeft()
        {
            switch (SendState)
            {
                case State.NotStarted: return 4;
                case State.AcceptedInit: return 3;
                case State.AcceptedRecipient: return 2;
                case State.AcceptedDataStart: return 1;
                default: return 0;
            }
        }

        internal override bool IsMultiline() { return true; }

        internal override bool ParseResponse(string Response)
        {
            bool Success = false;
            switch (SendState)
            {
                case State.SentInit:
                    Success = Response.StartsWith("250");
                    SendState = Success ? State.AcceptedInit : State.Failed;
                    break;

                case State.SentRecipient:
                    Success = Response.StartsWith("250");
                    SendState = Success ? State.AcceptedRecipient : State.Failed;
                    break;

                case State.SentDataStart:
                    Success = Response.StartsWith("354");
                    SendState = Success ? State.AcceptedDataStart : State.Failed;
                    break;

                case State.SentData:
                    Success = Response.StartsWith("250");
                    SendState = Success ? State.AcceptedData : State.Failed;
                    if(Success) OnMessageSent(true);
                    break;
            }

            if (!Success) OnMessageSent(false);
            return Success;
        }
    }
}
