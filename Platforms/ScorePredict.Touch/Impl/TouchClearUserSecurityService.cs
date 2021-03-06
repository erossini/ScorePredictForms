﻿using Foundation;
using ScorePredict.Services.Contracts;

namespace ScorePredict.Touch.Impl
{
    public class TouchClearUserSecurityService : IClearUserSecurityService
    {
        #region IClearUserSecurityService implementation

        public void ClearUserSecurity()
        {
            var defaults = NSUserDefaults.StandardUserDefaults;
            if (defaults.ValueForKey(new NSString(TouchConstants.UserIdKey)) != null)
                defaults.RemoveObject(TouchConstants.UserIdKey);

            if (defaults.ValueForKey(new NSString(TouchConstants.TokenKey)) != null)
                defaults.RemoveObject(TouchConstants.TokenKey);

            if (defaults.ValueForKey(new NSString(TouchConstants.UsernameKey)) != null)
                defaults.RemoveObject(TouchConstants.UsernameKey);

            defaults.Synchronize();
        }

        #endregion
    }
}

