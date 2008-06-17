# -*- coding: utf-8 -*-

##############################################################################$
# Written by:  Brian G. Merrell <bgmerrell@novell.com>$
# Date:        May 23 2008$
# Description: Application wrapper for checkButton.py 
#              Used by the checkbutton-*.py tests
##############################################################################$

'Application wrapper for checkbutton'

from strongwind import *

import os

def launchCheckButton(exe=None):
    'Launch checkbutton with accessibility enabled and return a Checkbutton object.  Log an error and return None if something goes wrong'

    if exe is None:
        # make sure we can find the sample application
        uiaqa_path = os.environ.get("UIAQA_HOME")
        if uiaqa_path is None:
          raise IOError, "When launching an application you must provide the "\
                         "full path or set the\nUIAQA_HOME environment "\
                         "variable."

        exe = '%s/samples/checkbutton.py' % uiaqa_path
   
    if not os.path.exists(exe):
      raise IOError, "%s does not exist" % exe
  
    args = [exe]

    (app, subproc) = cache.launchApplication(args=args)

    checkbutton = CheckButton(app, subproc)
    cache.addApplication(checkbutton)

    checkbutton.checkButtonFrame.app = checkbutton

    return checkbutton

# class to represent the application
class CheckButton(accessibles.Application):
    def __init__(self, accessible, subproc=None):
        'Get a reference to the Check Button window'
        super(CheckButton, self).__init__(accessible, subproc)
        self.findFrame(re.compile('^Check Button'), logName='Check Button')
