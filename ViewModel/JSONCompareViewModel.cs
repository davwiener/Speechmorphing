using System;
using System.IO;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using SpeechmorphingHomeAssignment.Model;

namespace SpeechmorphingHomeAssignment.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class JSONCompareViewModel : ViewModelBase
    {

        public JSONCompareViewModel()
        {
            ChooseJsonFileCommand = new RelayCommand<string>(OnChooseJsonFileCommand);
            CompareFileCommand = new RelayCommand(OnCompareFileCommand);
        }
        #region Properties
        /// <summary>
        /// The <see cref="JsonCompare1" /> property's name.
        /// </summary>
        public const string JsonCompare1PropertyName = "JsonCopmapre1";

        private JsonCompare jsonCompare1 = null;

        /// <summary>
        /// Sets and gets the JsonCopmapre1 property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public JsonCompare JsonCompare1
        {
            get
            {
                return jsonCompare1;
            }

            set
            {
                if (jsonCompare1 == value)
                {
                    return;
                }

                jsonCompare1 = value;
                RaisePropertyChanged(JsonCompare1PropertyName);
            }
        }
        /// <summary>
            /// The <see cref="JsonCompare2" /> property's name.
            /// </summary>
        public const string JsonCompare2PropertyName = "JsonCompare2";

        private JsonCompare jsonCompare2 = null;

        /// <summary>
        /// Sets and gets the JsonCompare2 property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public JsonCompare JsonCompare2
        {
            get
            {
                return jsonCompare2;
            }

            set
            {
                if (jsonCompare2 == value)
                {
                    return;
                }

                jsonCompare2 = value;
                RaisePropertyChanged(JsonCompare2PropertyName);
            }
        }
        /// <summary>
            /// The <see cref="FirstPath" /> property's name.
            /// </summary>
        public const string FirstPathPropertyName = "FirstPath";

        private string firstPath = string.Empty;

        /// <summary>
        /// Sets and gets the FirstPath property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FirstPath
        {
            get
            {
                return firstPath;
            }

            set
            {
                if (firstPath == value)
                {
                    return;
                }

                firstPath = value;
                RaisePropertyChanged(FirstPathPropertyName);
            }
        }
        /// <summary>
            /// The <see cref="SecPath" /> property's name.
            /// </summary>
        public const string SecPathPropertyName = "SecPath";

        private string secPath = string.Empty;

        /// <summary>
        /// Sets and gets the SecPath property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SecPath
        {
            get
            {
                return secPath;
            }

            set
            {
                if (secPath == value)
                {
                    return;
                }

                secPath = value;
                RaisePropertyChanged(SecPathPropertyName);
            }
        }
        /// <summary>
            /// The <see cref="ErrorString" /> property's name.
            /// </summary>
        public const string ErrorStringPropertyName = "ErrorString";

        private string errorString = string.Empty;

        /// <summary>
        /// Sets and gets the ErrorString property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ErrorString
        {
            get
            {
                return errorString;
            }

            set
            {
                if (errorString == value)
                {
                    return;
                }

                errorString = value;
                RaisePropertyChanged(ErrorStringPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="DiffrenceStr" /> property's name.
        /// </summary>
        public const string DiffrenceStrPropertyName = "DiffrenceStr";

        private string diffrenceStr = string.Empty;

        /// <summary>
        /// Sets and gets the DiffrenceStr property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string DiffrenceStr
        {
            get
            {
                return diffrenceStr;
            }

            set
            {
                if (diffrenceStr == value)
                {
                    return;
                }

                diffrenceStr = value;
                RaisePropertyChanged(DiffrenceStrPropertyName);
            }
        }
        #endregion
        #region commands
        //build a JsonCompare class
        public ICommand ChooseJsonFileCommand { get; private set; }
        private void OnChooseJsonFileCommand(string param)
        {
            ErrorString = string.Empty;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter =  "Json files (*.json)|*.json";
            dialog.Title = "Please select an Json.";
            dialog.ShowDialog();
            //check if a file has been choosen
            if (dialog.FileName.Trim() != string.Empty)
            {
                if (param == "first")
                {
                    FirstPath = dialog.FileName;
                    JsonCompare1 = new JsonCompare(FirstPath);
                    ErrorString = JsonCompare1.StringError;
                }
                else
                {
                    SecPath = dialog.FileName;
                    JsonCompare2 = new JsonCompare(dialog.FileName);
                    ErrorString = JsonCompare2.StringError;
                }
            }
        }
        //compare if the two selected JSON files are equal
        public ICommand CompareFileCommand { get; private set; }
        private void OnCompareFileCommand()
        {
            ErrorString = string.Empty;
            //first check if tow file are choosen
            if (JsonCompare1 == null || JsonCompare2 == null)
            {
                ErrorString = "Please choose tow JSON files";
                return;
            }
            
            DiffrenceStr = JsonCompare1.CompareTo(jsonCompare2, true) + jsonCompare2.CompareTo(JsonCompare1, false);
            if(DiffrenceStr == string.Empty)
            {
                DiffrenceStr = "The files are equal!";
            }
            ErrorString = JsonCompare2.StringError + " " +JsonCompare1.StringError; ;
        }
        #endregion
    }
}