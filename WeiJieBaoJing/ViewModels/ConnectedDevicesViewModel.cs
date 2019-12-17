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
        /// <summary>
        /// Gets or sets the currently selected item in the Devices collection
        /// </summary>
        public IFiberDefenderAPUDevice DevicesSelectedItem { get; set; }
        /// <summary>
        /// Gets if the progress status bar should be visible
        /// </summary>
        public bool IsProgressStatusVisible
        {
            get
            {
                return m_IsProgressStatusVisible;
            }
            private set
            {
                if (value != m_IsProgressStatusVisible)
                {
                    m_IsProgressStatusVisible = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets whether or not to use the System time as the timestamps for the model
        /// </summary>
        public bool UseSystemTimeAsAlarmTime
        {
            get
            {
                return m_UseSystemTimeAsAlarmTime;
            }
            set
            {
                if (value != m_UseSystemTimeAsAlarmTime)
                {
                    m_UseSystemTimeAsAlarmTime = value;
                    // Update the model
                    m_APUModel.UseSystemTimeInsteadOfAlarmTime = value;
                }
            }
        }

        #endregion
        #region Fields

        /// <summary>
        /// The model that talks to the SDK
        /// </summary>
        private DeviceSDKAPUModel m_APUModel;

        /// <summary>
        /// ICommand to request a close of the application
        /// </summary>
        private ICommand m_CloseCommand;
        /// <summary>
        /// Boolean to say whether scan progress bar should be visible
        /// </summary>
        private bool m_IsProgressStatusVisible;
        /// <summary>
        /// The task that will be running the scan
        /// </summary>
        private Task m_Scan = new Task(() => { }); // default task so we don't have to worry about null
        /// <summary>
        /// Boolean to say whether system time is used instead of the APU time for time stamps
        /// </summary>
        private bool m_UseSystemTimeAsAlarmTime;

        #endregion
        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="model">The <see cref="DeviceSDKAPUModel"/> model to talk to</param>
        public ConnectedDevicesViewModel( DeviceSDKAPUModel model )
        {
            m_CloseCommand = null;
            m_APUModel = model;

            Devices = new ObservableCollection<IFiberDefenderAPUDevice>();
            DevicesSelectedItem = null;
            IsProgressStatusVisible = false;
            UseSystemTimeAsAlarmTime = true;
            m_APUModel.UseSystemTimeInsteadOfAlarmTime = true; // Already done above, but being explicit

            //Creating Commands with the methods that they will call
            //RefreshCommand = new DelegateCommand(param => OnRefresh());
            //RemoveCommand = new DelegateCommand(param => OnRemove());
            //StartScanCommand = new DelegateCommand(param => StartScan());
            //ToggleTimeSourceCommand = new DelegateCommand(param => ToggleTimeSource());

            // Perform initial scan on start
            //StartScan();
        }

        #endregion
        #region Methods

        /// <summary>
        /// Closes the window
        /// </summary>
        private void OnClose()
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
                        // We are scanning so lets show the progress bar
                        IsProgressStatusVisible = true;
                        // Run it asynchronously so that control can return to the UI thread
                        m_Scan = Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                // Lets check model needs to be started
                                if (!m_APUModel.IsStarted)
                                {
                                    m_APUModel.Start();
                                    IsProgressStatusVisible = false;
                                }
                                else
                                {
                                    var apus = m_APUModel.ScanForAPUs();
                                    IsProgressStatusVisible = false;
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

        /// <summary>
        /// Toggles where the timestamp of alarms comes from.
        /// </summary>
        private void ToggleTimeSource()
        {
            // Performs a toggle and updates the model with the new time
            UseSystemTimeAsAlarmTime = !UseSystemTimeAsAlarmTime;
            m_APUModel.UseSystemTimeInsteadOfAlarmTime = UseSystemTimeAsAlarmTime;
        }

        #endregion
    }
}
