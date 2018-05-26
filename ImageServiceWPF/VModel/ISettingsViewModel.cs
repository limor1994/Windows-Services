
namespace ImageServiceWPF.VModel
{
    /// <summary>
    /// ISettingsViewModel interface
    /// </summary>
    interface ISettingsViewModel
    {
        /// <summary>
        /// Outputdir getter
        /// </summary>
        string VM_OutputDirectory { get; }


        /// <summary>
        /// Sourcename getter
        /// </summary>
        string VM_SourceName { get; }

        /// <summary>
        /// logname getter
        /// </summary>
        string VM_LogName { get; }

        /// <summary>
        /// thumbnail getter
        /// </summary>
        int VM_ThumbnailSize { get; }

    }
}
