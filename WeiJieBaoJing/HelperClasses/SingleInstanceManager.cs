using System;
using Microsoft.VisualBasic.ApplicationServices;

namespace IntegrationClient.HelperClasses
{
    // Using VB bits to detect single instances and process accordingly:
    //  * OnStartup is fired when the first instance loads
    //  * OnStartupNextInstance is fired when the application is re-run again
    //    NOTE: it is redirected to this instance thanks to IsSingleInstance
    public class SingleInstanceManager<T> : WindowsFormsApplicationBase where T : System.Windows.Application
    {
        private T m_Application;
        private Action m_ActivateMethod;

        /// <summary>
        /// A constructor taking an instance of an <see cref="System.Windows.Application"/> type
        /// </summary>
        /// <param name="application">The instance of the <see cref="System.Windows.Application"/> type to maintain a single instance of</param>
        /// <param name="methodToRunOnNewInstance">A method to run when the application is attempted to be started again</param>
        public SingleInstanceManager( T application, Action methodToRunOnNewInstance )
        {
            this.IsSingleInstance = true;
            m_Application = application;
            m_ActivateMethod = methodToRunOnNewInstance;
        }

        protected override bool OnStartup( StartupEventArgs e )
        {
            // First time app is launched
            m_Application.Run();
            return false;
        }

        protected override void OnStartupNextInstance( StartupNextInstanceEventArgs eventArgs )
        {
            // Subsequent launches
            base.OnStartupNextInstance(eventArgs);
            m_ActivateMethod();
        }
    }
}
