
##############################################################################
# Written by:  Cachen Chen <cachen@novell.com>
# Date:        09/16/2008
# Description: notifyicon.py wrapper script
#              Used by the notifyicon-*.py tests
##############################################################################$

import sys
import os
import actions
import states

from strongwind import *
from notifyicon import *


# class to represent the main window.
class NotifyIconFrame(accessibles.Frame):

    # constants
    # the available widgets on the window
    BUTTON_ONE = "notifyicon"
    BUTTON_TWO = "balloon"

    def __init__(self, accessible):
        super(NotifyIconFrame, self).__init__(accessible)
        self.notifyicon_button = self.findPushButton(self.BUTTON_ONE)
        self.balloon_button = self.findPushButton(self.BUTTON_TWO)

    #give 'click' action
    def click(self, button):
        button.click()

    #find all widgets from alert windows
    def balloonWidgets(self):
        #self.balloon_alert = self.app.findAlert("Hello")
        self.balloon_alert = self.app.findAlert(None)
        self.label = self.balloon_alert.findLabel("I'm NotifyIcon")
        self.icon = self.balloon_alert.findIcon(None)
 
    #close application main window after running test
    def quit(self):
        self.altF4()
