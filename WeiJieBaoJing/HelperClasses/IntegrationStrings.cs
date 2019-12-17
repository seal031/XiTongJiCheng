
namespace IntegrationClient.HelperClasses
{
    /// <summary>
    /// This class contains the constant strings that are used to communicate with other software
    /// </summary>
    /// <remarks>
    /// The strings in this class should be changed to match the software that this application is integrating with
    /// </remarks>
    public class IntegrationStrings
    {
        /// <summary>
        /// The string that represents an alarm
        /// </summary>
        public const string Alarm = "Alarm";
        /// <summary>
        /// The string that represents a loss of communication
        /// </summary>
        public const string CommunicationsLost = "Communications Lost";
        /// <summary>
        /// The string that represents a restoration in communication
        /// </summary>
        public const string CommunicationsRestored = "Communications Restored";
        /// <summary>
        /// The string that represents a fault has ended
        /// </summary>
        public const string FaultOff = "Fault Off";
        /// <summary>
        /// The string that represents a fault has occurred
        /// </summary>
        public const string FaultOn = "Fault On";
        /// <summary>
        /// The string that represents a tamper has ended
        /// </summary>
        public const string TamperOff = "Tamper Off";

        /// <summary>
        /// The string that represents a tamper has occurred
        /// </summary>
        public const string TamperOn = "Tamper On";

        /// <summary>
        /// The string that represents an unknown alarm type
        /// </summary>
        public const string UnknownAlarm = "Unknown Alarm";
    }
}
