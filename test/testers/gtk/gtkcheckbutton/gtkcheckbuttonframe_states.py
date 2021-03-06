##############################################################################$
# Written by:  Brian G. Merrell <bgmerrell@novell.com>$
# Date:        May 23 2008$
# Description: gtkcheckbutton.py wrapper script
#              Used by the gtkcheckbutton-*.py tests
##############################################################################$

from strongwind import *
import states

# class to represent the main window.
class GtkCheckButtonFrame(accessibles.Frame):

    # constants
    # the available widgets on the window
    CHECK_BUTTON_ONE = "check button 1"
    CHECK_BUTTON_TWO = "check button 2"
    BUTTON_QUIT = "Quit"

    # available results for the check boxes
    RESULT_UNCHECKED = "unchecked"
    RESULT_CHECKED = "checked"
    # end constants

    def __init__(self, accessible):
        super(GtkCheckButtonFrame, self).__init__(accessible)
        self.checkbox1 = self.findCheckBox(self.CHECK_BUTTON_ONE)
        self.checkbox2 = self.findCheckBox(self.CHECK_BUTTON_TWO)

    #check checkbox's all expectant states
    def statesCheck(self, accessible, control,
                                invalid_states=[], add_states=[]):
        """Check the states of an accessible using the default states
        of the accessible (specified by control class in states.py) as
        the default expected states.
       
        Keyword arguments:
        accessible -- the accessible whose states will be checked
        control -- the class name of the control whose states we want to check
        invalid_states -- a list of states that should be removed from the
        list of default expected states
        add_states -- a list of states that should be added to the list of
        default expected states

        """
        procedurelogger.action('Check %s\'s states' % accessible)
        # create a list of all states for button except "sensitive"
        states_list = states.__getattribute__(control).states
        expected_states = \
                  [s for s in states_list if s not in invalid_states]
        expected_states = set(expected_states).union(set(add_states))

        procedurelogger.expectedResult('States:  %s' % expected_states)

        # get a list of all actual states for accessible
        actual_states = accessible._accessible.getState().getStates()
        # need to convert the numbers retrieved above into their associated
        # strings
        actual_states = [pyatspi.stateToString(s) for s in actual_states]

        # assert there are no elements in expected_states that are not
        # in actual_states
        missing_states = set(expected_states).difference(set(actual_states))

        # assert there are no elements in actual_states that are not
        # in expected_states
        extra_states = set(actual_states).difference(set(expected_states))

        is_same = len(missing_states) == 0 and len(extra_states) == 0
        assert is_same, "\n  %s: %s\n  %s: %s" %\
                                             ("Missing actual states: ",
                                               missing_states,
                                              "Extraneous actual states: ",
                                               extra_states) 

    def assertChecked(self, accessible):
        'Raise exception if the accessible does not match the given result'   
        procedurelogger.expectedResult('%s is %s.' % (accessible, self.RESULT_CHECKED))
        def resultMatches():
            return accessible.checked
	
        assert retryUntilTrue(resultMatches)

    def assertUnchecked(self, accessible):
        'Raise exception if the accessible does not match the given result'   
        procedurelogger.expectedResult('%s is %s.' % (accessible, self.RESULT_UNCHECKED))

        def resultMatches():
            return not accessible.checked
	
        assert retryUntilTrue(resultMatches)

    def quit(self):
        'Quit checkbutton'

        # click the quit button
        self.app.findPushButton(self.BUTTON_QUIT).click()

        self.assertClosed()

    def assertClosed(self):
        super(GtkCheckButtonFrame, self).assertClosed()

        # if the checkbutton window closes, the entire app should close.  
        # assert that this is true 
        self.app.assertClosed()
