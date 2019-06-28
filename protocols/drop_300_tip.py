
# coding: utf-8

# In[1]:


from opentrons import labware, instruments, robot, containers
import signal
import sys
import pickle
import subprocess

robot.connect()
robot.home()

if 'tiprack' not in locals():
    tiprack = labware.load('opentrons-tiprack-300ul', '1')

# pipettes
if 'pipette_300' not in locals():
    pipette_300 = instruments.P300_Single(mount='left', tip_racks=[tiprack])

print("Droping tip from 300 ul pipette")
pipette_300.drop_tip()
print("Homing robot")
robot.home()

    

