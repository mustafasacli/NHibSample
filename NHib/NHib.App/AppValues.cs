using System;

namespace NHib.App
{
    public static class AppValues
    {
        //System.Diagnostics.Trace.CorrelationManager.ActivityId = Guid.NewGuid();
        private static Guid gd = Guid.Empty;

        private static object lockObj = new object();

        public static Guid ActivityId
        {
            get
            {
                if (gd == null || gd == Guid.Empty)
                {
                    lock (lockObj)
                    {
                        if (gd == null || gd == Guid.Empty)
                        {
                            gd = Guid.NewGuid();
                        }
                    }
                }

                return gd;
            }
        }
    }
}
