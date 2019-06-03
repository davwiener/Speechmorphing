using System.Windows;
using SpeechmorphingHomeAssignment.ViewModel;

namespace SpeechmorphingHomeAssignment
{
    /// <summary>
    /// Interaction logic for JSONCompareView.xaml
    /// </summary>
    public partial class JSONCompareView : Window
    {
        /// <summary>
        /// Initializes a new instance of the JSONCompareView class.
        /// </summary>
        public JSONCompareView()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }
    }
}