using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FSI.DeviceSDK.FiberDefenderDevice;
using IntegrationClient.HelperClasses;
using IntegrationClient.Model;

namespace IntegrationClient.ViewModels
{
    /// <summary>
    /// ViewModel for the view that displays APUs
    /// </summary>
    public class ConnectedDevicesViewModel 
    {
        #region Properties

        /// <summary>
        /// Gets an observable collection of APU devices
        /// </summary>
        public ObservableCollection<IFiberDefenderAPUDevice> Devices
        {
            get;
            private set;
        }

        #endregion
        #region Fields

        /// <summary>
        /// The model that talks to the SDK
        /// </summary>
        private DeviceSDKAPUModel m_APUModel;
        
        /// <summary>
        /// The task that will be running the scan
        /// </summary>
        private Task m_Scan = new Task(() => { }); // default task so we don't have to worry about null

        #endregion
        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="model">The <see cref="DeviceSDKAPUModel"/> model to talk to</param>
        public ConnectedDevicesViewModel( DeviceSDKAPUModel model )
        {
            m_APUModel = model;
            Devices = new ObservableCollection<IFiberDefenderAPUDevice>();
            m_APUModel.UseSystemTimeInsteadOfAlarmTime = true; // Already done above, but being explicit
        }

        #endregion
        #region Methods

        /// <summary>
        /// Closes the window
        /// </summary>
        public void Stop()
        {
            m_APUModel.Stop();
        }
        
        /// <summary>
        /// Starts a scan for devices
        /// </summary>
        public void StartScan()
        {
            // Double check lock to ensure we don't keep sending scans to the DeviceSDKAPUModel since it will honour every one
            if (m_Scan.Status != TaskStatus.Running)
            {
                lock (m_Scan)
                {
                    if (m_Scan.Status != TaskStatus.Running)
                    {
                        // Run it asynchronously so that control can return to the UI thread
                        m_Scan = Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                // Lets check model needs to be started
                                if (!m_APUModel.IsStarted)
                                {
                                    m_APUModel.Start();
                                }
                                else
                                {
                                    var apus = m_APUModel.ScanForAPUs();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        });
                    }
                }
            }
        }
        #endregion
    }
}
