using NLog;

namespace Registration.Logger
{
    public static class NLogger
    {
        public static NLog.Logger Logger = LogManager.GetCurrentClassLogger();
    }
}