﻿using ControlLibrary;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CMS.WPFHeadOffice
{
    /// <summary>
    /// An ICommand implementation displaying a message box.
    /// </summary>
    public class SampleMsgBoxCommand
        : CommandBase
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected override void OnExecute(object parameter)
        {
            ModernDialog.Show("A messagebox triggered by selecting a hyperlink", "Messagebox", MessageBoxButton.OK);
        }
    }
}
