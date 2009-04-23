#!/usr/bin/env python
# vim: set tabstop=4 shiftwidth=4 expandtab
##############################################################################
# Written by:  Ray Wang <rawang@novell.com>
# Date:        12/07/2008
# Description: main test script of domainupdown
#              ../samples/domainupdown.py is the test sample script
#              domainupdown/* is the wrapper of domainupdown test sample script
##############################################################################

# The docstring below  is used in the generated log file
"""
Test accessibility of domainupdown widget
"""
# imports
from domainupdown import *
from helpers import *
from states import *
from actions import *
from sys import argv

app_path = None 
try:
  app_path = argv[1]
except IndexError:
  pass #expected

# open the domainupdown sample application
try:
  app = launchDomainUpDown(app_path)
except IOError, msg:
  print "ERROR:  %s" % msg
  exit(2)

# make sure we got the app back
if app is None:
  exit(4)

# just an alias to make things shorter
dudFrame = app.domainUpDownFrame

##############################
# check domainupdown's states
##############################
statesCheck(dudFrame.editable_domainupdown, "DomainUpDown", add_states=["focused"])
statesCheck(dudFrame.uneditable_domainupdown, "DomainUpDown", invalid_states=["editable"])

# move the focused to uneditable_domainupdown then check the states again
dudFrame.uneditable_domainupdown.mouseClick()
statesCheck(dudFrame.editable_domainupdown, "DomainUpDown")
statesCheck(dudFrame.uneditable_domainupdown, "DomainUpDown", invalid_states=["editable"], add_states=["focused"])

##############################
# input text from UI
##############################
# editable DomainUpDown
dudFrame.editable_domainupdown.mouseClick()
dudFrame.editable_domainupdown.typeText("provo")
sleep(config.SHORT_DELAY)
dudFrame.assertText(dudFrame.editable_domainupdown, "Provo")
# check the state of selected item
# TODO: BUG482285
#statesCheck(dudFrame.editable_domainupdown.listitems[4], "ListItem", add_states=["selected"])
# check other item's states, i.e., all the other list items except for the
# 4th index, which is "Provo".
for item in dudFrame.editable_domainupdown.listitems:
    if item.name == "Provo":
        continue
    else:
        # BUG482285, list items should not have editable state
        #statesCheck(item, "ListItem", invalid_states=["showing", "visible"])
        pass # remove this when removing when the bug is fixed

# try inserting some text in the editable DomainUpDown that does not match
# any of the DownUpDown list items, then check the text of the DomainUpDown
# accessible and the states of the list items
dudFrame.keyCombo("Delete", grabFocus=False)
sleep(config.SHORT_DELAY)
dudFrame.assertText(dudFrame.editable_domainupdown, "")
dudFrame.editable_domainupdown.typeText("Novell")
sleep(config.SHORT_DELAY)

# BUG491282, Uppercase characters generated by
# pyatspi.Registry.generateKeyboardEvent are inserted as lowercase characters
# dudFrame.assertText(dudFrame.editable_domainupdown, "Novell")

# check all item states, none of them should be +showing +visible because
# novell is not an actual list item
for item in dudFrame.editable_domainupdown.listitems:
    # BUG482285, list items should not have editable state
    # statesCheck(item, "ListItem", invalid_states=["showing", "visible"])
    pass # remove this when removing when the bug is fixed


# click in the uneditable DomainUpDown control and make sure that pressing
# the c key will bring up the "Cambridge" text.
dudFrame.uneditable_domainupdown.mouseClick()
dudFrame.uneditable_domainupdown.typeText("c")
sleep(config.SHORT_DELAY)
dudFrame.assertText(dudFrame.uneditable_domainupdown, "Cambridge")

# TODO: BUG458607 you should not be able to input any words in the uneditable
# textbox
# uneditable DomainUpDown
#dudFrame.uneditable_domainupdown.typeText("z")
#sleep(config.SHORT_DELAY)
# there is no list item that begins with a "z", so the "Cambridge" text
# should remain in the uneditable DomainUpDown control.  This is the
# behavior on Windows.
#dudFrame.assertText(dudFrame.uneditable_domainupdown, "Cambridge")

# check the state of selected item
# TODO: BUG482285 
#statesCheck(dudFrame.uneditable_domainupdown.listitems[2], "ListItem", add_states=["selected"])
# check other item's states
#statesCheck(dudFrame.uneditable_domainupdown.listitems[3], "ListItem", invalid_states=["showing", "visible"])

#############################
# input text from AtkText
#############################
# editable DomainUpDown
dudFrame.editable_domainupdown.mouseClick()
dudFrame.inputText(dudFrame.editable_domainupdown, "Boston")
sleep(config.SHORT_DELAY)
dudFrame.assertText(dudFrame.editable_domainupdown, "Boston")
# check the state of selected item
# TODO: BUG482285 
#statesCheck(dudFrame.editable_domainupdown.listitems[4], "ListItem", add_states=["selected"])
# check other item's states
#statesCheck(dudFrame.editable_domainupdown.listitems[3], "ListItem", invalid_states=["showing", "visible"])

# uneditable DomainUpDown
dudFrame.uneditable_domainupdown.mouseClick()
dudFrame.inputText(dudFrame.uneditable_domainupdown, "Boston")
sleep(config.SHORT_DELAY)
# the text will not be changed since it is readonly spin button
dudFrame.assertText(dudFrame.uneditable_domainupdown, "Cambridge")
# check the state of selected item
# TODO: BUG482285 
#statesCheck(dudFrame.uneditable_domainupdown.listitems[2], "ListItem", invalid_states=["showing", "visible"])
# check other items' states
#statesCheck(dudFrame.uneditable_domainupdown.listitems[3], "ListItem", invalid_states=["showing", "visible"])

############################
# press Up/Down on editab_domainupdown
############################
dudFrame.editable_domainupdown.mouseClick()
dudFrame.keyCombo("Up", grabFocus=False)
sleep(config.SHORT_DELAY)
dudFrame.assertText(dudFrame.editable_domainupdown, "Madrid")
# check the state of selected item
# TODO: BUG482285 
#statesCheck(dudFrame.editable_domainupdown.listitems[3], "ListItem", add_states=["selected", "focused"])
# check other item's states
#statesCheck(dudFrame.editable_domainupdown.listitems[0], "ListItem", invalid_states=["showing", "visible"])

# press "Down" on editab_domainupdown
dudFrame.keyCombo("Down", grabFocus=False)
sleep(config.SHORT_DELAY)
dudFrame.assertText(dudFrame.editable_domainupdown, "Provo")
# check the state of selected item
# TODO: BUG482285 
#statesCheck(dudFrame.editable_domainupdown.listitems[4], "ListItem", add_states=["selected"])
# check other item's states
#statesCheck(dudFrame.editable_domainupdown.listitems[0], "ListItem", invalid_states=["showing", "visible"])

############################
# press Up/Down on uneditab_domainupdown
############################
dudFrame.uneditable_domainupdown.mouseClick()
dudFrame.keyCombo("Up", grabFocus=False)
sleep(config.SHORT_DELAY)
dudFrame.assertText(dudFrame.uneditable_domainupdown, "Beijing")
# check the state of selected item
# TODO: BUG482285 
#statesCheck(dudFrame.uneditable_domainupdown.listitems[1], "ListItem", add_states=["selected", "focused"])
# check other item's states
#statesCheck(dudFrame.uneditable_domainupdown.listitems[0], "ListItem", invalid_states=["showing", "visible"])

# press "Down" on uneditab_domainupdown
dudFrame.keyCombo("Down", grabFocus=False)
sleep(config.SHORT_DELAY)
dudFrame.assertText(dudFrame.uneditable_domainupdown, "Cambridge")
# check the state of selected item
# TODO: BUG482285 
#statesCheck(dudFrame.uneditable_domainupdown.listitems[2], "ListItem", add_states=["selected", "focused"])
# check other items' states
#statesCheck(dudFrame.uneditable_domainupdown.listitems[0], "ListItem", invalid_states=["showing", "visible"])

############################
# check AtkAction of spin button's child - list item
############################
# editable DomainUpDown
actionsCheck(dudFrame.editable_domainupdown.listitems[0], "ListItem")
actionsCheck(dudFrame.editable_domainupdown.listitems[1], "ListItem")
actionsCheck(dudFrame.editable_domainupdown.listitems[2], "ListItem")
actionsCheck(dudFrame.editable_domainupdown.listitems[3], "ListItem")
actionsCheck(dudFrame.editable_domainupdown.listitems[4], "ListItem")
actionsCheck(dudFrame.editable_domainupdown.listitems[5], "ListItem")

# uneditable DomainUpDown
actionsCheck(dudFrame.uneditable_domainupdown.listitems[0], "ListItem")
actionsCheck(dudFrame.uneditable_domainupdown.listitems[1], "ListItem")
actionsCheck(dudFrame.uneditable_domainupdown.listitems[2], "ListItem")
actionsCheck(dudFrame.uneditable_domainupdown.listitems[3], "ListItem")
actionsCheck(dudFrame.uneditable_domainupdown.listitems[4], "ListItem")
actionsCheck(dudFrame.uneditable_domainupdown.listitems[5], "ListItem")

############################
# check DomainUpDown's AtkSelection
############################
dudFrame.selectChild(dudFrame.editable_domainupdown,
                 dudFrame.editable_domainupdown.listitems[0].getIndexInParent())
dudFrame.assertText(dudFrame.editable_domainupdown, "Austin")
# TODO: BUG482285.  I am not even sure if these list items should be selectable
#statesCheck(dudFrame.editable_domainupdown.listitems[0], "ListItem", add_states=["selected", "focused"])
# check other items' states
#statesCheck(dudFrame.editable_domainupdown.listitems[5], "ListItem", invalid_states=["showing", "visible"])

dudFrame.selectChild(dudFrame.uneditable_domainupdown,
               dudFrame.uneditable_domainupdown.listitems[0].getIndexInParent())
dudFrame.assertText(dudFrame.uneditable_domainupdown, "Austin")
# TODO: BUG482285 
#statesCheck(dudFrame.uneditable_domainupdown.listitems[0], "ListItem", add_states=["selected", "focused"])
# check other items' states
#statesCheck(dudFrame.uneditable_domainupdown.listitems[5], "ListItem", invalid_states=["showing", "visible"])

############################
# Ensure that the list items' click actions works as expected
############################
for item in dudFrame.editable_domainupdown.listitems:
    procedurelogger.action('Click on %s in the editable DomainUpDown control' % item)
    item.click()
    sleep(config.SHORT_DELAY)
    dudFrame.assertText(dudFrame.editable_domainupdown, item.name)
    dudFrame.checkAllStates(dudFrame.editable_domainupdown.listitems, item)

for item in dudFrame.uneditable_domainupdown.listitems:
    procedurelogger.action('Click on %s in the uneditable DomainUpDown control' % item)
    item.click()
    sleep(config.SHORT_DELAY)
    dudFrame.assertText(dudFrame.uneditable_domainupdown, item.name)
    dudFrame.checkAllStates(dudFrame.uneditable_domainupdown.listitems, item)

############################
# end
############################
# close application frame window
dudFrame.quit()

print "INFO:  Log written to: %s" % config.OUTPUT_DIR
