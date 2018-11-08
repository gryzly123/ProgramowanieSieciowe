using System;

namespace FtpClient
{
    public class FcAuthorize : FtpCommand
    {
        enum AuthorizationState
        {
            NotStarted,
            SentLogin,
            AcceptedLogin,
            SentPassword,
            AcceptedPassword,
            Failed
        } private AuthorizationState AuthState;

        public NetEvent OnUserLoginSuccess;
        public NetEvent OnUserLoginFailed;
        FtpConnectionSettings Creds;

        public FcAuthorize()
        {
            AuthState = AuthorizationState.NotStarted;
        }

        internal override string BuildVerb()
        {
            Creds = ParentService.GetConfig();

            switch (AuthState)
            {
                case AuthorizationState.NotStarted:
                    AuthState = AuthorizationState.SentLogin;
                    return "USER " + Creds.UserLogin + EOL;

                case AuthorizationState.AcceptedLogin:
                    AuthState = AuthorizationState.SentPassword;
                    return "PASS " + Creds.UserPassword + EOL;
                default:
                    throw new Exception("no_verbs_left");
            }
        }

        internal override bool ParseResponse(string Response)
        {
            System.Windows.Forms.MessageBox.Show(Response);
            //if (!Response.StartsWith(OK))
            //{
            //    AuthState = AuthorizationState.Failed;
            //    OnUserLoginFailed();
            //}
            //
            //switch(AuthState)
            //{
            //    case AuthorizationState.SentLogin:
            //        AuthState = AuthorizationState.AcceptedLogin;
            //        return true;
            //
            //    case AuthorizationState.SentPassword:
            //        AuthState = AuthorizationState.AcceptedPassword;
            //        OnUserLoginSuccess();
            //        ParentService.State = PopState.Transaction;
            //        return true;
            //}
            //
            return false;
        }

        internal override int VerbsLeft()
        {
            switch (AuthState)
            {
                case AuthorizationState.NotStarted: return 2;

                case AuthorizationState.AcceptedLogin:
                case AuthorizationState.SentLogin:  return 1;

                default:                            return 0;
            }
        }

        internal override bool IsMultiline()
        {
            return false;
        }
    }
}
