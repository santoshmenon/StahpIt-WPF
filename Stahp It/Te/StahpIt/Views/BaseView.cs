﻿/*
* Copyright (c) 2016 Jesse Nicholson.
*
* This file is part of Stahp It.
*
* Stahp It is free software: you can redistribute it and/or
* modify it under the terms of the GNU General Public License as published
* by the Free Software Foundation, either version 3 of the License, or (at
* your option) any later version.
*
* In addition, as a special exception, the copyright holders give
* permission to link the code of portions of this program with the OpenSSL
* library.
*
* You must obey the GNU General Public License in all respects for all of
* the code used other than OpenSSL. If you modify file(s) with this
* exception, you may extend this exception to your version of the file(s),
* but you are not obligated to do so. If you do not wish to do so, delete
* this exception statement from your version. If you delete this exception
* statement from all source files in the program, then also delete it
* here.
*
* Stahp It is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General
* Public License for more details.
*
* You should have received a copy of the GNU General Public License along
* with Stahp It. If not, see <http://www.gnu.org/licenses/>.
*/

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NLog;
using System;
using System.Windows.Controls;

namespace Te.StahpIt.Views
{   

    /// <summary>
    /// Base class that all views in this application should extend from. This is so that each of
    /// those views has the basic mechanism for data or user driven events to raise requests to
    /// modify the current view to respond to or notify the user.
    /// </summary>
    public class BaseView : UserControl, IViewController
    {
        /// <summary>
        /// Event for when a view requests another view.
        /// </summary>
        public event ViewChangeRequestCallback ViewChangeRequest;

        /// <summary>
        /// Logger for views.
        /// </summary>
        protected readonly Logger m_logger;

        /// <summary>
        /// Default ctor.
        /// </summary>
        public BaseView()
        {
            m_logger = LogManager.GetLogger("StahpIt");
        }

        /// <summary>
        /// Requests a change of view.
        /// </summary>
        /// <param name="view">
        /// The requested view.
        /// </param>
        /// <param name="data">
        /// Optional data for the requested view.
        /// </param>
        protected void RequestViewChange(View view, object data = null)
        {
            if (ViewChangeRequest != null)
            {
                var args = new ViewChangeRequestArgs(view, data);
                ViewChangeRequest(this, args);
            }
        }

        /// <summary>
        /// Displays a message to the user as an overlay, that the user can only accept.
        /// </summary>
        /// <param name="title">
        /// The large title for the message overlay.
        /// </param>
        /// <param name="message">
        /// The message content.
        /// </param>
        /// <param name="acceptButtonText">
        /// The text to display in the acceptance button.
        /// </param>
        protected void ShowUserMessage(string title, string message, string acceptButtonText = "Ok")
        {
            MetroDialogSettings mds = new MetroDialogSettings();
            mds.AffirmativeButtonText = acceptButtonText;
            MetroWindow parentWindow = this.TryFindParent<MetroWindow>();

            if (parentWindow != null)
            {
                DialogManager.ShowMessageAsync(parentWindow, title, message, MessageDialogStyle.Affirmative, mds);
            }
        }
    }
}