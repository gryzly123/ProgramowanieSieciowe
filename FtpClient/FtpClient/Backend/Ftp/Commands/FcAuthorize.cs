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

        public NetEventRetVal OnUserLogin;
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
            bool Success = false;
            switch(AuthState)
            {
                case AuthorizationState.SentLogin:
                    if (Response.StartsWith("331")) AuthState = AuthorizationState.AcceptedLogin;
                    else if (Response.StartsWith("230"))
                    {
                        AuthState = AuthorizationState.AcceptedPassword;
                        OnUserLogin(true);
                    }
                    else AuthState = AuthorizationState.Failed;
                    return (AuthState != AuthorizationState.Failed);
            
                case AuthorizationState.SentPassword:
                    Success = Response.StartsWith("230");
                    AuthState = Success ? AuthorizationState.AcceptedPassword : AuthorizationState.Failed;
                    OnUserLogin(Success);
                    return Success;
            }
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
